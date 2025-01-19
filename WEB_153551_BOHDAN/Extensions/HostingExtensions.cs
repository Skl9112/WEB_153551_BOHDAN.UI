using WEB_153551_BOHDAN.Services.ProductService;
using WEB_153551_BOHDAN.Services.CategoryService;

namespace WEB_153551_BOHDAN.Extensions
{
    public static class HostingExtensions
    {
        public static void RegisterCustomServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoryService, MemoryCategoryService>();
            builder.Services.AddScoped<IProductService, MemoryProductService>();
        }
    }
}
