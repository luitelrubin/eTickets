using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
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
            string userId = "testUserId";
            var orders = await _ordersService.GetOrderByUserIdAsync(userId);
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

        [HttpPost]
        public async Task<IActionResult> CompleteOrder([FromBody] CompleteOrderRequestVM request)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            const string userId = "testUserId";
            const string userEmail = "test@etickets.com";

            await _ordersService.StoreOrderAsync(items, userId, userEmail);
            await _shoppingCart.ClearShoppingCartAsync();
            return View("OrderCompleted");
        }
    }
}
