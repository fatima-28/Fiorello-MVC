using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
