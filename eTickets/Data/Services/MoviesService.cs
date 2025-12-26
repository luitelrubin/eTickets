using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _context = null!;
        public MoviesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewMovieAsync(NewMovieVM data)
        {
            var movie = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                ImageUrl = data.ImageUrl,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                Price = data.Price,
                CinemaId = data.CinemaId,
                DirectorId = data.DirectorId,
                MovieCategory = data.MovieCategory,

            };
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            foreach (var actorId in data.ActorsIds)
            {
                var am = new ActorMovie()
                {
                    ActorId = actorId,
                    MovieId = movie.Id
                };
                await _context.ActorsMovies.AddAsync(am);
            }
            await _context.SaveChangesAsync();


        }

        public async Task<Movie?> GetMovieByIdAsync(int id)
        {
            var movie = await _context.Movies
                .Include(d => d.Director)
                .Include(c => c.Cinema)
                .Include(am => am.ActorsMovies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);
            return movie;
        }

        public async Task<NewMovieDropdownsVM?> GetNewMovieDropdownValues()
        {
            var response = new NewMovieDropdownsVM()
            {
                ActorList = await _context.Actors.OrderBy(a => a.FullName).ToListAsync(),
                DirectorList = await _context.Directors.OrderBy(a => a.FullName).ToListAsync(),
                CinemaList = await _context.Cinemas.OrderBy(a => a.Name).ToListAsync()
            };
            return response;
        }

        public async Task UpdateMovieAsync(NewMovieVM data)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == data.Id);

            movie.Name = data.Name;
            movie.Description = data.Description;
            movie.ImageUrl = data.ImageUrl;
            movie.StartDate = data.StartDate;
            movie.EndDate = data.EndDate;
            movie.Price = data.Price;
            movie.CinemaId = data.CinemaId;
            movie.DirectorId = data.DirectorId;
            movie.MovieCategory = data.MovieCategory;


            // Get a list movieid->actorid mappings from actor list
            var amList = await _context.ActorsMovies.Where(am => am.MovieId == data.Id).ToListAsync();

            // Clear moviesactors list
            _context.ActorsMovies.RemoveRange(amList);

            await _context.SaveChangesAsync();
            movie.ActorsMovies.Clear();


            foreach (var actorId in data.ActorsIds)
            {
                var am = new ActorMovie()
                {
                    ActorId = actorId,
                    MovieId = data.Id
                };
                movie.ActorsMovies.Add(am);
                await _context.ActorsMovies.AddAsync(am);
            }

            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }
    }
}
