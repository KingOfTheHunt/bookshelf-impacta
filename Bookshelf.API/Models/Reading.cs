namespace Bookshelf.API.Models;

public class Reading
{
    public int Id { get; set; }
    public Book Book { get; set; }
    public Reader Reader { get; set; }
    public short PagesRead { get; set; }
    public int ReadingStatus { get; set; }
    public short Rate { get; set; }
    public string Review { get; set; }
}
