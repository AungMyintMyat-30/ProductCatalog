using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductCatalog.Repositories;
using ProductCatalogCore.Entities;
using ProductCatalogCore.Interfaces;

namespace ProductCatalog.Areas.Master.Pages.Brands
{
    public class EditModel : PageModel
    {
        private readonly BrandRepo? _repository;
        private readonly ICryptography _cryptography;
        public EditModel(BrandRepo repository, ICryptography cryptography)
        {
            _repository = repository;
            _cryptography = cryptography;
        }

        [BindProperty]
        public Brand? Brand { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            Brand = await _repository!.GetBrandById(_cryptography.DecryptAES(id));

            if (Brand == null)
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

            Brand!.UpdatedDate = DateTime.Now;
            Brand.UpdatedUser = "Admin One";
            await _repository!.UpdateBrand(Brand);

            return RedirectToPage("./Index");
        }
    }
}
