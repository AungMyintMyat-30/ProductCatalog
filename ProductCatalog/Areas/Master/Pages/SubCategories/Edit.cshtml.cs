using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductCatalog.Repositories;
using ProductCatalogCore.Entities;
using ProductCatalogCore.Interfaces;

namespace ProductCatalog.Areas.Master.Pages.SubCategories
{
    public class EditModel : PageModel
    {
        private readonly SubCategoryRepo? _repository;
        private readonly ICryptography _cryptography;

        public EditModel(SubCategoryRepo repository, ICryptography cryptography)
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

            SubCategory subCategory = new();
            subCategory!.SubId = ViSubCategory!.SubId;
            subCategory!.SubName =ViSubCategory!.SubName;
            subCategory!.CatId = ViSubCategory!.CatId;
            subCategory!.UpdatedDate = DateTime.Now;
            subCategory.UpdatedUser = "Admin One";

            await _repository!.UpdateSubCategory(subCategory);

            return RedirectToPage("./Index");
        }

    }
}
