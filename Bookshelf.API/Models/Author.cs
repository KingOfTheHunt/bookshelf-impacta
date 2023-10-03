using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Bookshelf.API.Models;

[Table("Author")]
public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    public IList<Book> Books { get; set; }
}
