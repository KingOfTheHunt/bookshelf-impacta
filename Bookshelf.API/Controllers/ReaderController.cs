using Bookshelf.API.Data;
using Bookshelf.API.Repositories;
using Bookshelf.API.ViewModels.Reader;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.API.Controllers;

[Route("v1/reader/")]
[ApiController]
public class ReaderController : ControllerBase
{
    [HttpPost("sing-up")]
    public async Task<IActionResult> SingUp([FromServices] BookshelfDbContext context,
        [FromBody] CreateReaderViewModel readerViewModel,
        [FromServices] ReaderRepository readerRepository)
    {
        var reader = await readerRepository.Insert(context, readerViewModel);

        return Created("", reader);
    }
}
