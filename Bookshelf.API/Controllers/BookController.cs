using Bookshelf.API.Data;
using Bookshelf.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.API.Controllers
{
    [Route("v1/book/")]
    public class BookController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] string query, 
            [FromServices] BookshelfDbContext context,
            [FromServices] BookRepository repository)
        {
            var books = await repository.GetBooksAsync(context, query);

            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookDetails([FromRoute] int id,
            [FromServices] BookshelfDbContext context,
            [FromServices] BookRepository repository)
        {
            var book = await repository.GetBookDetailsAsync(context, id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }
    }
}
