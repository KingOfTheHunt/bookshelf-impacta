using Bookshelf.API.Data;
using Bookshelf.API.Repositories;
using Bookshelf.API.Services;
using Bookshelf.API.ViewModels.Reader;
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
            return NotFound(new { message = "Usuário/senha incorreto(s)" });

        return Ok(tokenService.GenerateToken(reader));
    }
}
