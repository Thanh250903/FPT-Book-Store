using ASM1670.Models;
using ASM1670.Data;
using ASM1670.Repository.IRepository;
using ASM1670.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASM1670.Areas.Admin.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, ApplicationDBContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            List<Book> books = _unitOfWork.BookRepository.GetAll("Category").ToList();
            return View(books);
        }
        public IActionResult CreateUpdate(int? id)
        {
            BookVM bookVM = new BookVM()
            {
                Categories = _unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                }),
                Book = new Book()
            };
            if (id == null || id == 0)
            {
                //Create
                return View(bookVM);
            }
            else
            {
                //Update
                bookVM.Book = _unitOfWork.BookRepository.Get(b => b.Id == id);
                return View(bookVM);
            }

        }
        [HttpPost]
        public IActionResult CreateUpdate(BookVM bookVM, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string bookPath = Path.Combine(wwwRootPath, @"images\books");

                    if (!string.IsNullOrEmpty(bookVM.Book.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, bookVM.Book.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(bookPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    bookVM.Book.ImageUrl = @"\images\books\" + fileName;
                }
                if (bookVM.Book.Id == 0)
                {
                    _unitOfWork.BookRepository.Add(bookVM.Book);
                    TempData["success"] = "Book Created successfully";
                }
                else
                {
                    _unitOfWork.BookRepository.Update(bookVM.Book);
                    TempData["success"] = "Book Updated successfully";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                BookVM bookVMNew = new BookVM()
                {
                    Categories = _unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString(),
                    }),
                    Book = new Book()
                };
                return View(bookVMNew);
            }

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Book? book = _unitOfWork.BookRepository.Get(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        [HttpPost]
        public IActionResult Delete(Book book)
        {
            _unitOfWork.BookRepository.Delete(book);
            _unitOfWork.Save();
            TempData["success"] = "Book Deleted successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int id)
        {
            Book? book = _unitOfWork.BookRepository.Get(b => b.Id == id);
            return View(book);
        }
    }
}
