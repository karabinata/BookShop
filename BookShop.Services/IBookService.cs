using BookShop.Services.Model.Books;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShop.Services
{
    public interface IBookService
    {
        Task<int> CreateAsync(
            string title,
            string description,
            decimal price,
            int copies,
            DateTime releaseDate,
            int authorId,
            int? edition,
            int? ageRestriction,
            string categories);

        Task<int?> UpdateAsync(
            int bookId,
            string title,
            string description,
            decimal price,
            int copies,
            DateTime releaseDate,
            int authorId,
            int? edition,
            int? ageRestriction);

        Task<IEnumerable<BookListingServiceModel>> AllAsync(string searchText);

        Task<BookDetailsServiceModel> DetailsAsync(int bookId);

        void Delete(int bookId);
    }
}
