using Bookshelf.API.Data.Mappings;
using Bookshelf.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookshelf.API.Data;

public class BookshelfDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    // public DbSet<BookAuthor> BookAuthors { get; set; }
    // public DbSet<BookGenre> BookGenres { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Reader> Readers { get; set; }
    public DbSet<Reading> Readings { get; set; }

    public BookshelfDbContext(DbContextOptions<BookshelfDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookMap());
    }
}
