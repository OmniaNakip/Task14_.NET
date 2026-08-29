using Movie_Management_System.Models;

namespace Movie_Management_System.Services.Interfaces
{
    public interface IActorService
    {
        Task<List<Actor>> GetAllAsync();
        Task<Actor?> GetByIdAsync(int id);
        Task CreateAsync(Actor actor);
        Task UpdateAsync(Actor actor);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}