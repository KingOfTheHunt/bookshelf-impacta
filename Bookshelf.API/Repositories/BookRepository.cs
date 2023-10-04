using Bookshelf.API.Data;
using Bookshelf.API.Models;
using Bookshelf.API.ViewModels.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Bookshelf.API.Repositories;

public class BookRepository
{
    public async Task<List<BookSearchViewModel>> GetBooksAsync([FromServices] BookshelfDbContext context)
    {
        return await context.Books.Include(x => x.Authors)
            .Include(x => x.Genres)
            .AsSplitQuery()
            .Where(x => x.Id == 2 || x.Id == 4 || x.Id == 6)
            .Select<Book, BookSearchViewModel>(x => new BookSearchViewModel
            {
                Title = x.Title,
                Subtitle = x.Subtitle,
                Authors = x.Authors.Select(y => y.Name).ToList(),
                Genres = x.Genres.Select(y => y.Name).ToList()
            })
            .AsNoTracking()
            .ToListAsync();
    }
}
