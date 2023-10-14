using Bookshelf.API.Data;
using Bookshelf.API.Repositories;
using Bookshelf.API.ViewModels.Book;
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

        [HttpGet("{isbn}")]
        public async Task<IActionResult> GetBookDetails([FromRoute] string isbn,
            [FromServices] BookshelfDbContext context,
            [FromServices] BookRepository repository)
        {
            var book = await repository.GetBookDetailsAsync(context, isbn);

            if (book == null)
                return NotFound();

            return Ok(book);
        }
    }
}
