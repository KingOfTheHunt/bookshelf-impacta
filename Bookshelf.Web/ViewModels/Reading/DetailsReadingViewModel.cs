namespace Bookshelf.Web.ViewModels.Reading;

public class DetailsReadingViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public short Pages { get; set; }
    public short PagesRead { get; set; }
    public short ReadingStatus { get; set; }
    public short Rate { get; set; }
    public string Review { get; set; }
}
