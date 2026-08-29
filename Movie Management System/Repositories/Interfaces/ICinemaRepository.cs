using Movie_Management_System.Models;

namespace Movie_Management_System.Repositories.Interfaces
{
    public interface ICinemaRepository
    {
        Task<List<Cinema>> GetAllAsync();
        Task<Cinema?> GetByIdAsync(int id);
        Task AddAsync(Cinema cinema);
        Task UpdateAsync(Cinema cinema);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}