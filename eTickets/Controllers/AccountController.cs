using eTickets.Data;
using eTickets.Data.Cart;
using eTickets.Data.Static;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly AppDbContext _context;
        private readonly ShoppingCart _shoppingCart = null!;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager, ShoppingCart shoppingCart, AppDbContext context)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _context = context;
            _shoppingCart = shoppingCart;
        }
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
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
        public IActionResult Register()
        {
            var response = new RegisterVM();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM rvm)
        {
            if (!ModelState.IsValid) return View(rvm);
            var user = await _userManager.FindByEmailAsync(rvm.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "User exists already, enter another email or try logging in instead";
                return View(rvm);
            }
            var newUser = new ApplicationUser
            {
                FullName = rvm.FullName,
                Email = rvm.EmailAddress,
                UserName = rvm.EmailAddress
            };
            var response = await _userManager.CreateAsync(newUser, rvm.Password);
            if (response.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "User");
            }
            return View("RegistrationCompleted");
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> Logout()
        {
            await _shoppingCart.ClearShoppingCartAsync();
            await _signinManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }
        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }
    }
}
