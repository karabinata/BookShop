using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.Data;
using BookShop.Services.Model.Categories;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using BookShop.Data.Models;

namespace BookShop.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly BookShopDbContext db;

        public CategoryService(BookShopDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CategoryServiceModel>> AllAsync()
            => await this.db
                .Categories
                .OrderBy(c => c.Name)
                .ProjectTo<CategoryServiceModel>()
                .ToListAsync();

        public async Task<CategoryServiceModel> ByIdAsync(int categoryId)
        {
            var category = await this.db
                .Categories
                .Where(c => c.Id == categoryId)
                .ProjectTo<CategoryServiceModel>()
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return null;
            }

            return category;
        }

        public async Task<int> CreateAsync(string name)
        {
            var category = new Category
            {
                Name = name
            };

            this.db.Add(category);

            await this.db.SaveChangesAsync();

            return category.Id;
        }

        public void Delete(int categoryId)
        {
            var category = this.db.Categories.Find(categoryId);

            if (category == null)
            {
                return;
            }

            this.db.Remove(category);

            db.SaveChanges();
        }

        public async Task<int?> UpdateAsync(int categoryId, string name)
        {
            var category = await this.db.Categories.FindAsync(categoryId);

            if (category == null)
            {
                return null;
            }

            category.Name = name;

            await this.db.SaveChangesAsync();

            return categoryId;
        }

        public async Task<bool> CategoryExistsAsync(string name)
            => await this.db
                .Categories
                .AnyAsync(c => c.Name == name);
    }
}
