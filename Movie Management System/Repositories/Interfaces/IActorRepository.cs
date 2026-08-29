using Movie_Management_System.Models;

namespace Movie_Management_System.Repositories.Interfaces
{
    public interface IActorRepository
    {
        Task<List<Actor>> GetAllAsync();
        Task<Actor?> GetByIdAsync(int id);
        Task AddAsync(Actor actor);
        Task UpdateAsync(Actor actor);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}