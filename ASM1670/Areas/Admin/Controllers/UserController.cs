using ASM1670.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ASM1670.Data;
using ASM1670.Models;
using ASM1670.Models.ViewModels;

namespace ASM1670.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManger;

        public UserController(ApplicationDBContext db, UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManger = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userList = _db.Users.Where(u => u.Id != claims.Value);

            foreach (var user in userList)
            {
                var userTemp = await _userManager.FindByIdAsync(user.Id);
                var roleTemp = await _userManager.GetRolesAsync(userTemp);
                user.Role = roleTemp.FirstOrDefault();
            }


            return View(userList.ToList());
        }



        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            await _userManager.DeleteAsync(user);
            TempData["DeleteUserMessage"] = "User Deleted!";
            TempData["ShowMessage"] = true;
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            if (token == null || email == null) ModelState.AddModelError("", "Invalid password reset token");

            var resetPasswordViewModel = new ResetPasswordVM()
            {
                Email = email,
                Token = token
            };

            return View(resetPasswordViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Token,
                        resetPasswordViewModel.Password);
                    if (result.Succeeded) return RedirectToAction(nameof(Index));
                }
            }

            return View(resetPasswordViewModel);
        }



        [HttpGet]
        public IActionResult EditUser(string id)
        {
            var adminStoreOwnerCustomer = _db.Users.Find(id);
            return View(adminStoreOwnerCustomer);
        }

        [HttpPost]
        public IActionResult EditUser(User user)
        {
            var adminStoreOwnerCustomer = _db.Users.Find(user.Id);
            if (adminStoreOwnerCustomer == null)
            {
                return NotFound("User is null");
            }

            adminStoreOwnerCustomer.FullName = user.FullName;
            adminStoreOwnerCustomer.PhoneNumber = user.PhoneNumber;
            adminStoreOwnerCustomer.HomeAddress = user.HomeAddress;

            _db.Users.Update(adminStoreOwnerCustomer);
            _db.SaveChanges();

            TempData["EditUserMessage"] = "The information of User updated!";
            TempData["ShowMessage"] = true; 
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = _db.Users.Find(id);
            var roletemp = await _userManager.GetRolesAsync(user);
            var role = roletemp.First();

            return RedirectToAction("EditUser", new { id });
        }

        // list tất cả các request của categories tới admin
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Requests()
        {
            var requests = await _db.Categories.Where(_ => _.Status == Category.StatusCategory.Pending).ToListAsync();

            return View(requests);

        }

        [Authorize(Roles = "Admin")]
        // cái này chấp nhận request
        [HttpGet]
        public async Task<IActionResult> ApproveRequest(int id)
        {
            var categoryToApprove = await _db.Categories.FindAsync(id);
            if (categoryToApprove == null)
                return NotFound("The request not found!");

            // approve it if there's one
            categoryToApprove.Status = Category.StatusCategory.Approve;
            await _db.SaveChangesAsync();
            TempData["SuccessMessage1"] = "Approved for Create Category successfully!";
            TempData["ShowMessage"] = true; 
            return RedirectToAction("Requests");
        }


        [Authorize(Roles = "Admin")]
        // từ chối request
        [HttpGet]
        public async Task<IActionResult> RejectRequest(int id)
        {
            var categoryToReject = await _db.Categories.FindAsync(id);
            if (categoryToReject == null)
                return NotFound("The request not found!");

            categoryToReject.Status = Category.StatusCategory.Reject;
            _db.Categories.Remove(categoryToReject);
            await _db.SaveChangesAsync();
            TempData["RejectMessage"] = "Rejected for Create Category!";
            TempData["ShowMessage"] = true;

            return RedirectToAction("Requests");
        }

    }
}
