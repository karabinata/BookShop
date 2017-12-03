using BookShop.Services.Model;
using BookShop.Services.Model.Authors;
using BookShop.Services.Model.Books;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShop.Services
{
    public interface IAuthorService
    {
        Task<AuthorDetailsServiceModel> DetailsAsync(int id);

        Task<int> CreateAsync(string firsname, string lastname);

        Task<bool> Exists(int id);

        Task<IEnumerable<BookWithCategoriesServiceModel>> Books(int authorId);
    }
}
