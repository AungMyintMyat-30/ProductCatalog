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

        [HttpGet]
        public async Task<ActionResult<Brand>> GetBrandAsync()
        {
            List<Brand> brand = await _repository.GetAllBrand();
            return Ok(new APIRequestModel()
            {
                Meta = new { total_count = brand.Count },
                Data = brand,
                Errors = null,
                Links = null
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> CheckBrandByIdAsync(string id)
        {
            Product product = await _repository.CheckBrandById(id);
            return Ok(new APIRequestModel()
            {
                Data = product,
                Errors = null,
                Links = null
            });
        }
    }
}
