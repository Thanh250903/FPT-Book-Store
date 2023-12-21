using ASM1670.Models;
using ASM1670.Data;
using ASM1670.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ASM1670.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Owner")]
    public class BookController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(IWebHostEnvironment webHostEnvironment, ApplicationDBContext dbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            // lay id cua user
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var currentUserId = claim.Value;

            var books = _dbContext.Books
                .Where(x => x.CreateBy == currentUserId)
                .Include(x => x.Category).ToList();
            return View(books);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var objBook = _dbContext.Books.Find(id);
            _dbContext.Books.Remove(objBook);
            _dbContext.SaveChanges();

            TempData["DeleteBoMessage"] = "Deleted Book Successfully!";
            TempData["ShowMessage"] = true;
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            var bookVm = new BookVM();

            bookVm.Categories = CategorySelectListItems();


            if (id == null)
            {
                bookVm.Book = new Book();
                return View(bookVm);
            }
            var book = _dbContext.Books.Find(id);
            bookVm.Book = book;
            return View(bookVm);
        }
        [HttpPost]
        public IActionResult CreateUpdate(BookVM bookVm)
        {
            if (ModelState.IsValid)
            {
                bookVm.Categories = CategorySelectListItems();

                return View(bookVm);
            }

            var webRootPath = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                var fileName = Guid.NewGuid();
                var uploads = Path.Combine(webRootPath, @"images/books/");
                var extension = Path.GetExtension(files[0].FileName);
                if (bookVm.Book.Id != 0)
                {
                    var productDb = _dbContext.Books.AsNoTracking()
                        .Where(b => b.Id == bookVm.Book.Id).First();
                    if (productDb.ImageUrl != null && bookVm.Book.Id != 0)
                    {
                        var imagePath = Path.Combine(webRootPath, productDb.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(imagePath)) System.IO.File.Delete(imagePath);
                    }
                }

                using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(filesStreams);
                }

                bookVm.Book.ImageUrl = @"/images/books/"+ fileName + extension;
            }

            else
            {
                bookVm.Categories = CategorySelectListItems();
                return View(bookVm);
            }


            if (bookVm.Book.Id == 0 || bookVm.Book.Id == null)
            {
                // lay id cua user
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                var currentUserId = claim.Value;
                bookVm.Book.CreateBy = currentUserId;

                _dbContext.Books.Add(bookVm.Book);
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                var currentUserId = claim.Value;
                bookVm.Book.CreateBy = currentUserId;
                _dbContext.Books.Update(bookVm.Book);
            }
            _dbContext.SaveChanges();
            TempData["CreateBoMessage"] = "Created Book sSuccessfully!";
            TempData["ShowMessage"] = true;
            return RedirectToAction(nameof(Index));
        }
        private IEnumerable<SelectListItem> CategorySelectListItems()
        {
            var categories = _dbContext.Categories
                .Where(c => c.Status == Category.StatusCategory.Approve)
                .ToList();

            var result = categories
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });

            return result;
        }

    }
}
