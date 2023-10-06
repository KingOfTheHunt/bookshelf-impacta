namespace Bookshelf.API.ViewModels.Book;

public class BookSearchViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Subtitle { get; set; }
    public List<string> Authors { get; set; }
    public List<string> Genres { get; set; }
}
