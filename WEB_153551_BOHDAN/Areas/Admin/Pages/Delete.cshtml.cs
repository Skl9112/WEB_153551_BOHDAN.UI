using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using WEB_153551_BOHDAN.API.Services;
using WEB_153551_BOHDAN.UI.Domain.Entities;

namespace WEB_153551_BOHDAN.Areas.Admin.Pages
{
    // WEB_153551_BOHDAN/Areas/Admin/Pages/Delete.cshtml.cs
    public class DeleteModel : PageModel
    {
        private readonly IProductService _productService;

        public DeleteModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Dish Product { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _productService.GetProductByIdAsync(id);
            if (!response.Successfull)
                return NotFound();

            Product = response.Data!;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToPage("./Index");
        }
    }
}

