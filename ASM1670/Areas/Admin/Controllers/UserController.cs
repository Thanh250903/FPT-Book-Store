using ASM1670.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ASM1670.Data;
using ASM1670.Models;
using ASM1670.Models.ViewModels;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
=======
>>>>>>> 7fb99d824d7fb4f33e04ee52783ce70f0ed8ad17

namespace ASM1670.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManger;
<<<<<<< HEAD
        private readonly ResetPasswordVM _resetPasswordVM;
=======
>>>>>>> 7fb99d824d7fb4f33e04ee52783ce70f0ed8ad17

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


<<<<<<< HEAD
=======
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


>>>>>>> 7fb99d824d7fb4f33e04ee52783ce70f0ed8ad17

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

<<<<<<< HEAD
        public IActionResult ResetPassword(string id)
        {
            var user = _db.Users.Find(id);
            ResetPasswordVM model = new ResetPasswordVM()
            {
                Email = user.Email,
                Password = user.PasswordHash,
                ConfirmPassword = user.PasswordHash
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            var email = Request.Form["email"];
            var password = Request.Form["password"];
            var user = await _userManager.FindByEmailAsync(email);
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            if (user == null)
                return NotFound();
            var result = await _userManager.ResetPasswordAsync(user, code, password);
            if (result.Succeeded)
            {
                TempData["success"] = $"Password reset successfully! for (Email:  {user.Email})";
                return RedirectToAction("Index");
            }
            //error
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            model = new ResetPasswordVM()
            {
                Email = user.Email,
                Password = user.PasswordHash,
                ConfirmPassword = user.PasswordHash,
                Token = user.Id
            };
            TempData["error"] = "Something wrong!";
            return View(model);
        }

=======
>>>>>>> 7fb99d824d7fb4f33e04ee52783ce70f0ed8ad17
    }
}
