using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using WEB_153551_BOHDAN.API.Services;
using WEB_153551_BOHDAN.UI.Domain.Entities;

namespace WEB_153551_BOHDAN.Areas.Admin.Pages
{
    // WEB_153551_BOHDAN/Areas/Admin/Pages/Create.cshtml.cs
    public class CreateModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public CreateModel(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [BindProperty]
        public Dish Product { get; set; } = new();

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public List<Category> Categories { get; set; } = new();

        public async Task OnGetAsync()
        {
            var response = await _categoryService.GetCategoryListAsync();
            if (response.Successfull)
                Categories = response.Data!;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var response = await _categoryService.GetCategoryListAsync();
                if (response.Successfull)
                    Categories = response.Data!;
                return Page();
            }

            var result = await _productService.CreateProductAsync(Product, ImageFile);
            if (result.Successfull)
                return RedirectToPage("./Index");

            ModelState.AddModelError("", result.ErrorMessage!);
            return Page();
        }
    }
}
