using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]

    public class CinemasController : Controller
    {
        private readonly ICinemasService _service = null!;
        public CinemasController(ICinemasService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var cinemas = await _service.GetAllAsync();
            return View(cinemas);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var cinema = await _service.GetByIdAsync(id);
            if (cinema == null) return View(nameof(NotFound));
            return View(cinema);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("LogoUrl, Name, Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            await _service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var cinema = await _service.GetByIdAsync(id);
            if (cinema == null)
            {
                return View(nameof(NotFound));
            }
            return View(cinema);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, LogoUrl, Name, Description")] Cinema cinema)
        {
            if (id != cinema.Id)
            {
                return View(nameof(NotFound));
            }
            await _service.UpdateAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var cinema = await _service.GetByIdAsync(id);
            if (cinema == null)
            {
                return View(nameof(NotFound));
            }
            return View(cinema);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmedDelete(int id)
        {
            var cinema = await _service.GetByIdAsync(id);
            if (cinema == null)
            {
                return View(nameof(NotFound));
            }
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
