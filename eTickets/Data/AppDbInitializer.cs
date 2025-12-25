using eTickets.Data.Enums;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>() ?? throw new NullReferenceException("Service can't be null");
                context.Database.EnsureCreated();

                // Cinema
                if (!context.Cinemas.Any())
                {
                    context.Cinemas.AddRange(new List<Cinema>()
                    {
                        new Cinema()
                        {
                            Name = "Cinema 1",
                            LogoUrl = "img/cinemas/cin-1.jpg",
                            Description = "This is the description of the first cinema"
                        },
                        new Cinema()
                        {
                            Name = "Cinema 2",
                            LogoUrl = "img/cinemas/cin-2.jpg",
                            Description = "This is the description of the first cinema"
                        },
                        new Cinema()
                        {
                            Name = "Cinema 3",
                            LogoUrl = "img/cinemas/cin-3.jpg",
                            Description = "This is the description of the first cinema"
                        },
                        new Cinema()
                        {
                            Name = "Cinema 4",
                            LogoUrl = "img/cinemas/cin-4.jpg",
                            Description = "This is the description of the first cinema"
                        },
                        new Cinema()
                        {
                            Name = "Cinema 5",
                            LogoUrl = "img/cinemas/cin-5.jpg",
                            Description = "This is the description of the first cinema"
                        },
                    });
                    context.SaveChanges();
                }
                // Actors
                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor()
                        {
                            FullName = "Actor 1",
                            Bio = "This is the Bio of the first actor",
                            ProfilePictureUrl = "img/actors/act-1.jpg"

                        },
                        new Actor()
                        {
                            FullName = "Actor 2",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureUrl = "img/actors/act-2.jpg"
                        },
                        new Actor()
                        {
                            FullName = "Actor 3",
                            Bio = "This is the Bio of the third actor",
                            ProfilePictureUrl = "img/actors/act-3.jpg"
                        },
                        new Actor()
                        {
                            FullName = "Actor 4",
                            Bio = "This is the Bio of the fourth actor",
                            ProfilePictureUrl = "img/actors/act-4.jpg"
                        },
                        new Actor()
                        {
                            FullName = "Actor 5",
                            Bio = "This is the Bio of the fifth actor",
                            ProfilePictureUrl = "img/actors/act-5.jpg"
                        }
                    });
                    context.SaveChanges();
                }
                // Producers
                if (!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Director>()
                    {
                        new Director()
                        {
                            FullName = "Director 1",
                            Bio = "This is the Bio of the first director",
                            ProfilePictureUrl = "img/directors/dir-1.jpg"

                        },
                        new Director()
                        {
                            FullName = "Director 2",
                            Bio = "This is the Bio of the second director",
                            ProfilePictureUrl = "img/directors/dir-2.jpg"
                        },
                        new Director()
                        {
                            FullName = "Director 3",
                            Bio = "This is the Bio of the third director",
                            ProfilePictureUrl = "img/directors/dir-3.jpg"
                        },
                        new Director()
                        {
                            FullName = "Director 4",
                            Bio = "This is the Bio of the fourth director",
                            ProfilePictureUrl = "img/directors/dir-4.jpg"
                        },
                        new Director()
                        {
                            FullName = "Director 5",
                            Bio = "This is the Bio of the fifth director",
                            ProfilePictureUrl = "img/directors/dir-5.jpg"
                        }
                    });
                    context.SaveChanges();
                }
                // Movies
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                         new Movie()
                        {
                            Name = "Grinch",
                            Description = "This is the Grinch movie description",
                            Price = 39.50,
                            ImageUrl = "img/movies/mv-1.jpg",
                            StartDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-10),
                            EndDate = DateOnly.FromDateTime(DateTime.Now).AddDays(10),
                            CinemaId = 3,
                            DirectorId = 3,
                            MovieCategory = MovieCategory.Documentary
                        },
                        new Movie()
                        {
                            Name = "The Greatest Showman",
                            Description = "This is The Greatest Showman movie description",
                            Price = 29.50,
                            ImageUrl = "img/movies/mv-2.jpg",
                            StartDate = DateOnly.FromDateTime(DateTime.Now),
                            EndDate = DateOnly.FromDateTime(DateTime.Now).AddDays(3),
                            CinemaId = 1,
                            DirectorId = 1,
                            MovieCategory = MovieCategory.Action
                        },
                        new Movie()
                        {
                            Name = "Roofman",
                            Description = "This is the Roofman movie description",
                            Price = 39.50,
                            ImageUrl = "img/movies/mv-3.jpg",
                            StartDate = DateOnly.FromDateTime(DateTime.Now),
                            EndDate = DateOnly.FromDateTime(DateTime.Now).AddDays(7),
                            CinemaId = 4,
                            DirectorId = 4,
                            MovieCategory = MovieCategory.Horror
                        },
                        new Movie()
                        {
                            Name = "Home Alone",
                            Description = "This is the Home Alone movie description",
                            Price = 39.50,
                            ImageUrl = "img/movies/mv-4.jpg",
                            StartDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-10),
                            EndDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-5),
                            CinemaId = 1,
                            DirectorId = 2,
                            MovieCategory = MovieCategory.Documentary
                        },
                        new Movie()
                        {
                            Name = "Oh What Fun",
                            Description = "This is the Oh What Fun movie description",
                            Price = 39.50,
                            ImageUrl = "img/movies/mv-5.jpg",
                            StartDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-10),
                            EndDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-2),
                            CinemaId = 1,
                            DirectorId  = 3,
                            MovieCategory = MovieCategory.Cartoon
                        },
                        new Movie()
                        {
                            Name = "One Battle After Another",
                            Description = "This is the One Battle After Another movie description",
                            Price = 39.50,
                            ImageUrl = "img/movies/mv-6.jpg",
                            StartDate = DateOnly.FromDateTime(DateTime.Now).AddDays(3),
                            EndDate = DateOnly.FromDateTime(DateTime.Now).AddDays(20),
                            CinemaId = 1,
                            DirectorId  = 5,
                            MovieCategory = MovieCategory.Drama
                        }
                    });
                    context.SaveChanges();
                }
                // Actors and Movies
                if (!context.ActorsMovies.Any())
                {
                    context.ActorsMovies.AddRange(new List<ActorMovie>()
                    {
                        new ActorMovie()
                        {
                            ActorId = 1,
                            MovieId = 1
                        },
                        new ActorMovie()
                        {
                            ActorId = 3,
                            MovieId = 1
                        },
                         new ActorMovie()
                        {
                            ActorId = 1,
                            MovieId = 2
                        },
                         new ActorMovie()
                        {
                            ActorId = 4,
                            MovieId = 2
                        },
                        new ActorMovie()
                        {
                            ActorId = 1,
                            MovieId = 3
                        },
                        new ActorMovie()
                        {
                            ActorId = 2,
                            MovieId = 3
                        },
                        new ActorMovie()
                        {
                            ActorId = 5,
                            MovieId = 3
                        },
                        new ActorMovie()
                        {
                            ActorId = 2,
                            MovieId = 4
                        },
                        new ActorMovie()
                        {
                            ActorId = 3,
                            MovieId = 4
                        },
                        new ActorMovie()
                        {
                            ActorId = 4,
                            MovieId = 4
                        },
                        new ActorMovie()
                        {
                            ActorId = 2,
                            MovieId = 5
                        },
                        new ActorMovie()
                        {
                            ActorId = 3,
                            MovieId = 5
                        },
                        new ActorMovie()
                        {
                            ActorId = 4,
                            MovieId = 5
                        },
                        new ActorMovie()
                        {
                            ActorId = 5,
                            MovieId = 5
                        },
                        new ActorMovie()
                        {
                            ActorId = 3,
                            MovieId = 6
                        },
                        new ActorMovie()
                        {
                            ActorId = 4,
                            MovieId = 6
                        },
                        new ActorMovie()
                        {
                            ActorId = 5,
                            MovieId = 6
                        },
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}

