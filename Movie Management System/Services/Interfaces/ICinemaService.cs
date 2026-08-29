using Movie_Management_System.Models;

namespace Movie_Management_System.Services.Interfaces
{
    public interface ICinemaService
    {
        Task<List<Cinema>> GetAllAsync();
        Task<Cinema?> GetByIdAsync(int id);
        Task CreateAsync(Cinema cinema);
        Task UpdateAsync(Cinema cinema);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}