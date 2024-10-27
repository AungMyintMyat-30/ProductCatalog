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

        [HttpGet]
        public async Task<ActionResult<ViProduct>> GetCategoriesAsync()
        {
            List<ViProduct> product = await _repository.GetAllProduct();
            return Ok(new APIRequestModel()
            {
                Meta = new { total_count = product.Count },
                Data = product,
                Errors = null,
                Links = null
            });
        }
    }
}
