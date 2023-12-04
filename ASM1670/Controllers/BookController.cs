using Microsoft.AspNetCore.Mvc;

namespace ASM1670.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
