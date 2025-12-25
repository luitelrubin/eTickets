using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service = null!;
        public ActorsController(IActorsService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var actors = await _service.GetAllAsync();
            return View(actors);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureUrl", "FullName", "Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View(actor);
            }
            await _service.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var actor = await _service.GetByIdAsync(id);
            if (actor == null) return View("Not Found");
            return View(actor);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, ProfilePictureUrl, FullName, Bio")] Actor actor)
        {
            if (id != actor.Id)
            {
                return BadRequest("Route id not same as actor Id.");
            }
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.UpdateAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var actor = await _service.GetByIdAsync(id);
            if (actor == null) return View(nameof(NotFound));
            return View(actor);
        }

        public async Task<IActionResult> Delete(int id)
        {

            var actor = await _service.GetByIdAsync(id);
            if (actor == null) return View(nameof(NotFound));
            return View(actor);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var actor = await _service.GetByIdAsync(id);
            if (actor == null) return View(nameof(NotFound));
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}