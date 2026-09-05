using Microsoft.AspNetCore.Mvc;
using Movie_Management_System.Models;
using Movie_Management_System.Services.Interfaces;

namespace Movie_Management_System.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ActorsController : Controller
    {
        private readonly IActorService _service;

        public ActorsController(IActorService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var actor = await _service.GetByIdAsync(id.Value);

            if (actor == null)
                return NotFound();

            return View(actor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Actor actor)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(actor);

                return RedirectToAction(nameof(Index));
            }

            return View(actor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var actor = await _service.GetByIdAsync(id.Value);

            if (actor == null)
                return NotFound();

            return View(actor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Actor actor)
        {
            if (id != actor.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(actor);

                return RedirectToAction(nameof(Index));
            }

            return View(actor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var actor = await _service.GetByIdAsync(id.Value);

            if (actor == null)
                return NotFound();

            return View(actor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}