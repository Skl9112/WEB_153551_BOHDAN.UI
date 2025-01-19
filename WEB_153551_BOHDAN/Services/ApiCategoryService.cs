using System.Text.Json;
using WEB_153551_BOHDAN.Services.CategoryService;
using WEB_153551_BOHDAN.UI.Domain.Entities;
using WEB_153551_BOHDAN.UI.Domain.Models;

namespace WEB_153551_BOHDAN.Services
{
    public class ApiCategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly ILogger<ApiCategoryService> _logger;

        public ApiCategoryService(HttpClient httpClient, ILogger<ApiCategoryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

        }

        public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("categories");
                if (!response.IsSuccessStatusCode)
                {
                    return ResponseData<List<Category>>.Error($"Error: {response.StatusCode}");
                }

                return await response.Content.ReadFromJsonAsync<ResponseData<List<Category>>>(_serializerOptions)
                    ?? ResponseData<List<Category>>.Error("Unable to get categories");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting categories");
                return ResponseData<List<Category>>.Error(ex.Message);
            }
        }
    }
}
