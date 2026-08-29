using Microsoft.AspNetCore.Mvc;
using Movie_Management_System.Models;
using Movie_Management_System.Services.Interfaces;

namespace Movie_Management_System.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IActorService _actorService;
        private readonly ICinemaService _cinemaService;
        private readonly ICategoryService _categoryService;

        public MoviesController(
            IMovieService movieService,
            IActorService actorService,
            ICinemaService cinemaService,
            ICategoryService categoryService)
        {
            _movieService = movieService;
            _actorService = actorService;
            _cinemaService = cinemaService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetAllAsync();

            return View(movies);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var movie =
                await _movieService.GetByIdAsync(id.Value);

            if (movie == null)
                return NotFound();

            return View(movie);
        }

        public async Task<IActionResult> Create()
        {
            await LoadData();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Movie movie,
            int[] actorIds)
        {
            if (ModelState.IsValid)
            {
                await _movieService.CreateAsync(
                    movie,
                    actorIds);

                return RedirectToAction(nameof(Index));
            }

            await LoadData();

            return View(movie);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var movie =
                await _movieService.GetByIdAsync(id.Value);

            if (movie == null)
                return NotFound();

            await LoadData();

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            Movie movie,
            int[] actorIds)
        {
            if (id != movie.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _movieService.UpdateAsync(
                    movie,
                    actorIds);

                return RedirectToAction(nameof(Index));
            }

            await LoadData();

            return View(movie);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var movie =
                await _movieService.GetByIdAsync(id.Value);

            if (movie == null)
                return NotFound();

            return View(movie);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(
            int? id)
        {
            if (id == null)
                return NotFound();

            await _movieService.DeleteAsync(id.Value);

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadData()
        {
            ViewBag.Categories =
                await _categoryService.GetAllAsync();

            ViewBag.Cinemas =
                await _cinemaService.GetAllAsync();

            ViewBag.Actors =
                await _actorService.GetAllAsync();
        }
    }
}