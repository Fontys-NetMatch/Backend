using Microsoft.AspNetCore.Mvc;

namespace TravelPlanner.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
