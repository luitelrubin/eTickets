using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eTickets.Controllers
{
    [Authorize]

    public class OrdersController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;
        public OrdersController(IMoviesService movieService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _moviesService = movieService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }

        public IActionResult ShoppingCart()
        {
            var itemList = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = itemList;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new ArgumentNullException("User id not found");
            string userRole = User.FindFirstValue(ClaimTypes.Role) ?? throw new ArgumentNullException("User role not found");
            var orders = await _ordersService.GetOrderByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }
        public async Task<RedirectToActionResult> AddToShoppingCart(int id)
        {
            var movie = await _moviesService.GetMovieByIdAsync(id);
            if (movie != null)
            {
                _shoppingCart.AddShoppingCartItem(movie);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<RedirectToActionResult> RemoveFromShoppingCart(int id)
        {
            var movie = await _moviesService.GetMovieByIdAsync(id);
            if (movie != null)
            {
                _shoppingCart.RemoveShoppingCartItem(movie);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new ArgumentNullException("User Id not found");
            string userEmail = User.FindFirstValue(ClaimTypes.Email) ?? throw new ArgumentNullException("User Email not found");

            await _ordersService.StoreOrderAsync(items, userId, userEmail);
            await _shoppingCart.ClearShoppingCartAsync();
            return View();
        }
    }
}
