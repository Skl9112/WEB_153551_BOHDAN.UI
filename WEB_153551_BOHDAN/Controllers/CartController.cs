using Microsoft.AspNetCore.Mvc;

namespace WEB_153551_BOHDAN.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Add(int id, string returnUrl)
        {
            // Здесь должна быть логика добавления товара в корзину
            return Redirect(returnUrl ?? "/");
        }
    }
}