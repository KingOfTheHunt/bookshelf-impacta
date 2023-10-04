using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Web.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index([FromQuery] string query)
        {
            ViewData["Title"] = query;
            return View();
        }
    }
}
