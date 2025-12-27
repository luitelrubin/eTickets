using eTickets.Data.Base;

namespace eTickets.Models
{
    public class OrderItem : EntityBase
    {
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
    }
}
