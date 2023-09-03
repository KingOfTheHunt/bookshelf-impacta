using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshelf.API.Models;

[Table("Author")]
public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
}
