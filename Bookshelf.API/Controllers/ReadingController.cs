using Bookshelf.API.Data;
using Bookshelf.API.Repositories;
using Bookshelf.API.ViewModels.Reading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bookshelf.API.Controllers;

[Route("v1/reading/")]
public class ReadingController : ControllerBase
{
    [Authorize]
    [HttpPost("new")]
    public async Task<IActionResult> AddReading([FromServices] BookshelfDbContext context,
        [FromServices] ReadingRepository repository,
        [FromBody] CreateReadingViewModel viewModel)
    {
        var reading = await repository.AddReadingAsync(context, viewModel, int.Parse(User.FindFirstValue("id"))); ;

        return Created("", reading);
    }

    [Authorize]
    [HttpGet("{readerId:int}")]
    public async Task<IActionResult> GetReadings([FromServices] BookshelfDbContext context,
        [FromServices] ReadingRepository repository,
        [FromRoute] int readerId)
    {
        var readings = await repository.GetReadingsAsync(context, readerId);

        return Ok(readings);
    }
}
