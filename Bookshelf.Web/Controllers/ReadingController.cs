using Bookshelf.Web.Extensions;
using Bookshelf.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Web.Controllers
{
    public class ReadingController : Controller
    {
        public async Task<IActionResult> Index([FromServices] ReadingService readingService)
        {
            if (HttpContext.GetValueFromSession("token") == null)
                return RedirectToAction("Index", "Account");

            var readings = await readingService.GetReadingsAsync(HttpContext.GetValueFromSession("token"));

            return View(readings);
        }
    }
}
