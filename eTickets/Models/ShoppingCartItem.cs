using eTickets.Data.Base;

namespace eTickets.Models
{
    public class ShoppingCartItem : EntityBase
    {
        public Movie Movie { get; set; } = null!;
        public int Quantity { get; set; }
        public string ShoppingCartId { get; set; } = String.Empty;
    }
}
