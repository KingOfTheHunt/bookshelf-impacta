using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshelf.API.Models;

[Table("Book")]
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Subtitle { get; set; }
    public short Pages { get; set; }
    public string Synopsis { get; set; }
    public string PublishingCompany { get; set; }
    public string ISBN { get; set; }
    public IList<Author> Authors { get; set; }
    public IList<Genre> Genres { get; set; }
}
