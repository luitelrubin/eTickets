using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]

    public class DirectorsController : Controller
    {
        private readonly IDirectorsService _service = null!;
        public DirectorsController(IDirectorsService service)
        {
            _service = service;
        }
        [AllowAnonymous]

        public async Task<IActionResult> Index()
        {
            var directors = await _service.GetAllAsync();
            return View(directors);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureUrl", "FullName", "Bio")] Director director)
        {
            if (!ModelState.IsValid)
            {
                return View(director);
            }
            await _service.AddAsync(director);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var director = await _service.GetByIdAsync(id);
            if (director == null) return View(nameof(NotFound));
            return View(director);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, ProfilePictureUrl, FullName, Bio")] Director director)
        {
            if (id != director.Id)
            {
                return RedirectToAction(nameof(NotFound));
            }
            if (!ModelState.IsValid)
            {
                return View(director);
            }
            await _service.UpdateAsync(director);
            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]

        public async Task<IActionResult> Details(int id)
        {
            var director = await _service.GetByIdAsync(id);
            if (director == null) return View(nameof(NotFound));
            return View(director);
        }

        public async Task<IActionResult> Delete(int id)
        {

            var director = await _service.GetByIdAsync(id);
            if (director == null) return View(nameof(NotFound));
            return View(director);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var director = await _service.GetByIdAsync(id);
            if (director == null) return View(nameof(NotFound));
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
