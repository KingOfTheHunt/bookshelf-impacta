using Bookshelf.API.Data;
using Bookshelf.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.API.Controllers
{
    [Route("v1/books/")]
    public class BookController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBooks([FromServices] BookshelfDbContext context,
            [FromServices] BookRepository repository)
        {
            var books = await repository.GetBooksAsync(context);

            return Ok(books);
        }
    }
}
