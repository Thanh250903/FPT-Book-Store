using ASM1670.Models;
using ASM1670.Data;
using ASM1670.Repository.IRepository;
using ASM1670.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace ASM1670.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Book> books = _unitOfWork.BookRepository.GetAll("Category").ToList();
            return View(books);
        }
        public IActionResult Detail(int id)
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
            bookVM.Book = _unitOfWork.BookRepository.Get(b => b.Id == id);
            return View(bookVM);
        }
    }
}
