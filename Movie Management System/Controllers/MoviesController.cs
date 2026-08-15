using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_Management_System.Data;
using Movie_Management_System.Models;

public class MoviesController : Controller
{
    private readonly ApplicationDbContext _context;

    public MoviesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Movies
    public async Task<IActionResult> Index()
    {
        var movies = await _context.Movies
            .Include(m => m.Category)
            .Include(m => m.Cinema)
            .Include(m => m.Actors)
            .ToListAsync();

        return View(movies);
    }

    // GET: Movies/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movies
            .Include(m => m.Category)
            .Include(m => m.Cinema)
            .Include(m => m.Actors)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // GET: Movies/Create
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();
        ViewBag.Cinemas = await _context.Cinemas.ToListAsync();
        ViewBag.Actors = await _context.Actors.ToListAsync();

        return View();
    }

    // POST: Movies/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Movie movie, int[] actorIds)
    {
        if (ModelState.IsValid)
        {
            movie.Actors = await _context.Actors
                .Where(a => actorIds.Contains(a.Id))
                .ToListAsync();

            movie.SubImages ??= new List<string>();

            _context.Movies.Add(movie);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        ViewBag.Categories = await _context.Categories.ToListAsync();
        ViewBag.Cinemas = await _context.Cinemas.ToListAsync();
        ViewBag.Actors = await _context.Actors.ToListAsync();

        return View(movie);
    }

    // GET: Movies/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movies
            .Include(m => m.Actors)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            return NotFound();
        }

        ViewBag.Categories = await _context.Categories.ToListAsync();
        ViewBag.Cinemas = await _context.Cinemas.ToListAsync();
        ViewBag.Actors = await _context.Actors.ToListAsync();

        return View(movie);
    }

    // POST: Movies/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Movie movie, int[] actorIds)
    {
        if (id != movie.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var existingMovie = await _context.Movies
                .Include(m => m.Actors)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (existingMovie == null)
            {
                return NotFound();
            }

            existingMovie.Name = movie.Name;
            existingMovie.Description = movie.Description;
            existingMovie.Price = movie.Price;
            existingMovie.Status = movie.Status;
            existingMovie.DateTime = movie.DateTime;
            existingMovie.MainImg = movie.MainImg;
            existingMovie.CategoryId = movie.CategoryId;
            existingMovie.CinemaId = movie.CinemaId;
            existingMovie.SubImages = movie.SubImages;

            existingMovie.Actors.Clear();

            var selectedActors = await _context.Actors
                .Where(a => actorIds.Contains(a.Id))
                .ToListAsync();

            foreach (var actor in selectedActors)
            {
                existingMovie.Actors.Add(actor);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        ViewBag.Categories = await _context.Categories.ToListAsync();
        ViewBag.Cinemas = await _context.Cinemas.ToListAsync();
        ViewBag.Actors = await _context.Actors.ToListAsync();

        return View(movie);
    }

    // GET: Movies/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movies
            .Include(m => m.Category)
            .Include(m => m.Cinema)
            .Include(m => m.Actors)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: Movies/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var movie = await _context.Movies.FindAsync(id);

        if (movie != null)
        {
            _context.Movies.Remove(movie);
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(int id)
    {
        return _context.Movies.Any(e => e.Id == id);
    }
}