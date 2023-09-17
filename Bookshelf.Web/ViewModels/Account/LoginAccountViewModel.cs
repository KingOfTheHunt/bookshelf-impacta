using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Web.ViewModels.Account;

public class LoginAccountViewModel
{
    [Required(ErrorMessage = "O login é obrigatório.")]
    public string Login { get; set; }
    [Required(ErrorMessage = "A senha é obrigatória.")]
    public string Password { get; set; }
}
