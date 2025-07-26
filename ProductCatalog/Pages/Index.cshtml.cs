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

        public IndexModel(ProductRepo repository)
        {
            _repository = repository;
        }

        public List<ViProduct>? Product { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Product = await _repository!.GetAllProducts();

            return Page();
        }
    }
}
