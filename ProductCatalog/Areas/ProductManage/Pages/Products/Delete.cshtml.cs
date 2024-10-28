using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductCatalog.Repositories;
using ProductCatalogCore.Entities;
using ProductCatalogCore.Interfaces;

namespace ProductCatalog.Areas.ProductManage.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly ProductRepo? _repository;
        private readonly ICryptography _cryptography;
        public DeleteModel(ProductRepo repository, ICryptography cryptography)
        {
            _repository = repository;
            _cryptography = cryptography;
        }

        [BindProperty]
        public ViProduct? ViProduct { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            ViProduct = await _repository!.GetProductById(_cryptography.DecryptAES(id));

            if (ViProduct == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Product product = new();
            product!.ProductId = ViProduct!.ProductId;
            product!.DeletedDate = DateTime.Now;
            product.DeletedUser = "Admin Two";
            await _repository!.DeleteProduct(product);

            return RedirectToPage("./Index");
        }
    }
}
