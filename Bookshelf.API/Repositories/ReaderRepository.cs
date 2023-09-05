﻿using Bookshelf.API.Data;
using Bookshelf.API.Models;
using Bookshelf.API.ViewModels.Reader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookshelf.API.Repositories
{
    public class ReaderRepository
    {
        public async Task<Reader> InsertAsync([FromServices] BookshelfDbContext context,
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

        public async Task<Reader> GetReaderAsync([FromServices] BookshelfDbContext context,
            LoginReaderViewModel readerViewModel)
        {
            var reader = await context.Readers.FirstOrDefaultAsync(x => 
                (x.Email == readerViewModel.Login || x.UserName == readerViewModel.Login) && 
                    x.Password == readerViewModel.Password);

            return reader;
        }
    }
}
