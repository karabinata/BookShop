using BookShop.Services.Model.Authors;

namespace BookShop.Services
{
    public interface IAuthorService
    {
        AuthorDetailsServiceModel Details(int id); 
    }
}
