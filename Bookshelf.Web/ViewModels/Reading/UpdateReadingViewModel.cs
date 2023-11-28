namespace Bookshelf.Web.ViewModels.Reading;

public class UpdateReadingViewModel
{
    public int Id { get; set; }
    public short PagesRead { get; set; }
    public int ReadingStatus { get; set; }
    public short Rate { get; set; }
    public string Review { get; set; }
}
