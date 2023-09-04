using Bookshelf.API.Data;
using Bookshelf.API.Models;
using Bookshelf.API.ViewModels.Reader;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.API.Repositories
{
    public class ReaderRepository
    {
        public async Task<Reader> Insert([FromServices] BookshelfDbContext context,
            CreateReaderViewModel readerViewModel)
        {
            var reader = new Reader
            {
                Name = readerViewModel.Name,
                UserName = readerViewModel.UserName,
                Email = readerViewModel.Email,
                Password = readerViewModel.Password
            };
            reader.Image = $"https://api.multiavatar.com/{reader.UserName}.png";

            context.Readers.Add(reader);
            await context.SaveChangesAsync();

            return reader;
        }
    }
}
