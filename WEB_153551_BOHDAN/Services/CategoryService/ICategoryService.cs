using WEB_153551_BOHDAN.UI.Domain.Entities;
using WEB_153551_BOHDAN.UI.Domain.Models;

namespace WEB_153551_BOHDAN.Services.CategoryService
{
    public interface ICategoryService
    {
        /// <summary>
        /// Получение списка всех категорий
        /// </summary>
        /// <returns></returns>
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }

}
