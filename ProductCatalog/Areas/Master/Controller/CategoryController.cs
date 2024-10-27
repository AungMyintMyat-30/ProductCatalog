using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using ProductCatalog.Model;
using ProductCatalog.Repositories;
using ProductCatalogCore.Entities;

namespace ProductCatalog.Areas.Master.Controller
{
    [Route("api/master/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepo _repository;

        public CategoryController(CategoryRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<Category>> GetCategoriesAsync()
        {
            List<Category> category = await _repository.GetAllcategory();
            return Ok(new APIRequestModel()
            {
                Meta = new { total_count = category.Count },
                Data = category,
                Errors = null,
                Links = null
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategory>> CheckCategoriesAsync(int id)
        {
            List<SubCategory> category = await _repository.CheckCategory(id);
            return Ok(new APIRequestModel()
            {
                Meta = new { total_count = category.Count },
                Data = category,
                Errors = null,
                Links = null
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddCategoryAsync(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            category.CreatedDate = DateTime.Now;
            await _repository.AddCategory(category); // Ensure AddCategoryAsync method is async

            return Ok(new APIRequestModel()
            {
                Meta = "Category added successfully",
                Data = category,
                Errors = null,
                Links = null
            });
        }
    }
}
