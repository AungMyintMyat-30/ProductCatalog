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

        [HttpGet]
        public async Task<ActionResult<ViSubCategory>> GetCategoriesAsync()
        {
            List<ViSubCategory> subcategory = await _repository.GetAllSubCategory();
            return Ok(new APIRequestModel()
            {
                Meta = new { total_count = subcategory.Count },
                Data = subcategory,
                Errors = null,
                Links = null
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> CheckSubCategoryByIdAsync(string id)
        {
            Product product = await _repository.CheckSubCategoryById(id);
            return Ok(new APIRequestModel()
            {
                Data = product,
                Errors = null,
                Links = null
            });
        }
    }
}
