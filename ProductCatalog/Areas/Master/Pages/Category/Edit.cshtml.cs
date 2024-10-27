using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductCatalog.Repositories;
using ProductCatalogCore.Interfaces;

namespace ProductCatalog.Areas.Master.Pages.Category
{
    public class EditModel : PageModel
    {
        private readonly CategoryRepo? _repository;
        private readonly ICryptography _cryptography;
        public EditModel(CategoryRepo repository, ICryptography cryptography)
        {
            _repository = repository;
            _cryptography = cryptography;
        }

        [BindProperty]
        public ProductCatalogCore.Entities.Category? Category { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            Category = await _repository!.GetCategoryById(_cryptography.DecryptAES(id));

            if (Category == null)
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

            Category!.UpdatedDate = DateTime.Now;
            Category.UpdatedUser = "Admin One";
            await _repository!.UpdateCategory(Category);

            return RedirectToPage("./Index");
        }
    }
}
