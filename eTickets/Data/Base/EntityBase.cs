using System.ComponentModel.DataAnnotations;

namespace eTickets.Data.Base
{
    public class EntityBase : IEntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
