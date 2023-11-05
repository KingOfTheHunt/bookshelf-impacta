using Bookshelf.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Web.Controllers
{
    public class BookController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] string query,
            [FromServices] BookService bookService)
        {
            ViewData["query"] = query;
            var books = await bookService.SearchBooksAsync(query);

            return View(books);
        }

        [HttpGet("/Details/{id:int}")]
        public async Task<IActionResult> Details([FromRoute] int id,
            [FromServices] BookService bookService)
        {
            var book = await bookService.GetBookDetailsAsync(id);

            if (book == null)
                return NotFound();

            return View(book);
        }
    }
}
