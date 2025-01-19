using WEB_153551_BOHDAN.UI.Domain.Models;
using WEB_153551_BOHDAN.UI.Domain.Entities;

namespace WEB_153551_BOHDAN.Services.ProductService
{
    public interface IProductService
    {
        Task<ResponseData<ListModel<Dish>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1);
        Task<ResponseData<Dish>> GetProductByIdAsync(int id); // Проверьте, есть ли этот метод
        Task UpdateProductAsync(int id, Dish product, IFormFile? formFile);
        Task DeleteProductAsync(int id);
        Task<ResponseData<Dish>> CreateProductAsync(Dish product, IFormFile? formFile);
    }
}
