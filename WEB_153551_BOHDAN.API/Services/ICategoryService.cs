using WEB_153551_BOHDAN.UI.Domain.Entities;
using WEB_153551_BOHDAN.UI.Domain.Models;

namespace WEB_153551_BOHDAN.API.Services
{
    public interface ICategoryService
    {
        Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }

}
