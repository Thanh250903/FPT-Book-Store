using ASM1670.Data;
using ASM1670.Models;
using ASM1670.Models.ViewModels;
using ASM1670.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;

namespace ASM1670.Areas.User.Controllers
{
    [Area("User")]
    public class CartController : Controller
    {
        private readonly ApplicationDBContext   _dbContext;
        private readonly UnitOfWork _unitOfWork;
        public IActionResult Index()
        {
            return View();
        }


        // Lấy cart từ Session (danh sách CartItem)
        //List<CartItem> GetCartItems()
        //{

        //    var session = HttpContext.Session;
        //    string jsoncart = session.GetString(CARTKEY);
        //    if (jsoncart != null)
        //    {
        //        return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
        //    }
        //    return new List<CartItem>();
        //}

        //// Xóa cart khỏi session
        //void ClearCart()
        //{
        //    var session = HttpContext.Session;
        //    session.Remove(CARTKEY);
        //}

        //// Lưu Cart (Danh sách CartItem) vào session
        //void SaveCartSession(List<CartItem> ls)
        //{
        //    var session = HttpContext.Session;
        //    string jsoncart = JsonConvert.SerializeObject(ls);
        //    session.SetString(CARTKEY, jsoncart);
        //}

        ///// Thêm sản phẩm vào cart
        //[Route("User/Cart/addcart/{id:int}")]
        //public IActionResult AddToCart([FromRoute] int? id)
        //{

        //    var book = _unitOfWork.BookRepository?.Get(b => b.Id == id);
        //    if (book == null)
        //        return NotFound("Không có sản phẩm");

        //    // Xử lý đưa vào Cart ...
        //    var cart = GetCartItems();
        //    var cartitem = cart.Find(p => p.book.Id == id);
        //    if (cartitem != null)
        //    {
        //        // Đã tồn tại, tăng thêm 1
        //        cartitem.Quantity++;
        //    }
        //    else
        //    {
        //        //  Thêm mới
        //        cart.Add(new CartItem() { Quantity = 1, book = book });
        //    }
        //    // Lưu cart vào Session
        //    SaveCartSession(cart);
        //    // Chuyển đến trang hiện thị Cart
        //    return RedirectToAction(nameof(Cart));
        //}
        ///// xóa item trong cart
        //[Route("User/Cart/removecart/{id:int}", Name = "removecart")]
        //public IActionResult RemoveCart([FromRoute] int id)
        //{

        //    // Xử lý xóa một mục của Cart ...
        //    return RedirectToAction(nameof(Cart));
        //}

        ///// Cập nhật
        //[Route("User/Cart/updatecart", Name = "updatecart")]
        //[HttpPost]
        //public IActionResult UpdateCart([FromForm] int id, [FromForm] int quantity)
        //{
        //    // Cập nhật Cart thay đổi số lượng quantity ...

        //    return RedirectToAction(nameof(Cart));
        //}


        //// Hiện thị giỏ hàng
        //[Route("User/Cart/Cart", Name = "cart")]
        //public IActionResult Cart()
        //{
        //    return View(GetCartItems());
        //}

        //[Route("User/Cart/checkout")]
        //public IActionResult CheckOut()
        //{
        //    // Xử lý khi đặt hàng
        //    return View();
        //}

        public const string CARTKEY = "cart";
        public AcceptedResult AddItem(int id, int quantity)
        {
            var cart = ISeaso;
            if (cart != null)
            {

            }
            else
            {
                var item = new CartItem();
                item.Id = id;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                HttpContext.Session = (ISession)list;
            }
        }
    }
}
