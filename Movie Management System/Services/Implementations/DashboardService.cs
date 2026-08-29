using Microsoft.EntityFrameworkCore;
using Movie_Management_System.Data;
using Movie_Management_System.Services.Interfaces;
using Movie_Management_System.ViewModels;

namespace Movie_Management_System.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardViewModel> GetDashboardAsync()
        {
            var dashboard = new DashboardViewModel
            {
                MoviesCount = await _context.Movies.CountAsync(),
                ActorsCount = await _context.Actors.CountAsync(),
                CinemasCount = await _context.Cinemas.CountAsync(),
                CategoriesCount = await _context.Categories.CountAsync(),

                Categories = await _context.Categories
                    .Include(c => c.Movies)
                    .ToListAsync(),

                Cinemas = await _context.Cinemas
                    .Include(c => c.Movies)
                    .ToListAsync()
            };

            return dashboard;
        }
    }
}