using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_Management_System.Data;

namespace Movie_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.MoviesCount = await _context.Movies.CountAsync();
            ViewBag.ActorsCount = await _context.Actors.CountAsync();
            ViewBag.CinemasCount = await _context.Cinemas.CountAsync();
            ViewBag.CategoriesCount = await _context.Categories.CountAsync();

            ViewBag.RecentMovies = await _context.Movies
                .Include(m => m.Category)
                .Include(m => m.Cinema)
                .OrderByDescending(m => m.DateTime)
                .Take(5)
                .ToListAsync();

            ViewBag.Categories = await _context.Categories
                .Include(c => c.Movies)
                .ToListAsync();

            ViewBag.Cinemas = await _context.Cinemas
                .Include(c => c.Movies)
                .ToListAsync();

            return View();
        }
    }
}