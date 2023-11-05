using Bookshelf.Web.Extensions;
using Bookshelf.Web.Services;
using Bookshelf.Web.ViewModels.Reading;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Web.Controllers
{
    public class ReadingController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index([FromServices] ReadingService readingService)
        {
            if (HttpContext.GetValueFromSession("token") == null)
                return RedirectToAction("Index", "Account");

            var readings = await readingService.GetReadingsAsync(HttpContext.GetValueFromSession("token"));

            return View(readings);
        }

        [HttpGet("/AddReading")]
        public async Task<IActionResult> AddReading([FromServices] ReadingService readingService,
            [FromQuery] int bookId)
        {
            if (HttpContext.GetValueFromSession("token") == null)
                return RedirectToAction("Index", "Account");

            var viewModel = new AddReadingViewModel
            {
                BookId = bookId
            };

            var result = await readingService.AddReadingAsync(HttpContext.GetValueFromSession("token"),
                viewModel);

            if (result == true)
                return RedirectToAction(nameof(Index));
            else
                return BadRequest("Não foi possível adicionar uma nova leitura");
        }
    }
}
