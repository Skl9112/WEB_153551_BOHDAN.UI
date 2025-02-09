// WEB_153551_BOHDAN/Areas/Admin/Pages/Index.cshtml.cs
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_153551_BOHDAN.UI.Domain.Entities;
using WEB_153551_BOHDAN.UI.Domain.Models;
using WEB_153551_BOHDAN.API.Services;
using WEB_153551_BOHDAN.UI.Domain.Entities;
using WEB_153551_BOHDAN.UI.Domain.Models;

public class IndexModel : PageModel
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public IndexModel(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public ListModel<Dish> Products { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
    public string? CurrentCategory { get; set; }
    public int CurrentPage { get; set; }

    public async Task OnGetAsync(string? category, int pageNo = 1)
    {
        var categoriesResponse = await _categoryService.GetCategoryListAsync();
        if (categoriesResponse.Successfull)
            Categories = categoriesResponse.Data!;

        var productsResponse = await _productService.GetProductListAsync(category, pageNo);
        if (productsResponse.Successfull)
            Products = productsResponse.Data!;

        CurrentCategory = category;
        CurrentPage = pageNo;
    }
}
