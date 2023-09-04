using Bookshelf.API.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshelf.API.Models;

[Table("Reading")]
public class Reading
{
    public int Id { get; set; }
    public Book Book { get; set; }
    public Reader Reader { get; set; }
    public short PagesRead { get; set; }
    public EReadingStatus ReadingStatus { get; set; }
    public short Rate { get; set; }
    public string Review { get; set; }
}
