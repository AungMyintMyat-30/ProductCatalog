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

        /// <summary>
        /// Retrieves all categories from the database.
        /// </summary>
        /// <returns>An API response containing the list of categories.</returns>
        [HttpGet]
        public async Task<ActionResult<APIRequestModel>> GetCategories()
        {
            try
            {
                List<Category> category = await _repository.GetAllCategories();
                return Ok(new APIRequestModel()
                {
                    Meta = new { total_count = category.Count },
                    Data = category,
                    Errors = null,
                    Links = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIRequestModel
                {
                    Data = null,
                    Errors = new[] { ex.Message },
                    Links = null
                });
            }
        }

        /// <summary>
        /// Checks if a category with the specified ID is associated with any category.
        /// </summary>
        /// <param name="id">The category ID to check.</param>
        /// <returns>An API response with the category if found, or an error message.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<APIRequestModel>> CheckCategoryHasSubcategory(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest(new APIRequestModel
                    {
                        Data = null,
                        Errors = new[] { "Category ID cannot be null or empty." },
                        Links = null
                    });
                }

                SubCategory? subCategory = await _repository.CheckCategoryById(id);

                return Ok(new APIRequestModel()
                {
                    Data = subCategory,
                    Errors = null,
                    Links = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIRequestModel
                {
                    Data = null,
                    Errors = new[] { ex.Message },
                    Links = null
                });
            }
        }
    }
}
