using Bookshelf.API.Data;
using Bookshelf.API.Models;
using Bookshelf.API.ViewModels.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Bookshelf.API.Repositories;

public class BookRepository
{
    public async Task<List<BookSearchViewModel>> GetBooksAsync([FromServices] BookshelfDbContext context, string query)
    {
        return await context.Books.Include(x => x.Authors)
            .Include(x => x.Genres)
            .AsSplitQuery()
            .Where(x => x.Title.Contains(query) || x.ISBN == query)
            .Select<Book, BookSearchViewModel>(x => new BookSearchViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Subtitle = x.Subtitle,
                Authors = x.Authors.Select(y => y.Name).ToList(),
                Genres = x.Genres.Select(y => y.Name).ToList()
            })
            .AsNoTracking()
            .ToListAsync();
    }
}
