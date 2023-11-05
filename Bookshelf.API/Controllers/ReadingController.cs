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
    [HttpGet]
    public async Task<IActionResult> GetReadings([FromServices] BookshelfDbContext context,
        [FromServices] ReadingRepository repository,
        [FromRoute] int readerId)
    {
        var readings = await repository.GetReadingsAsync(context, int.Parse(User.FindFirstValue("id")));

        return Ok(readings);
    }

    [Authorize]
    [HttpGet("get-reading/{readingId:int}")]
    public async Task<IActionResult> GetReadingAsync([FromServices] BookshelfDbContext context,
        [FromServices] ReadingRepository repository,
        [FromRoute] int readingId)
    {
        var reading = await repository.GetReadingAsync(context, readingId);

        if (reading == null)
            return NotFound();

        return Ok(reading);
    }

    [Authorize]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateReadingAsync([FromServices] BookshelfDbContext context,
        [FromServices] ReadingRepository repository,
        [FromBody] UpdateReadingViewModel viewModel)
    {
        var result = await repository.UpdateReadingAsync(context, viewModel);

        if (result == false)
            return BadRequest(new { message = "Não foi possível atualizar a leitura!" });

        return NoContent();
    }

    [Authorize]
    [HttpDelete("delete/{readingId:int}")]
    public async Task<IActionResult> DeleteReadingAsync([FromServices] BookshelfDbContext context,
        [FromServices] ReadingRepository repository,
        [FromRoute] int readingId)
    {
        var result = await repository.DeleteReadingAsync(context, readingId);

        if (result == false) return BadRequest(new { message = "Não foi possível deletar a leitura!" });

        return NoContent();
    }
}
