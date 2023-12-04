using ASM1670.Data;
using ASM1670.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASM1670.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        public CategoryController(ApplicationDBContext applicationDBContext)
        {
            _dbContext = applicationDBContext;
        }
        public IActionResult Index()
        {
            List<Category> categories = _dbContext.Categories.ToList();
            return View(categories);
        }
        public IActionResult Details()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
