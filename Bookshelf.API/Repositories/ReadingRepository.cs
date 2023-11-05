using Bookshelf.API.Data;
using Bookshelf.API.Enums;
using Bookshelf.API.Models;
using Bookshelf.API.ViewModels.Reading;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Bookshelf.API.Repositories;

public class ReadingRepository
{
    public async Task<Reading> AddReadingAsync([FromServices] BookshelfDbContext context, 
        CreateReadingViewModel viewModel, int readerId)
    {
        var reading = new Reading
        {
            BookId = viewModel.BookId,
            ReaderId = readerId,
            PagesRead = 0,
            Rate = 0,
            ReadingStatus = EReadingStatus.NotRead
        };

        await context.Readings.AddAsync(reading);
        await context.SaveChangesAsync();

        return reading;
    }

    public async Task<IEnumerable<ShowReadingsViewModel>> GetReadingsAsync([FromServices] BookshelfDbContext context,
        int readerId)
    {
        var readings = new List<ShowReadingsViewModel>();

        using var connection = new SqlConnection(context.Database.GetConnectionString());
        var sql = @"SELECT [Reading].[Id], [Reading].[PagesRead], [Book].[Title], [Book].[Pages] 
                    FROM [Reading]
                    INNER JOIN [Book] ON [Book].[Id] = [Reading].[BookId]
                    INNER JOIN [Reader] ON [Reader].[Id] = [Reading].[ReaderId]
                    WHERE [Reader].[Id] = @id";

        var rows = await connection.QueryAsync<Reading, Book, ShowReadingsViewModel>(sql,
            (r, b) =>
            {
                var reading = readings.FirstOrDefault(x => x.Id == r.Id);

                if (reading == null)
                {
                    reading = new ShowReadingsViewModel
                    {
                        Id = r.Id,
                        PagesRead = r.PagesRead,
                        Title = b.Title,
                        Pages = b.Pages
                    };

                    readings.Add(reading);
                }

                return reading;
            }, new { id = readerId }, splitOn: "Title");

        return readings;
    }

    public async Task<ReadingDetailsViewModel> GetReadingAsync([FromServices] BookshelfDbContext context,
        int readingId)
    {
        var reading = await context.Readings
            .Include(x => x.Book)
            .Where(x => x.Id == readingId)
            .Select<Reading, ReadingDetailsViewModel>(x => new ReadingDetailsViewModel
            {
                Id = x.Id,
                Title = x.Book.Title,
                Pages = x.Book.Pages,
                PagesRead = x.PagesRead,
                ReadingStatus = x.ReadingStatus
            }).FirstOrDefaultAsync();

        return reading;
    }

    public async Task<bool> UpdateReadingAsync([FromServices] BookshelfDbContext context,
        UpdateReadingViewModel viewModel)
    {
        var reading = await context.Readings.FirstOrDefaultAsync(x => x.Id == viewModel.Id);

        if (reading == null) return false;

        reading.PagesRead = viewModel.PagesRead;
        reading.ReadingStatus = viewModel.ReadingStatus;
        context.Readings.Update(reading);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteReadingAsync([FromServices] BookshelfDbContext context, 
        int readingId)
    {
        var reading = await context.Readings.FirstOrDefaultAsync(x => x.Id == readingId);

        if (reading == null) return false;

        context.Readings.Remove(reading);
        await context.SaveChangesAsync();

        return true;
    }
}
