﻿using Bookshelf.API.Data;
using Bookshelf.API.Models;
using Bookshelf.API.ViewModels.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Bookshelf.API.Repositories;

public class BookRepository
{
    public async Task<IEnumerable<BookSearchViewModel>> GetBooksAsync([FromServices] BookshelfDbContext context,
        string query)
    {
        using var connection = new SqlConnection(context.Database.GetConnectionString());

        string sql = @"SELECT [Book].[Id], [Book].[Title], [Book].[Subtitle],
                [Author].[Name], [Genre].[Name] FROM [Book]
                INNER JOIN [BookAuthor] ON [BookAuthor].[BookId] = [Book].[Id]
                INNER JOIN [Author] ON [BookAuthor].[AuthorId] = [Author].[Id]
                INNER JOIN [BookGenre] ON [BookGenre].[BookId] = [Book].[Id]
                INNER JOIN [Genre] ON [BookGenre].[GenreId] = [Genre].[Id]
                WHERE [Book].[Title] LIKE @likeQuery OR [Book].[ISBN] = @query
                OR [Author].[Name] LIKE @likeQuery";

        var searchedBooks = new List<BookSearchViewModel>();

        var result = await connection.QueryAsync<Book, Author, Genre, BookSearchViewModel>(
            sql, (book, author, genre) =>
            {
                var searchedBook = searchedBooks.FirstOrDefault(x => x.Id == book.Id);

                if (searchedBook == null)
                {
                    searchedBook = new()
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Subtitle = book.Subtitle,
                    };
                    searchedBook.Authors.Add(author.Name);
                    searchedBook.Genres.Add(genre.Name);

                    searchedBooks.Add(searchedBook);
                }
                else
                {
                    if (searchedBook.Authors.Any(x => x == author.Name) == false)
                        searchedBook.Authors.Add(author.Name);

                    searchedBook.Genres.Add(genre.Name);
                }

                return searchedBook;
            },
            new { LikeQuery = $"%{query}%", query },
            splitOn: "Name,Name");

        return searchedBooks;
    }

    public async Task<BookDetailsViewModel> GetBookDetailsAsync([FromServices] BookshelfDbContext context,
        int id)
    {
        var connection = new SqlConnection(context.Database.GetConnectionString());

        var sql = @"SELECT [Book].[Id], [Book].[Title], [Book].[Subtitle], [Book].[ISBN], 
                    [Book].[Pages], [Book].[PublishingCompany], [Book].[Synopsis],
                    [Author].[Name], [Genre].[Name] FROM [Book]
                    INNER JOIN [BookAuthor] ON [BookAuthor].[BookId] = [Book].[Id]
                    INNER JOIN [BookGenre] ON [BookGenre].[BookId] = [Book].[Id]
                    INNER JOIN [Author] ON [Author].[Id] = [BookAuthor].[AuthorId]
                    INNER JOIN [Genre] as [Genre] ON [Genre].[Id] = [BookGenre].[GenreId]
                    WHERE [Book].[Id] = @id";

        var books = new List<BookDetailsViewModel>();

        var rows = await connection.QueryAsync<Book, Author, Genre, BookDetailsViewModel>(sql,
            (book, author, genre) =>
            {
                var b = books.FirstOrDefault(x => x.Id == book.Id);

                if (b == null)
                {
                    b = new()
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Subtitle = book.Subtitle,
                        ISBN = book.ISBN,
                        Pages = book.Pages,
                        PublishingCompany = book.PublishingCompany,
                        Synopsis = book.Synopsis
                    };

                    b.Authors.Add(author.Name);
                    b.Genres.Add(genre.Name);

                    books.Add(b);
                }
                else
                {
                    if (b.Authors.Contains(author.Name) == false)
                        b.Authors.Add(author.Name);

                    b.Genres.Add(genre.Name);

                    books.Add(b);
                }

                return b;
            }
            , new { id },
            splitOn: "Name,Name");

        if (books.Count > 0)
        {
            var book = books.FirstOrDefault();
            book.Rate = (int) await context.Readings.Where(x => x.BookId == book.Id).AverageAsync(x => x.Rate);
        }
        return books.FirstOrDefault();
    }
}
