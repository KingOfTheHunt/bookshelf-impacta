using Bookshelf.API.Enums;
using Bookshelf.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookshelf.API.Data.Mappings;

public class ReadingMap : IEntityTypeConfiguration<Reading>
{
    public void Configure(EntityTypeBuilder<Reading> builder)
    {
        builder.Property(x => x.ReadingStatus)
            .HasConversion(x => x.ToString(), 
                x => (EReadingStatus)Enum.Parse(typeof(EReadingStatus), x));
    }
}
