using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }
        public string ProfilePictureUrl { get; set; } = String.Empty;

        public string FullName { get; set; } = String.Empty;

        public string Bio { get; set; } = String.Empty;

        public List<Movie> Movies { get; set; } = null!;
    }
}
