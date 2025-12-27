using eTickets.Data;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{

    public class AccountsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly AppDbContext _context;
        public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager, AppDbContext context)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _context = context;
        }
        public IActionResult Login()
        {
            var response = new LoginVM();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Login([Bind("EmailAddress, Password")] LoginVM lvm)
        {
            if (!ModelState.IsValid)
            {
                return View(lvm);
            }
            var user = await _userManager.FindByEmailAsync(lvm.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, lvm.Password);
                if (passwordCheck)
                {
                    var result = await _signinManager.PasswordSignInAsync(user, lvm.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movies");
                    }
                }
                TempData["Error"] = "incorrect password. Please try again.";
                //return Content("Incorrect Credentials. Please try again.");
                return View(lvm);
            }
            TempData["Error"] = "user not found. Plese register or login with a valid email address.";
            //return Content("User not found. Plese register first or login with a valid email address.");
            return View(lvm);

        }
    }
}
