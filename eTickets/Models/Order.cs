using eTickets.Data.Base;

namespace eTickets.Models
{
    public class Order : EntityBase
    {
        public string Email { get; set; } = String.Empty;
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
