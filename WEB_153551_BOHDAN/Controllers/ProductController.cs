using Microsoft.AspNetCore.Mvc;
using WEB_153551_BOHDAN.Services.ProductService;
using WEB_153551_BOHDAN.Services.CategoryService;

namespace WEB_153551_BOHDAN.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(string? categoryNormalizedName)
        {
            var productResponse = await _productService.GetProductListAsync(categoryNormalizedName);
            if (!productResponse.Successfull)
                return NotFound(productResponse.ErrorMessage);

            ViewBag.SelectedCategory = categoryNormalizedName == null
                ? "Все категории"
                : _categoryService.GetCategoryListAsync().Result.Data
                    .FirstOrDefault(c => c.NormalizedName == categoryNormalizedName)?.Name ?? "Все категории";

            return View(productResponse.Data.Items);
        }
    }
}