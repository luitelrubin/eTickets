using eTickets.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models;

public class Movie
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string ImageUrl { get; set; } = String.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public double Price { get; set; }
    public MovieCategory MovieCategory { get; set; }

    public int ProducerId { get; set; }
    public Producer Producer { get; set; } = null!;

    public int CinemaId { get; set; }
    public Cinema Cinema { get; set; } = null!;

    public List<ActorMovie> ActorsMovies { get; set; } = null!;
}
