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
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
        public IActionResult Create(Category category)
        {

            if (category.Name == category.Description)
            {
                ModelState.AddModelError("Description", "Name can not be equal to Description");
            }
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                TempData["success"] = "Category Created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? category = _dbContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(category);
                _dbContext.SaveChanges();
                TempData["success"] = "Category Updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? category = _dbContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Delete(Category category)
        {
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            TempData["success"] = "Category Deleted successfully";
            return RedirectToAction("Index");
        }
    }
}


