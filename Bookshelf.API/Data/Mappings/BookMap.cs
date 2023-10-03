using Bookshelf.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookshelf.API.Data.Mappings;

public class BookMap : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        // Configurando a relação N-N
        builder.HasMany(x => x.Authors)
            .WithMany(x => x.Books)
            .UsingEntity<BookAuthor>();

        builder.HasMany(x => x.Genres)
            .WithMany(x => x.Books)
            .UsingEntity<BookGenre>();
    }
}
