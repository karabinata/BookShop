using BookShop.Api.Infrastructure.Extentions;
using BookShop.Api.Infrastructure.Filters;
using BookShop.Api.Models.Books;
using BookShop.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static BookShop.Api.WebConstants;

namespace BookShop.Api.Controllers
{
    public class BooksController : BaseController
    {
        private readonly IBookService books;
        private readonly IAuthorService authors;

        public BooksController(IBookService books, IAuthorService authors)
        {
            this.books = books;
            this.authors = authors;
        }

        [HttpGet(WithId)]
        public async Task<IActionResult> Get(int id)
            => this.OkOrNotFound(await this.books.DetailsAsync(id));

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string search = "")
            => this.OkOrNotFound(await this.books.AllAsync(search));

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody]CreateBookRequestModel model)
        {
            if (!await this.authors.Exists(model.AuthorId))
            {
                return BadRequest("Author does not exists.");
            }

            var id = await this.books
                .CreateAsync(
                    model.Title,
                    model.Description,
                    model.Price,
                    model.Copies,
                    model.ReleaseDate,
                    model.AuthorId,
                    model.Edition,
                    model.AgeRestriction,
                    model.Categories
                );

            return Ok(id); 
        }

        [HttpPut(WithId)]
        public async Task<IActionResult> Put(int id, [FromBody]EditBookRequestModel model)
            => this.OkOrNotFound(await this.books.UpdateAsync(
                id,
                model.Title,
                model.Description,
                model.Price,
                model.Copies,
                model.ReleaseDate,
                model.AuthorId,
                model.Edition,
                model.AgeRestriction));

        [HttpDelete(WithId)]
        public IActionResult Delete(int id)
        {
            this.books.Delete(id);

            return Ok();
        }
    }
}
