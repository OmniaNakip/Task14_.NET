using Movie_Management_System.Models;

namespace Movie_Management_System.Services.Interfaces
{
    public interface IMovieService
    {
        Task<List<Movie>> GetAllAsync();
        Task<Movie?> GetByIdAsync(int id);
        Task CreateAsync(Movie movie, int[] actorIds);
        Task UpdateAsync(Movie movie, int[] actorIds);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}