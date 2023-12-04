using Microsoft.AspNetCore.Mvc;

namespace ASM1670.Controllers
{
    public class AuthorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
