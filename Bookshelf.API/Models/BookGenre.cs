using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshelf.API.Models;

[Table("BookGenre")]
public class BookGenre
{
    public int BookId { get; set; }
    public int GenreId { get; set; }
}
