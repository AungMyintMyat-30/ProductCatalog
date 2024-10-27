using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductCatalog.Repositories;
using ProductCatalogCore.Entities;

namespace ProductCatalog.Areas.Master.Pages.Brands
{
    public class CreateModel : PageModel
    {
        private readonly BrandRepo? _repository;

        public CreateModel(BrandRepo repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public Brand? Brand { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Brand!.CreatedDate = DateTime.Now;
            Brand.CreatedUser = "Admin";
            await _repository!.AddBrand(Brand);

            return RedirectToPage("./Index");
        }
    }
}
