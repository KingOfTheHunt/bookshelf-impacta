using Bookshelf.Web.Services;
using Bookshelf.Web.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginAccountViewModel viewModel,
            [FromServices] AccountService accountService)
        {
            try
            {
                var token = await accountService.LoginAsync(viewModel);
            }
            catch (Exception)
            {
                return BadRequest("Houve um problema na autenticação.");
            }
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(CreateAccountViewModel viewModel,
            [FromServices] AccountService accountService)
        {
            try
            {
                await accountService.CreateAccountAsync(viewModel);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest("Houve um problema na hora de cadastrar.");
            }
        }
    }
}
