using System.Text.Json;
using System.Text;
using WEB_153551_BOHDAN.UI.Domain.Models;
using WEB_153551_BOHDAN.Services.ProductService;
using WEB_153551_BOHDAN.UI.Domain.Entities;

namespace WEB_153551_BOHDAN.Services
{
    public class ApiProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly ILogger<ApiProductService> _logger;

        public ApiProductService(HttpClient httpClient, ILogger<ApiProductService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<ResponseData<Dish>> CreateProductAsync(Dish dish)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("products", dish, _serializerOptions);
                if (!response.IsSuccessStatusCode)
                {
                    return ResponseData<Dish>.Error($"Error: {response.StatusCode}");
                }

                return await response.Content.ReadFromJsonAsync<ResponseData<Dish>>(_serializerOptions)
                    ?? ResponseData<Dish>.Error("Unable to create product");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                return ResponseData<Dish>.Error(ex.Message);
            }
        }

        public async Task<ResponseData<string>> SaveImageAsync(int id, IFormFile file)
        {
            try
            {
                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(file.OpenReadStream()), "file", file.FileName);

                var response = await _httpClient.PostAsync($"products/{id}/image", content);
                if (!response.IsSuccessStatusCode)
                {
                    return ResponseData<string>.Error($"Error uploading image: {response.StatusCode}");
                }

                return await response.Content.ReadFromJsonAsync<ResponseData<string>>(_serializerOptions)
                    ?? ResponseData<string>.Error("Unable to upload image");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading image");
                return ResponseData<string>.Error(ex.Message);
            }
        }

        public async Task<ResponseData<bool>> DeleteProductAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"products/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    return ResponseData<bool>.Error($"Error: {response.StatusCode}");
                }

                return await response.Content.ReadFromJsonAsync<ResponseData<bool>>(_serializerOptions)
                    ?? ResponseData<bool>.Error("Unable to delete product");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product");
                return ResponseData<bool>.Error(ex.Message);
            }
        }

        public async Task<ResponseData<Dish>> GetProductByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"products/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    return ResponseData<Dish>.Error($"Error: {response.StatusCode}");
                }

                return await response.Content.ReadFromJsonAsync<ResponseData<Dish>>(_serializerOptions)
                    ?? ResponseData<Dish>.Error("Unable to get product");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product");
                return ResponseData<Dish>.Error(ex.Message);
            }
        }

        public async Task<ResponseData<ListModel<Dish>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1, int pageSize = 3)
        {
            try
            {
                var urlBuilder = new StringBuilder("products?");
                if (!string.IsNullOrEmpty(categoryNormalizedName))
                {
                    urlBuilder.Append($"category={Uri.EscapeDataString(categoryNormalizedName)}&");
                }
                urlBuilder.Append($"pageNo={pageNo}&pageSize={pageSize}");

                var response = await _httpClient.GetAsync(urlBuilder.ToString());
                if (!response.IsSuccessStatusCode)
                {
                    return ResponseData<ListModel<Dish>>.Error($"Error: {response.StatusCode}");
                }

                return await response.Content.ReadFromJsonAsync<ResponseData<ListModel<Dish>>>(_serializerOptions)
                    ?? ResponseData<ListModel<Dish>>.Error("Unable to get products");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product list");
                return ResponseData<ListModel<Dish>>.Error(ex.Message);
            }
        }

        public async Task<ResponseData<bool>> UpdateProductAsync(int id, Dish dish)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"products/{id}", dish, _serializerOptions);
                if (!response.IsSuccessStatusCode)
                {
                    return ResponseData<bool>.Error($"Error: {response.StatusCode}");
                }

                return ResponseData<bool>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product");
                return ResponseData<bool>.Error(ex.Message);
            }
        }

        public Task<ResponseData<ListModel<Dish>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(int id, Dish product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        Task IProductService.DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Dish>> CreateProductAsync(Dish product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}
