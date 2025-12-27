using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext _context;
        public OrdersService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetOrderByUserIdAsync(string userId)
        {
            var userOrders = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Movie).Where(uo => uo.UserId == userId).ToListAsync();
            return userOrders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmail)
        {

            var order = new Order
            {
                UserId = userId,
                Email = userEmail,
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    MovieId = item.Movie.Id,
                    Price = item.Movie.Price,
                    Quantity = item.Quantity
                };
                await _context.OrderItems.AddAsync(orderItem);
                await _context.SaveChangesAsync();
            }

        }
    }
}
