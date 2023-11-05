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

        [HttpGet("/Update/{id:int}")]
        public async Task<IActionResult> Update([FromServices] ReadingService readingService,
            [FromRoute] int id)
        {
            var reading = await readingService.GetReadingAsync(HttpContext.GetValueFromSession("token"),
                id);

            return View(reading);
        }

        [HttpPost("/Update/{id:int}")]
        public async Task<IActionResult> Update([FromServices] ReadingService readingService,
            [FromForm] ReadingDetailsViewModel viewModel)
        {
            var updateViewModel = new UpdateReadingViewModel
            {
                Id = viewModel.Id,
                PagesRead = viewModel.PagesRead,
                ReadingStatus = viewModel.ReadingStatus
            };

            var result = await readingService.UpdateReadingAsync(HttpContext.GetValueFromSession("token"),
                updateViewModel);

            if (result == true)
                return RedirectToAction(nameof(Index));

            return BadRequest("Não foi possível atualizar a leitura!");
        }
    }
}
