using Microsoft.AspNetCore.Mvc;

namespace WEB_153551_BOHDAN.Controllers
{
    public class Home : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
