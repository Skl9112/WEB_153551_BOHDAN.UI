using Microsoft.AspNetCore.Mvc;
using WEB_153551_BOHDAN.Services.CategoryService;
using WEB_153551_BOHDAN.UI.Domain.Entities;
using WEB_153551_BOHDAN.UI.Domain.Models;

namespace WEB_153551_BOHDAN.Services.ProductService
{
    public class MemoryProductService : IProductService
    {
        private List<Dish> _dishes;
        private List<Category> _categories;

        public MemoryProductService(ICategoryService categoryService)

        {
            _categories = categoryService.GetCategoryListAsync().Result.Data;
            SetupData();
        }

        /// <summary>
        /// Инициализация списка блюд
        /// </summary>
        private void SetupData()
        {
            _dishes = new List<Dish>
            {
                new Dish { Id = 1, Name = "Суп-харчо", Description = "Очень острый, невкусный", Calories = 200, Image = "Images/soup-harcho.jpg",
                           Category = _categories.Find(c => c.NormalizedName.Equals("soups")) },

                new Dish { Id = 2, Name = "Борщ", Description = "Много сала, без сметаны", Calories = 330, Image = "Images/borscht.jpg",
                           Category = _categories.Find(c => c.NormalizedName.Equals("soups")) },

                new Dish { Id = 3, Name = "Цезарь", Description = "С курицей и сыром", Calories = 250, Image = "Images/caesar.jpg",
                           Category = _categories.Find(c => c.NormalizedName.Equals("salads")) },

                new Dish { Id = 4, Name = "Шашлык", Description = "Сочный, на углях", Calories = 500, Image = "Images/shashlik.jpg",
                           Category = _categories.Find(c => c.NormalizedName.Equals("main-dishes")) },

                new Dish { Id = 5, Name = "Чай", Description = "Чёрный, без сахара", Calories = 10, Image = "Images/tea.jpg",
                           Category = _categories.Find(c => c.NormalizedName.Equals("drinks")) }
            };
        }

        public async Task<ResponseData<ListModel<Dish>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var filteredDishes = string.IsNullOrEmpty(categoryNormalizedName)
                ? _dishes
                : _dishes.Where(d => d.Category.NormalizedName == categoryNormalizedName).ToList();

            var response = new ListModel<Dish>
            {
                Items = filteredDishes,
                CurrentPage = 1,
                TotalPages = 1
            };

            return ResponseData<ListModel<Dish>>.Success(response);
        }

        public Task<ResponseData<Dish>> GetProductByIdAsync(int id)
        {
            var dish = _dishes.FirstOrDefault(d => d.Id == id);
            if (dish == null)
            {
                return Task.FromResult(ResponseData<Dish>.Error("Блюдо не найдено"));
            }

            return Task.FromResult(ResponseData<Dish>.Success(dish));
        }

        public Task UpdateProductAsync(int id, Dish product, IFormFile? formFile)
        {
            var dish = _dishes.FirstOrDefault(d => d.Id == id);
            if (dish != null)
            {
                dish.Name = product.Name;
                dish.Description = product.Description;
                dish.Calories = product.Calories;
                dish.Image = formFile != null ? $"Images/{formFile.FileName}" : dish.Image;
                dish.Category = product.Category;
            }

            return Task.CompletedTask;
        }

        public Task DeleteProductAsync(int id)
        {
            _dishes.RemoveAll(d => d.Id == id);
            return Task.CompletedTask;
        }

        public Task<ResponseData<Dish>> CreateProductAsync(Dish product, IFormFile? formFile)
        {
            product.Id = _dishes.Max(d => d.Id) + 1;
            product.Image = formFile != null ? $"Images/{formFile.FileName}" : "Images/default.jpg";
            _dishes.Add(product);

            return Task.FromResult(ResponseData<Dish>.Success(product));
        }
    }
}