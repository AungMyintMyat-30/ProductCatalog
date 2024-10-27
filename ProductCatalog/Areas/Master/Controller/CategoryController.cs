using Microsoft.AspNetCore.Mvc;
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
            List<Category> category = await _repository.GetAllCategory();
            return Ok(new APIRequestModel()
            {
                Meta = new { total_count = category.Count },
                Data = category,
                Errors = null,
                Links = null
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategory>> CheckCategoryByIdAsync(string id)
        {
            SubCategory category = await _repository.CheckCategoryById(id);
            return Ok(new APIRequestModel()
            {
                Data = category,
                Errors = null,
                Links = null
            });
        }
    }
}
