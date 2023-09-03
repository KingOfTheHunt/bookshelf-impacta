using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshelf.API.Models;

[Table("Genre")]
public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }
}
