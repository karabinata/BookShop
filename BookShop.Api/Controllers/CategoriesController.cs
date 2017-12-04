using BookShop.Api.Infrastructure.Extentions;
using BookShop.Api.Models.Categories;
using BookShop.Services;
using BookShop.Services.Model.Categories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using static BookShop.Api.WebConstants;

namespace BookShop.Api.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService categories;

        public CategoriesController(ICategoryService categories)
        {
            this.categories = categories;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await this.categories.AllAsync());

        [HttpGet(WithId)]
        public async Task<IActionResult> Get(int id)
            => this.OkOrNotFound(await this.categories.ByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryServiceModel model)
        {
            var exists = await this.categories.CategoryExistsAsync(model.Name);

            if (exists)
            {
                return BadRequest("Category already exists.");
            }

            return Ok(await this.categories.CreateAsync(model.Name));
        }

        [HttpPut(WithId)]
        public async Task<IActionResult> Put(int id, [FromBody]EditCategoryRequestModel model)
        {
            var exists = await this.categories.CategoryExistsAsync(model.Name);

            if (exists)
            {
                return BadRequest("Category already exists.");
            }

            return this.OkOrNotFound(await this.categories.UpdateAsync(id, model.Name));
        }

        [HttpDelete(WithId)]
        public IActionResult Delete(int id)
        {
            this.categories.Delete(id);

            return Ok(id);
        }

    }
            
}
