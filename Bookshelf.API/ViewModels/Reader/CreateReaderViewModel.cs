using System.ComponentModel.DataAnnotations;

namespace Bookshelf.API.ViewModels.Reader;

public class CreateReaderViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "A senha é obrigatória.")]
    public string Password { get; set; }
}
