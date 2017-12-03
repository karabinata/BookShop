using System.Threading.Tasks;
using BookShop.Services.Model.Books;
using BookShop.Data;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using BookShop.Data.Models;
using BookShop.Common.Extentions;

namespace BookShop.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly BookShopDbContext db;

        public BookService(BookShopDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<BookListingServiceModel>> AllAsync(string searchText)
            => await this.db
                .Books
                .Where(b => b.Title.ToLower().Contains(searchText.ToLower()))
                .OrderBy(b => b.Title)
                .Take(10)
                .ProjectTo<BookListingServiceModel>()
                .ToListAsync();

        public async Task<int> CreateAsync(
            string title, 
            string description, 
            decimal price, 
            int copies, 
            DateTime releaseDate, 
            int authorId, 
            int? edition, 
            int? ageRestriction, 
            string categories)
        {
            var categoryNames = categories.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToHashSet();

            var existingCategories = await this.db
                .Categories
                .Where(c => categoryNames.Contains(c.Name))
                .ToListAsync();

            var allCategories = new List<Category>(existingCategories);

            foreach (var categoryName in categoryNames)
            {
                if (existingCategories.All(c => c.Name != categoryName))
                {
                    var category = new Category
                    {
                        Name = categoryName
                    };

                    this.db.Add(category);

                    allCategories.Add(category);
                }
            }

            await this.db.SaveChangesAsync();

            var book = new Book
            {
                Title = title,
                Description = description,
                Price = price,
                Copies = copies,
                ReleaseDate = releaseDate,
                AuthorId = authorId,
                Edition = edition,
                AgeRestriction = ageRestriction
            };

            allCategories.ForEach(c => book.Categories.Add(new CategoryBook
            {
                CategoryId = c.Id
            }));

            this.db.Add(book);
            await this.db.SaveChangesAsync();

            return book.Id;
        }

        public void Delete(int bookId)
        {
            var book = this.db.Books.Find(bookId);

            if (book == null)
            {
                return;
            }

            this.db.Remove(book);

            db.SaveChanges();
        }

        public async Task<BookDetailsServiceModel> DetailsAsync(int bookId)
            => await this.db
                .Books
                .Where(b => b.Id == bookId)
                .ProjectTo<BookDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<int> UpdateAsync(
            int bookId,
            string title, 
            string description, 
            decimal price, 
            int copies, 
            DateTime releaseDate, 
            int authorId, 
            int? edition, 
            int? ageRestriction)
        {
            var book = await this.db
                .Books
                .FindAsync(bookId);

            if (book == null)
            {
                return 0;
            }

            book.Title = title;
            book.Description = description;
            book.Price = price;
            book.Copies = copies;
            book.ReleaseDate = releaseDate;
            book.AuthorId = authorId;
            book.Edition = edition;
            book.AgeRestriction = ageRestriction;

            await this.db.SaveChangesAsync();

            return bookId;
        }
    }
}
