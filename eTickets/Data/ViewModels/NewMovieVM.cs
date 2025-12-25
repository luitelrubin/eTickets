using eTickets.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models;

public class NewMovieVM
{
    [Display(Description = "Movie Name")]
    [Required(ErrorMessage = "*Required field")]
    public string Name { get; set; } = String.Empty;

    [Display(Description = "Movie Description")]
    [Required(ErrorMessage = "*Required field")]
    public string Description { get; set; } = String.Empty;

    [Display(Name = "Movie Poster URL")]
    [Required(ErrorMessage = "*Required field")]
    public string ImageUrl { get; set; } = String.Empty;

    [Display(Description = "Start Date")]
    [Required(ErrorMessage = "*Required field")]
    public DateOnly StartDate { get; set; }

    [Display(Description = "End Date")]
    [Required(ErrorMessage = "*Required field")]
    public DateOnly EndDate { get; set; }

    [Display(Description = "Price in $")]
    [Required(ErrorMessage = "*Required field")]
    public double Price { get; set; }

    public MovieCategory MovieCategory { get; set; }
    public int DirectorId { get; set; }
    public int CinemaId { get; set; }
    public List<int> ActorsIds { get; set; } = new List<int>();
}
