using ASM1670.Models;
using ASM1670.Data;
using ASM1670.Repository.IRepository;
using ASM1670.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using Newtonsoft.Json;
using Microsoft.CodeAnalysis;

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
        public async Task<IActionResult> Index(string currentFilter,string id, int? page)
        {
            if (_dbContext.Books == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            var movies = from m in _dbContext.Books
                         select m;

            if (!String.IsNullOrEmpty(id))
            {
                movies = movies.Where(s => s.Title!.Contains(id));
            }
            if (id != null)
            {
                page = 1;
            }
            else
            {
                id = currentFilter;
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(movies.ToPagedList(pageNumber, pageSize));
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
        public IActionResult AddToCart([FromRoute] int id)
        {

            var product = _dbContext.Books
                            .Where(p => p.Id == id)
                            .FirstOrDefault();
            if (product == null)
                return NotFound("Không có sản phẩm");

            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.book.Id == id);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.Quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartItem() { Quantity = 1, book = product });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }
        // Hiện thị giỏ hàng
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }
        /// xóa item trong cart
        public IActionResult RemoveCart([FromRoute] int id)
        {

            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.book.Id == id);
            if(cartitem != null)
            {
                cartitem.Quantity--;
            }
            if(cartitem.Quantity == 0)
            {
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }


        // Hiện thị giỏ hàng

        public IActionResult CheckOut()
        {
            // Xử lý khi đặt hàng
            return View();
        }



        // Key lưu chuỗi json của Cart
        public const string CARTKEY = "cart";

        // Lấy cart từ Session (danh sách CartItem)
        List<CartItem> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }

        // Xóa cart khỏi session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }
    }
}
