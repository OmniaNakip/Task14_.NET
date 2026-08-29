using Movie_Management_System.Models;
using Movie_Management_System.Repositories.Interfaces;
using Movie_Management_System.Services.Interfaces;

namespace Movie_Management_System.Services.Implementations
{
    public class CinemaService : ICinemaService
    {
        private readonly ICinemaRepository _repository;

        public CinemaService(ICinemaRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Cinema>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Cinema?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Cinema cinema)
        {
            await _repository.AddAsync(cinema);
        }

        public async Task UpdateAsync(Cinema cinema)
        {
            await _repository.UpdateAsync(cinema);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _repository.ExistsAsync(id);
        }
    }
}