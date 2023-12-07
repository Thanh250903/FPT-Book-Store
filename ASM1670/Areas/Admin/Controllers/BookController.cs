using ASM1670.Data;
using ASM1670.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASM1670.Areas.Admin.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(ApplicationDBContext applicationDBContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = applicationDBContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Book> books = _dbContext.Books.ToList();
            return View(books);
        }
        public IActionResult Detail(int id)
        {
            var book = _dbContext.Books.Where(s => s.Id == id).FirstOrDefault();
            return View(book);
        }
        public IActionResult CreateUpdate(int? id)
        {
            Book book = new Book();
            if (id == null || id == 0)
            {
                //Create
                return View(book);
            }
            else
            {
                //Update
                book = _dbContext.Books.Find(id);
                return View(book);
            }

        }
        [HttpPost]
        public IActionResult CreateUpdate(Book book, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string bookPath = Path.Combine(wwwRootPath, @"images\books");

                    if (!String.IsNullOrEmpty(book.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, book.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(bookPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    book.ImageUrl = @"\images\books\" + fileName;
                }
            }

        if (ModelState.IsValid)
            {
                if (book.Id == 0)
                {
                    _dbContext.Books.Add(book);
                    TempData["success"] = "Book Created successfully";
                }
                else
                {
                    _dbContext.Books.Update(book);
                    TempData["success"] = "Book Updated successfully";
                }

                _dbContext.SaveChanges();
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
            Book? book = _dbContext.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        [HttpPost]
        public IActionResult Delete(Book book)
        {
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
            TempData["success"] = "Book Deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
