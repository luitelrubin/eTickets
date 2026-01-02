using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Director : EntityBase
    {
        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "*Required field")]
        public string ProfilePictureUrl { get; set; } = String.Empty;
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "*Required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must be at least 3 chars and at most 50 chars long")]
        public string FullName { get; set; } = String.Empty;
        [Display(Name = "Biography")]
        [Required(ErrorMessage = "*Required field")]
        public string Bio { get; set; } = String.Empty;
        [Display(Name = "Movies")]
        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}
