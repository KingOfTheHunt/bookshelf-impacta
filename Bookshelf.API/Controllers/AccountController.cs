using Bookshelf.API.Data;
using Bookshelf.API.Repositories;
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
        var reader = await readerRepository.Insert(context, readerViewModel);

        return Created("", new { name = reader.Name, userName = reader.UserName });
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn()
    {
        return Ok();
    }
}
