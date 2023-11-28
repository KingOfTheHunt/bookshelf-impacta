namespace Bookshelf.Web.ViewModels.Book;

public class BookDetailsViewModel
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string? Subtitle { get; init; }
    public string ISBN { get; init; }
    public int Pages { get; init; }
    public string PublishingCompany { get; init; }
    public string Synopsis { get; init; }
    public List<string> Authors { get; init; } = new();
    public List<string> Genres { get; init; } = new();
    public int Rate { get; set; }
    public List<Review> Reviews { get; set; } = new();
}

public class Review
{
    public string Reader { get; set; }
    public string Content { get; set; }
}
