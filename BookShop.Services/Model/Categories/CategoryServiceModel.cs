using BookShop.Common.Mapping;
using BookShop.Data.Models;

namespace BookShop.Services.Model.Categories
{
    public class CategoryServiceModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
