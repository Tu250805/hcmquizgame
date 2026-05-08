using Microsoft.AspNetCore.Mvc;

namespace HCMQuizGame.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}