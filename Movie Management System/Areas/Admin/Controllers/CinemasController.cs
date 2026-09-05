using Microsoft.AspNetCore.Mvc;
using Movie_Management_System.Models;
using Movie_Management_System.Services.Interfaces;

namespace Movie_Management_System.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CinemasController : Controller
    {
        private readonly ICinemaService _service;

        public CinemasController(ICinemaService service)
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

            var cinema = await _service.GetByIdAsync(id.Value);

            if (cinema == null)
                return NotFound();

            return View(cinema);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(cinema);

                return RedirectToAction(nameof(Index));
            }

            return View(cinema);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var cinema = await _service.GetByIdAsync(id.Value);

            if (cinema == null)
                return NotFound();

            return View(cinema);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            Cinema cinema)
        {
            if (id != cinema.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(cinema);

                return RedirectToAction(nameof(Index));
            }

            return View(cinema);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var cinema = await _service.GetByIdAsync(id.Value);

            if (cinema == null)
                return NotFound();

            return View(cinema);
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