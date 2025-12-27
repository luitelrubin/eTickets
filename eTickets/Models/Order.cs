using eTickets.Data.Base;

namespace eTickets.Models
{
    public class Order : EntityBase
    {
        public string Email { get; set; } = String.Empty;
        public string UserId { get; set; } = string.Empty;
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
