using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace eTickets.Data.Cart
{
    public class ShoppingCart
    {
        private readonly AppDbContext _context;
        public string ShoppingCartId { get; set; } = String.Empty;
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            var session = services.GetRequiredService<IHttpContextAccessor>().HttpContext?.Session ?? throw new ArgumentNullException("HTTPContext is required for shopping cart");
            var appDbContext = services.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShoppingCart(appDbContext) { ShoppingCartId = cartId };
        }
        public void AddShoppingCartItem(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(item => item.Movie.Id == movie.Id && item.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem == null) //Add
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Quantity = 1
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Quantity += 1;
            }
            _context.SaveChanges();
        }
        public void RemoveShoppingCartItem(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(item => item.Movie.Id == movie.Id && item.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Quantity > 1)
                {
                    shoppingCartItem.Quantity -= 1;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();
        }
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            if (ShoppingCartItems.IsNullOrEmpty())
            {
                ShoppingCartItems = _context.ShoppingCartItems.Where(item => item.ShoppingCartId == ShoppingCartId).Include(m => m.Movie).ToList();
            }
            return ShoppingCartItems;
        }
        public double GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems.Where(item => item.ShoppingCartId == ShoppingCartId).Select(item => item.Quantity * item.Movie.Price).Sum();
            return total;
        }

        public async Task ClearShoppingCartAsync()
        {
            var items = _context.ShoppingCartItems.Where(item => item.ShoppingCartId == ShoppingCartId).Include(n => n.Movie).ToList();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
