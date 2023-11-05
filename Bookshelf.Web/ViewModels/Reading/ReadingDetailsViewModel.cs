namespace Bookshelf.Web.ViewModels.Reading;

public class ReadingDetailsViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public short Pages { get; set; }
    public short PagesRead { get; set; }
    public short ReadingStatus { get; set; }
}
