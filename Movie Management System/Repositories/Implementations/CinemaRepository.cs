using Microsoft.EntityFrameworkCore;
using Movie_Management_System.Data;
using Movie_Management_System.Models;
using Movie_Management_System.Repositories.Interfaces;

namespace Movie_Management_System.Repositories.Implementations
{
    public class CinemaRepository : ICinemaRepository
    {
        private readonly ApplicationDbContext _context;

        public CinemaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cinema>> GetAllAsync()
        {
            return await _context.Cinemas
                .Include(c => c.Movies)
                .ToListAsync();
        }

        public async Task<Cinema?> GetByIdAsync(int id)
        {
            return await _context.Cinemas
                .Include(c => c.Movies)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Cinema cinema)
        {
            _context.Cinemas.Add(cinema);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cinema cinema)
        {
            _context.Cinemas.Update(cinema);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cinema = await _context.Cinemas.FindAsync(id);

            if (cinema != null)
            {
                _context.Cinemas.Remove(cinema);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Cinemas.AnyAsync(c => c.Id == id);
        }
    }
}