using System.Security.Claims;
using ASM1670.Data;
<<<<<<< HEAD
using ASM1670.Models;
using ASM1670.Utility;
using ASM1670.Models.ViewModels;
=======
using ASM1670.Models.ViewModels;
using ASM1670.Models;
using ASM1670.Utility;
>>>>>>> 7fb99d824d7fb4f33e04ee52783ce70f0ed8ad17
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASM1670.Controllers;

    [Area("Admin")]
    [Authorize(Roles = "User")]
<<<<<<< HEAD
    public class CartsController(ApplicationDBContext db) : Controller
    {
        private readonly ApplicationDBContext _db = db;

    [BindProperty] public ShoppingCartVM ShoppingCartVm { get; set; }

        // GET
=======
    public class CartsController : Controller
    {
        private readonly ApplicationDBContext _db;

        public CartsController(ApplicationDBContext db)
        {
            _db = db;
        }
        
        [BindProperty] public ShoppingCartVM ShoppingCartVm { get; set; }

>>>>>>> 7fb99d824d7fb4f33e04ee52783ce70f0ed8ad17
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVm = new ShoppingCartVM()
            {
                Order = new Models.Order(),
                ListCarts = _db.Carts.Where(u => u.UserId == claim.Value)
                    .Include(p => p.Book.Category)
            };

            ShoppingCartVm.Order.Total = 0;
            ShoppingCartVm.Order.User = _db.Users
                .FirstOrDefault(u => u.Id == claim.Value);


            foreach (var list in ShoppingCartVm.ListCarts)
            {
                list.Price = list.Book.Price;
                ShoppingCartVm.Order.Total += (list.Price * list.Count);
                if (list.Book.Description.Length > 100)
                {
                    list.Book.Description = list.Book.Description.Substring(0, 99) + "...";
                }
            }
            
            return View(ShoppingCartVm);
        }
        
<<<<<<< HEAD
        // Plus
=======
>>>>>>> 7fb99d824d7fb4f33e04ee52783ce70f0ed8ad17
        public IActionResult Plus(int cartId)
        {
            var cart = _db.Carts.Include(p => p.Book)
                .FirstOrDefault(c => c.Id == cartId);

            cart.Count += 1;
            cart.Price = cart.Book.Price;
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cart = _db.Carts.Include(p => p.Book).FirstOrDefault(c => c.Id == cartId);
            if (cart.Count == 1)
            {
                var cnt = _db.Carts.Where(u => u.UserId == cart.UserId).ToList().Count;
                _db.Carts.Remove(cart);
                _db.SaveChanges();
            }
            else
            {
                cart.Count -= 1;
                cart.Price = cart.Book.Price;
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var cart = await _db.Carts.FindAsync(id);

                if (cart == null)
                    return NotFound("Cart is empty");
                
        
                _db.Carts.Remove(cart);
                await _db.SaveChangesAsync();
        
                // update the shopping cart count in the session
                var userId = cart.UserId;
                var count = _db.Carts.Count(c => c.UserId == userId);
                HttpContext.Session.SetInt32(Constraintt.ssShoppingCart, count);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong");
            }

            return RedirectToAction(nameof(Index));
        }
        
<<<<<<< HEAD
        // summary
=======
>>>>>>> 7fb99d824d7fb4f33e04ee52783ce70f0ed8ad17
        [HttpGet]
        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVm = new ShoppingCartVM()
            {
                Order = new Models.Order(),
                ListCarts = _db.Carts.Where(u => u.UserId == claim.Value)
                    .Include(c => c.Book)
            };

            ShoppingCartVm.Order.User = _db.Users
                .FirstOrDefault(u => u.Id == claim.Value);

            foreach (var list in ShoppingCartVm.ListCarts)
            {
                list.Price = list.Book.Price;
<<<<<<< HEAD
                ShoppingCartVm.Order.Total += (list.Price * list.Count);
=======
                ShoppingCartVm.Order.Total += (list.Price + list.Count);
>>>>>>> 7fb99d824d7fb4f33e04ee52783ce70f0ed8ad17
            }

            ShoppingCartVm.Order.Address = ShoppingCartVm.Order.User.HomeAddress;
            ShoppingCartVm.Order.Order_Date = DateTime.Now;

            return View(ShoppingCartVm);
        }
        
        // summary post
        [HttpPost]
        [ActionName("Summary")]
        [ValidateAntiForgeryToken]
        public IActionResult SummaryPost()
        {
            // lay id cua user
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            // lay thong tin user dang mua
            // lay toan bo list cart
            ShoppingCartVm.Order.User = _db.Users
                .FirstOrDefault(c => c.Id == claim.Value);
            ShoppingCartVm.ListCarts = _db.Carts.Where(c => c.UserId== claim.Value)
                .Include(c => c.Book);

            
            // assign value for each field in order header
            ShoppingCartVm.Order.UserId = claim.Value;
            ShoppingCartVm.Order.Address = ShoppingCartVm.Order.User.HomeAddress;
            ShoppingCartVm.Order.Order_Date = DateTime.Now;

            // add to order header and save changes to get order header id
            _db.Orders.Add(ShoppingCartVm.Order);
            _db.SaveChanges();

            // moi san pham add vao order detail
            foreach (var item in ShoppingCartVm.ListCarts)
            {
                item.Price = item.Book.Price;
                OrderDetail orderDetail = new OrderDetail()
                {
                    BookId = item.BookId,
                    OrderId = ShoppingCartVm.Order.Id,
                    Price = item.Price,
                    Quantity = item.Count
                };

                // calculate total for order header and add to order detail
<<<<<<< HEAD
                ShoppingCartVm.Order.Total += orderDetail.Quantity * orderDetail.Price;
=======
                ShoppingCartVm.Order.Total += orderDetail.Quantity + orderDetail.Price;
>>>>>>> 7fb99d824d7fb4f33e04ee52783ce70f0ed8ad17
                _db.OrderDetails.Add(orderDetail);
            }
            
            // remove that item from cart
            _db.Carts.RemoveRange(ShoppingCartVm.ListCarts);
            _db.SaveChanges();
            HttpContext.Session.SetInt32(Constraintt.ssShoppingCart, 0);

            return RedirectToAction("OrderConfirmation", "Carts", 
                new { id = ShoppingCartVm.Order.Id });
        }
        
        // order confirm 
        public IActionResult OrderConfirmation(int id)
        {
            var claimIdentity = (ClaimsIdentity) User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return View(id);
        }
    }