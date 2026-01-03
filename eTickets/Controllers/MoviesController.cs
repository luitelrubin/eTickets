using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service = null!;
        public MoviesController(IMoviesService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var movies = await _service.GetAllAsync(c => c.Cinema);
            return View(movies);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var movieList = await _service.GetAllAsync(c => c.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredList = movieList.Where(mv => mv.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) || mv.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                return View(nameof(Index), filteredList);
            }
            return View(nameof(Index), movieList);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _service.GetMovieByIdAsync(id);
            return View(movie);
        }
        public async Task<IActionResult> Create()
        {
            var movieDropdownsData = await _service.GetNewMovieDropdownValues();
            if (movieDropdownsData != null)
            {
                ViewBag.CinemaList = new SelectList(movieDropdownsData.CinemaList, "Id", "Name");
                ViewBag.ActorList = new SelectList(movieDropdownsData.ActorList, "Id", "FullName");
                ViewBag.DirectorList = new SelectList(movieDropdownsData.DirectorList, "Id", "FullName");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }
            await _service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _service.GetMovieByIdAsync(id);

            if (movie == null) return View(nameof(NotFound));

            var movieVM = new NewMovieVM()
            {
                Id = movie.Id,
                Name = movie.Name,
                ImageUrl = movie.ImageUrl,
                Description = movie.Description,
                Price = movie.Price,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                CinemaId = movie.CinemaId,
                DirectorId = movie.DirectorId,
                ActorsIds = movie.ActorsMovies.Select(am => am.ActorId).ToList()

            };

            var movieDropdownsData = await _service.GetNewMovieDropdownValues();
            if (movieDropdownsData != null)
            {
                ViewBag.CinemaList = new SelectList(movieDropdownsData.CinemaList, "Id", "Name");
                ViewBag.ActorList = new SelectList(movieDropdownsData.ActorList, "Id", "FullName");
                ViewBag.DirectorList = new SelectList(movieDropdownsData.DirectorList, "Id", "FullName");
            }
            return View(movieVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM movieVM)
        {
            if (id != movieVM.Id) return View(nameof(NotFound));
            var movieDropdownsData = await _service.GetNewMovieDropdownValues();

            if (!ModelState.IsValid)
            {
                if (movieDropdownsData != null)
                {
                    ViewBag.CinemaList = new SelectList(movieDropdownsData.CinemaList, "Id", "Name");
                    ViewBag.ActorList = new SelectList(movieDropdownsData.ActorList, "Id", "FullName");
                    ViewBag.DirectorList = new SelectList(movieDropdownsData.DirectorList, "Id", "FullName");
                }
                return View(movieVM);
            }

            await _service.UpdateMovieAsync(movieVM);
            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}