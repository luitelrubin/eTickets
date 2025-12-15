using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        public string ProfilePictureUrl { get; set; } = String.Empty;

        public string FullName { get; set; } = String.Empty;

        public string Bio { get; set; } = String.Empty;

        // Relationships
        public List<ActorMovie> ActorsMovies { get; set; } = null!;
    }
}
