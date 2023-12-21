using ASM1670.Data;
using ASM1670.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ASM1670.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Owner")]
    public class OrderController : Controller
    {
        private readonly ApplicationDBContext _db;

        public OrderController(ApplicationDBContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<int> listId = new List<int>();

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            bool isStoreOwner = User.IsInRole(Constraintt.OwnerRole);

            if (isStoreOwner)
            {
                var orderList = _db.Orders
                    .Include(o => o.User)
                    .ToList();

                return View(orderList);
            }
            else
            {
                var orderList = _db.Orders
                    .Where(x => x.UserId == claims.Value)
                    .Include(o => o.User)
                    .ToList();

                return View(orderList);
            }


        }

        [HttpGet]
        public IActionResult Detail(int orderId)
        {
            var Detail = _db.OrderDetails
                .Where(d => d.OrderId == orderId)
                .Include(d => d.Book)
                .ToList();


            return View(Detail);
        }
    }
}
