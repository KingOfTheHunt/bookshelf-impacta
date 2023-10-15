using Bookshelf.Web.Extensions;
using Bookshelf.Web.Services;
using Bookshelf.Web.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.GetValueFromSession("token") != null)
                return RedirectToAction(nameof(Index), "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginAccountViewModel viewModel,
            [FromServices] AccountService accountService)
        {
            try
            {
                var token = await accountService.LoginAsync(viewModel);
                HttpContext.SetValueInSession("token", token);
                HttpContext.SetValueInSession("userName", viewModel.Login);

                return RedirectToAction(nameof(Profile));
            }
            catch (Exception)
            {
                return BadRequest("Houve um problema na hora de realizar o login.");
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

        [HttpGet]
        public async Task<IActionResult> Profile([FromServices] AccountService accountService)
        {
            if (HttpContext.GetValueFromSession("token") == null)
                return RedirectToAction(nameof(Index));

            try
            {
                var viewModel = await accountService.GetAccountAsync(HttpContext.GetValueFromSession("userName"),
                    HttpContext.GetValueFromSession("token"));
                return View(viewModel);
            }
            catch (Exception)
            {
                return BadRequest("Houve algum problema!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string userName,
            [FromServices] AccountService accountService)
        {
            try
            {
                var result = await accountService.DeleteAccountAsync(userName,
                    HttpContext.GetValueFromSession("token"));
                HttpContext.Session.Clear();

                return RedirectToAction(nameof(Index), "Home");
            }
            catch (Exception)
            {
                return BadRequest("Houve um problema na hora de deletar a conta.");
            }
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromServices] AccountService accountService,
            ChangePassowordAccountViewModel viewModel)
        {
            try
            {
                viewModel.UserName = HttpContext.GetValueFromSession("userName");
                await accountService.UpdatePasswordAsync(viewModel, viewModel.UserName,
                    HttpContext.GetValueFromSession("token"));

                return RedirectToAction(nameof(Profile));
            }
            catch (Exception)
            {
                return BadRequest("Houve um problema na hora de atualizar a senha.");
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.ClearSession();

            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
