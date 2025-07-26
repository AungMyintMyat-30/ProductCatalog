using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Model;
using ProductCatalog.Repositories;
using ProductCatalogCore.Entities;

namespace ProductCatalog.Areas.Master.Controller
{
    [Route("api/master/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly SubCategoryRepo _repository;

        public SubCategoryController(SubCategoryRepo repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves all subcategories from the database.
        /// </summary>
        /// <returns>An API response containing the list of subcategories.</returns>
        [HttpGet]
        public async Task<ActionResult<APIRequestModel>> GetSubCategories()
        {
            try
            {
                List<ViSubCategory> subcategory = await _repository.GetAllSubCategories();
                return Ok(new APIRequestModel()
                {
                    Meta = new { total_count = subcategory.Count },
                    Data = subcategory,
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
        /// Checks if a subcategory with the specified ID is associated with any subcategory.
        /// </summary>
        /// <param name="id">The subcategory ID to check.</param>
        /// <returns>An API response with the subcategory if found, or an error message.</returns>
        [HttpGet("check-subcategory/{id}")]
        public async Task<ActionResult<APIRequestModel>> CheckSubCategoryByIdAsync(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest(new APIRequestModel
                    {
                        Data = null,
                        Errors = new[] { "Subcategory ID cannot be null or empty." },
                        Links = null
                    });
                }

                Product? product = await _repository.CheckSubCategoryById(id);

                return Ok(new APIRequestModel()
                {
                    Data = product,
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
        /// Retrieves all subcategories associated with the specified category ID.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        /// <returns>An API response containing the list of matching subcategories.</returns>
        [HttpGet("by-category/{id}")]
        public async Task<ActionResult<APIRequestModel>> GetSubCategoryByCatIdAsync(string id)
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

                List<ViSubCategory> subCat = await _repository.GetSubCategoryByCatId(id);

                return Ok(new APIRequestModel()
                {
                    Data = subCat,
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
