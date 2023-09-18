using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Web.ViewModels.Account
{
    public class ChangePassowordAccountViewModel
    {
        public string UserName { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória")]
        public string NewPassword { get; set; }
    }
}
