using eTickets.Data.Base;
using eTickets.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models;

public class Movie : EntityBase
{
    [Display(Name = "Name")]
    [Required(ErrorMessage = "*Required field")]
    public string Name { get; set; } = String.Empty;

    [Display(Name = "Description")]
    [Required(ErrorMessage = "*Required field")]
    public string Description { get; set; } = String.Empty;

    [Display(Name = "Image URL")]
    [Required(ErrorMessage = "*Required field")]
    public string ImageUrl { get; set; } = String.Empty;

    [Display(Name = "Start Date")]
    [Required(ErrorMessage = "*Required field")]
    public DateOnly StartDate { get; set; }

    [Display(Name = "End Date")]
    [Required(ErrorMessage = "*Required field")]
    public DateOnly EndDate { get; set; }

    [Display(Name = "Price")]
    [Required(ErrorMessage = "*Required field")]
    public double Price { get; set; }

    public MovieCategory MovieCategory { get; set; }

    public int DirectorId { get; set; }
    public Director Director { get; set; } = null!;

    public int CinemaId { get; set; }
    public Cinema Cinema { get; set; } = null!;

    public List<ActorMovie> ActorsMovies { get; set; } = new List<ActorMovie>();
}
