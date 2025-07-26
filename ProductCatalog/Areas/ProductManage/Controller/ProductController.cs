using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Model;
using ProductCatalog.Repositories;
using ProductCatalogCore.Entities;

namespace ProductCatalog.Areas.ProductManage.Controller
{
    [Route("api/productmanage/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepo _repository;

        public ProductController(ProductRepo repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves all products from the database.
        /// </summary>
        /// <returns>An API response containing the list of products.</returns>
        [HttpGet]
        public async Task<ActionResult<APIRequestModel>> GetAllProducts()
        {
            try
            {
                List<ViProduct> product = await _repository.GetAllProducts();
                return Ok(new APIRequestModel()
                {
                    Meta = new { total_count = product.Count },
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
