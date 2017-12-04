using BookShop.Services.Model.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShop.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryServiceModel>> AllAsync();

        Task<CategoryServiceModel> ByIdAsync(int categoryId);

        Task<int?> UpdateAsync(int categoryId, string name);

        Task<int> CreateAsync(string name);

        void Delete(int categoryId);

        Task<bool> CategoryExistsAsync(string name);
    }
}
