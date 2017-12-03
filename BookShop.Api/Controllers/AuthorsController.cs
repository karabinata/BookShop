using BookShop.Api.Infrastructure.Extentions;
using BookShop.Api.Infrastructure.Filters;
using BookShop.Api.Models.Authors;
using BookShop.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static BookShop.Api.WebConstants;

namespace BookShop.Api.Controllers
{
    public class AuthorsController : BaseController
    {
        private readonly IAuthorService authors;

        public AuthorsController(IAuthorService authors)
        {
            this.authors = authors;
        }

        [HttpGet(WithId)]
        public async Task<IActionResult> Get(int id)
            => this.OkOrNotFound(await this.authors.DetailsAsync(id));

        [HttpGet(WithId + "/books")]
        public async Task<IActionResult> GetBooks(int id)
            => Ok(await this.authors.Books(id));

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody]AuthorRequestModel model)
        {
            var id = await this.authors
                    .CreateAsync(model.Firstname, model.Lastname);

            return Ok(id);
        }
    }
}
