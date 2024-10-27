using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductCatalog.Repositories;

namespace ProductCatalog.Areas.Master.Pages.Category
{
    public class CreateModel : PageModel
    {
        private readonly CategoryRepo? _repository;

        public CreateModel(CategoryRepo repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public ProductCatalogCore.Entities.Category? Category { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Category!.CreatedDate = DateTime.Now;
            Category.CreatedUser = "Admin";
            await _repository!.AddCategory(Category);

            return RedirectToPage("./Index");
        }
    }
}
