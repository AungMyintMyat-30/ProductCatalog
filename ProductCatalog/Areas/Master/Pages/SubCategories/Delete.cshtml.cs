using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductCatalog.Repositories;
using ProductCatalogCore.Entities;
using ProductCatalogCore.Interfaces;

namespace ProductCatalog.Areas.Master.Pages.SubCategories
{
    public class DeleteModel : PageModel
    {
        private readonly SubCategoryRepo? _repository;
        private readonly ICryptography _cryptography;

        public DeleteModel(SubCategoryRepo repository, ICryptography cryptography)
        {
            _repository = repository;
            _cryptography = cryptography;
        }

        [BindProperty]
        public ViSubCategory? ViSubCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            ViSubCategory = await _repository!.GetSubCategoryBySubId(_cryptography.DecryptAES(id));

            if (ViSubCategory == null)
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

            SubCategory subcategory = new();
            subcategory!.SubId = ViSubCategory!.SubId;
            subcategory!.DeletedDate = DateTime.Now;
            subcategory.DeletedUser = "Admin Two";

            await _repository!.DeleteSubCategory(subcategory);

            return RedirectToPage("./Index");
        }
    }
}
