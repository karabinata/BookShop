﻿using AutoMapper.QueryableExtensions;
using BookShop.Data;
using BookShop.Data.Models;
using BookShop.Services.Model.Authors;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BookShop.Services.Model.Books;

namespace BookShop.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly BookShopDbContext db;

        public AuthorService(BookShopDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<BookWithCategoriesServiceModel>> Books(int authorId)
            => await this.db
                .Books
                .Where(b => b.AuthorId == authorId)
                .ProjectTo<BookWithCategoriesServiceModel>()
                .ToListAsync();

        public async Task<int> CreateAsync(string firsName, string lastName)
        {
            var author = new Author
            {
                FirstName = firsName,
                LastName = lastName
            };

            this.db.Add(author);
            await this.db.SaveChangesAsync();

            return author.Id;
        }

        public async Task<AuthorDetailsServiceModel> DetailsAsync(int id)
            => await this.db
                .Authors
                .Where(a => a.Id == id)
                .ProjectTo<AuthorDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<bool> Exists(int id)
            => await this.db
                .Authors
                .AnyAsync(a => a.Id == id);
    }
}
