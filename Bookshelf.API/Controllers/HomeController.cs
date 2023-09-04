using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.API.Controllers;

[Route("v1/")]
[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("OK");
    }
}
