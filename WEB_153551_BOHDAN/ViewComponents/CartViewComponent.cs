using Microsoft.AspNetCore.Mvc;

namespace WEB_153551_BOHDAN.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cartSummary = new { TotalPrice = "00.0 руб", ItemsCount = 0 };
            return View(cartSummary);
        }
    }
}
