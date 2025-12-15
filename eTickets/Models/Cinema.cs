using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }
        public string LogoUrl { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;

        // Relationships
        public List<Movie> Movies { get; set; } = null!;
    }
}
