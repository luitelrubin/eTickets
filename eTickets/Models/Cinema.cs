using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Cinema : EntityBase
    {
        [Display(Name = "Logo")]
        [Required(ErrorMessage = "*Required field")]
        public string LogoUrl { get; set; } = String.Empty;
        [Display(Name = "Name")]
        [Required(ErrorMessage = "*Required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Must have at least 3 and at most 50 characters")]
        public string Name { get; set; } = String.Empty;

        [Display(Name = "Description")]
        [Required(ErrorMessage = "*Required field")]
        public string Description { get; set; } = String.Empty;

        // Relationships
        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}
