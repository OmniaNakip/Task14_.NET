using Movie_Management_System.Models;
using Movie_Management_System.Repositories.Interfaces;
using Movie_Management_System.Services.Interfaces;

namespace Movie_Management_System.Services.Implementations
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _repository;

        public ActorService(IActorRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Actor>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Actor?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Actor actor)
        {
            await _repository.AddAsync(actor);
        }

        public async Task UpdateAsync(Actor actor)
        {
            await _repository.UpdateAsync(actor);
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