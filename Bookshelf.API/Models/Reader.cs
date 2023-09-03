using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshelf.API.Models;

[Table("Reader")]
public class Reader
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Image { get; set; }
}
