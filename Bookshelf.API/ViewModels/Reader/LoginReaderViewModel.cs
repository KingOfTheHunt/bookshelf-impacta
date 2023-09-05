using System.ComponentModel.DataAnnotations;

namespace Bookshelf.API.ViewModels.Reader
{
    public class LoginReaderViewModel
    {
        [Required(ErrorMessage = "O login é obrigatório.")]
        public string Login { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Password { get; set; }
    }
}
