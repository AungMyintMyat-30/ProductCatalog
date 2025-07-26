using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductCatalog.Repositories;
using ProductCatalogCore.Entities;

namespace ProductCatalog.Areas.Master.Pages.SubCategories
{
    public class CreateModel : PageModel
    {
        private readonly SubCategoryRepo? _repository;

        public CreateModel(SubCategoryRepo repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public SubCategory? SubCategory { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            SubCategory!.CreatedDate = DateTime.Now;
            SubCategory.CreatedUser = "Admin";

            await _repository!.AddSubCategory(SubCategory);

            return RedirectToPage("./Index");
        }
    }
}
