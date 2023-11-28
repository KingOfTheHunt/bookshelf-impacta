using Bookshelf.API.Enums;

namespace Bookshelf.API.ViewModels.Reading;

public class UpdateReadingViewModel
{
    public int Id { get; set; }
    public short PagesRead { get; set; }
    public EReadingStatus ReadingStatus { get; set; }
    public short Rate { get; set; }
    public string Review { get; set; }
}
