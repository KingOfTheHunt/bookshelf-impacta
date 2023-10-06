using Bookshelf.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Web.Controllers
{
    public class BookController : Controller
    {
        public async Task<IActionResult> Index([FromQuery] string query,
            [FromServices] BookService bookService)
        {
            var books = await bookService.SearchBooksAsync(query);
            
            return View(books);
        }
    }
}
