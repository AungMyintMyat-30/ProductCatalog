using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Protocol.Core.Types;
using ProductCatalog.Repositories;
using ProductCatalogCore.Entities;

namespace ProductCatalog.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ProductRepo? _repository;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ProductRepo repository, ILogger<IndexModel> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public List<ViProduct>? Product { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Product = await _repository!.GetAllProduct();

            return Page();
        }
    }
}
