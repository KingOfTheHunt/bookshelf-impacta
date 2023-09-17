using Bookshelf.API.Data;
using Bookshelf.API.Repositories;
using Bookshelf.API.Services;
using Bookshelf.API.ViewModels.Reader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.API.Controllers;

[Route("v1/account/")]
[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromServices] BookshelfDbContext context,
        [FromBody] CreateReaderViewModel readerViewModel,
        [FromServices] ReaderRepository readerRepository)
    {
        var reader = await readerRepository.InsertAsync(context, readerViewModel);

        return Created("", new { name = reader.Name, userName = reader.UserName });
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromServices] BookshelfDbContext context,
        [FromServices] ReaderRepository readerRepository,
        [FromServices] TokenService tokenService,
        [FromBody] LoginReaderViewModel readerViewModel)
    {
        var reader = await readerRepository.GetReaderAsync(context, readerViewModel);

        if (reader == null)
            return NotFound(new { message = "Usuário e/ou senha incorreto(s)" });

        return Ok(tokenService.GenerateToken(reader));
    }

    [Authorize]
    [HttpGet("{userName}")]
    public async Task<IActionResult> GetUser([FromRoute] string userName,
        [FromServices] ReaderRepository repository,
        [FromServices] BookshelfDbContext context)
    {
        if (userName != User.Identity.Name)
        {
            return Forbid();
        }

        var reader = await repository.GetReaderAsync(context, userName);

        if (reader == null)
        {
            return BadRequest(new { message = "Não foi encontrado nenhum leitor com este username." });
        }

        return Ok(new { reader.Name, reader.UserName, reader.Email, reader.Image });
    }

    [Authorize]
    [HttpPut("{userName}/change-password")]
    public async Task<IActionResult> ChangePassword([FromRoute] string userName,
        [FromServices] BookshelfDbContext context,
        [FromBody] ChangePasswordReaderViewModel viewModel,
        [FromServices] ReaderRepository repository)
    {
        if (User.Identity.Name == userName)
        {
            return Forbid();
        }

        var result = await repository.UpdatePasswordReader(context, viewModel);

        if (result == false)
            return BadRequest("Houve um problema na hora de atualizar a senha.");

        return Ok(new { message = "Atualizado com sucesso!" });
    }

    [Authorize]
    [HttpDelete("{userName}/delete")]
    public async Task<IActionResult> DeleteAccount()
    {
        return Ok();
    }
}
