using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Model;
using ProductCatalog.Repositories;
using ProductCatalogCore.Entities;

namespace ProductCatalog.Areas.Master.Controller
{
    [Route("api/master/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandRepo _repository;

        public BrandController(BrandRepo repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves all brands from the database.
        /// </summary>
        /// <returns>An API response containing the list of brands.</returns>
        [HttpGet]
        public async Task<ActionResult<APIRequestModel>> GetAllBrands()
        {
            try
            {
                List<Brand> brand = await _repository.GetAllBrands();
                return Ok(new APIRequestModel()
                {
                    Meta = new { total_count = brand.Count },
                    Data = brand,
                    Errors = null,
                    Links = null
                });
            }
            catch(Exception ex)
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
        /// Checks if a brand with the specified ID is associated with any product.
        /// </summary>
        /// <param name="id">The brand ID to check.</param>
        /// <returns>An API response with the product if found, or an error message.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<APIRequestModel>> CheckBrandHasProduct(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest(new APIRequestModel
                    {
                        Data = null,
                        Errors = new[] { "Brand ID cannot be null or empty." },
                        Links = null
                    });
                }

                Product? product = await _repository.CheckBrandById(id);

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
    }
}
