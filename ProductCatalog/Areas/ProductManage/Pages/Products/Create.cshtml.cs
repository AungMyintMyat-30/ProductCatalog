using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductCatalog.Repositories;
using ProductCatalogCore.Entities;
using ProductCatalogCore.Interfaces;
using System.Text;

namespace ProductCatalog.Areas.ProductManage.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly ProductRepo? _repository;
        private readonly IImageService _imageService;
        private readonly IRandomizer _randomizer;

        public CreateModel(ProductRepo repository,
                           IImageService imageService,
                           IRandomizer randomizer)
        {
            _repository = repository;
            _imageService = imageService;
            _randomizer = randomizer;
        }

        [BindProperty]
        public IFormFile? ImageFile { get; set; }
        [BindProperty]
        public ViProduct? ViProduct { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            StringBuilder GetId()
            {
                StringBuilder stringBuilder = new();
                stringBuilder.Append(_randomizer.RandomAlphanumeric(4));
                stringBuilder.Append('-');
                stringBuilder.Append(_randomizer.RandomAlphanumeric(4));
                return stringBuilder;
            }
            string PID = GetId().ToString();

            Product product = new();
            product.ProductId = PID;
            product.SubId = ViProduct!.SubId;
            product.BrandId = ViProduct!.BrandId;
            product.Code = ViProduct!.Code;
            product.ProductName = ViProduct!.ProductName;
            product.Price = ViProduct!.Price;
            product.Description = ViProduct!.Description;
            product!.CreatedDate = DateTime.Now;
            product.CreatedUser = "Admin";

            if (ImageFile != null)
            {
                string extension = ("." + ImageFile.FileName.Split('.')[^1]).ToLower();
                product.ImgUrl = $"images/products/{PID}{extension}";
            }

            if (await _repository!.AddProduct(product))
            {
                if (ImageFile != null)
                    _imageService.Upload(ImageFile, PID, "products");

            }

            return RedirectToPage("./Index");
        }
    }
}
