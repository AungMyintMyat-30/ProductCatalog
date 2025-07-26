using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductCatalog.Repositories;
using ProductCatalogCore.Entities;
using ProductCatalogCore.Interfaces;
using System.Text;

namespace ProductCatalog.Areas.ProductManage.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly ProductRepo? _repository;
        private readonly IImageService _imageService;
        private readonly IRandomizer _randomizer;
        private readonly ICryptography _cryptography;

        public EditModel(ProductRepo repository, 
                         IImageService imageService, 
                         IRandomizer randomizer, 
                         ICryptography cryptography)
        {
            _repository = repository;
            _imageService = imageService;
            _randomizer = randomizer;
            _cryptography = cryptography;
        }

        [BindProperty]
        public IFormFile? ImageFile { get; set; }
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
            product.ProductId = ViProduct!.ProductId;
            product.SubId = ViProduct!.SubId;
            product.BrandId = ViProduct!.BrandId;
            product.Code = ViProduct!.Code;
            product.ProductName = ViProduct!.ProductName;
            product.Price = ViProduct!.Price;
            product.Description = ViProduct!.Description;
            product!.UpdatedDate = DateTime.Now;
            product.UpdatedUser = "Admin One";

            if (ImageFile != null)
            {
                string extension = ("." + ImageFile.FileName.Split('.')[^1]).ToLower();
                product.ImgUrl = $"images/products/{ViProduct!.ProductId}{extension}";
            }

            if (await _repository!.UpdateProduct(product))
            {
                if (ImageFile != null)
                    _imageService.Upload(ImageFile, ViProduct!.ProductId, "products");
            }

            return RedirectToPage("./Index");
        }
    }
}
