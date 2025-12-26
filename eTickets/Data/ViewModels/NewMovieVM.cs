using eTickets.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models;

public class NewMovieVM
{
    public int Id { get; set; }

    [Display(Name = "Movie Name")]
    [Required(ErrorMessage = "*Required field")]
    public string Name { get; set; } = String.Empty;

    [Display(Name = "Movie Description")]
    [Required(ErrorMessage = "*Required field")]
    public string Description { get; set; } = String.Empty;

    [Display(Name = "Movie Poster URL")]
    [Required(ErrorMessage = "*Required field")]
    public string ImageUrl { get; set; } = String.Empty;

    [Display(Name = "Movie Start Date")]
    [Required(ErrorMessage = "*Required field")]
    public DateOnly StartDate { get; set; }

    [Display(Name = "Movie End Date")]
    [Required(ErrorMessage = "*Required field")]
    public DateOnly EndDate { get; set; }

    [Display(Name = "Price in $")]
    [Required(ErrorMessage = "*Required field")]
    public double Price { get; set; }

    [Display(Name = "Select a Movie Category")]
    [Required(ErrorMessage = "*Required field")]
    public MovieCategory MovieCategory { get; set; }

    [Display(Name = "Select a Movie Director")]
    [Required(ErrorMessage = "*Required field")]
    public int DirectorId { get; set; }

    [Display(Name = "Select a Cinema")]
    [Required(ErrorMessage = "*Required field")]
    public int CinemaId { get; set; }

    [Display(Name = "Select Movie Actor(s)")]
    [Required(ErrorMessage = "*Required field")]
    public List<int> ActorsIds { get; set; } = new List<int>();
}
