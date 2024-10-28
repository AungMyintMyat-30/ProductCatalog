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
        public async Task<ActionResult<ViSubCategory>> GetSubCategoriesAsync()
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

        [HttpGet("check-subcategory/{id}")]
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

        [HttpGet("by-category/{id}")]
        public async Task<ActionResult<ViSubCategory>> GetSubCategoryByCatIdAsync(string id)
        {
            List<ViSubCategory> subCat = await _repository.GetSubCategoryByCatId(id);
            return Ok(new APIRequestModel()
            {
                Data = subCat,
                Errors = null,
                Links = null
            });
        }
    }
}
