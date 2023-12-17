using ASM1670.Models;
using ASM1670.Data;
using ASM1670.Repository.IRepository;
using ASM1670.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASM1670.Areas.User.Controllers
{
    [Area("User")]
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDBContext _dbContext;
        public BookController(IUnitOfWork unitOfWork, ApplicationDBContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }
        //public IActionResult Index()
        //{
        //    List<Book> books = _unitOfWork.BookRepository.GetAll("Category").ToList();
        //    return View(books);
        //}
        public async Task<IActionResult> Index(string searchString)
        {
            if (_dbContext.Books == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            var movies = from m in _dbContext.Books
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title!.Contains(searchString));
            }

            return View(await movies.ToListAsync());
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
