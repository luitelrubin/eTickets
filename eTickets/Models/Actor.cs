using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Actor : EntityBase
    {
        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "*Required field")]
        public string ProfilePictureUrl { get; set; } = String.Empty;

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "*Required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full name must contain at least 3 and at most 50 characters")]
        public string FullName { get; set; } = String.Empty;

        [Display(Name = "Biography")]
        [Required(ErrorMessage = "*Required field")]
        public string Bio { get; set; } = String.Empty;

        // Relationships
        public List<ActorMovie> ActorsMovies { get; set; } = new List<ActorMovie>();
    }
}
