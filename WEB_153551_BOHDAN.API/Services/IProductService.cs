using WEB_153551_BOHDAN.UI.Domain.Entities;
using WEB_153551_BOHDAN.UI.Domain.Models;

namespace WEB_153551_BOHDAN.API.Services
{
    public interface IProductService
    {
        Task<ResponseData<ListModel<Dish>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1);
        Task<ResponseData<Dish>> GetProductByIdAsync(int id);
        Task UpdateProductAsync(int id, Dish product, IFormFile? formFile);
        Task DeleteProductAsync(int id);
        Task<ResponseData<Dish>> CreateProductAsync(Dish product, IFormFile? formFile);
    }

}
