using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {

            return View();
        }

    }
}