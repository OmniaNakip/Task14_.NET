using Movie_Management_System.Models;
using Movie_Management_System.Repositories.Interfaces;
using Movie_Management_System.Services.Interfaces;

namespace Movie_Management_System.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorRepository _actorRepository;

        public MovieService(
            IMovieRepository movieRepository,
            IActorRepository actorRepository)
        {
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
        }

        public async Task<List<Movie>> GetAllAsync()
        {
            return await _movieRepository.GetAllAsync();
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _movieRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Movie movie, int[] actorIds)
        {
            var actors = new List<Actor>();

            foreach (var actorId in actorIds)
            {
                var actor = await _actorRepository.GetByIdAsync(actorId);

                if (actor != null)
                {
                    actors.Add(actor);
                }
            }

            movie.Actors = actors;

            await _movieRepository.AddAsync(movie);
        }

        public async Task UpdateAsync(Movie movie, int[] actorIds)
        {
            var existingMovie =
                await _movieRepository.GetByIdAsync(movie.Id);

            if (existingMovie == null)
                return;

            existingMovie.Name = movie.Name;
            existingMovie.Description = movie.Description;
            existingMovie.Price = movie.Price;
            existingMovie.Status = movie.Status;
            existingMovie.DateTime = movie.DateTime;
            existingMovie.MainImg = movie.MainImg;
            existingMovie.SubImages = movie.SubImages;
            existingMovie.CategoryId = movie.CategoryId;
            existingMovie.CinemaId = movie.CinemaId;

            existingMovie.Actors.Clear();

            foreach (var actorId in actorIds)
            {
                var actor =
                    await _actorRepository.GetByIdAsync(actorId);

                if (actor != null)
                {
                    existingMovie.Actors.Add(actor);
                }
            }

            await _movieRepository.UpdateAsync(existingMovie);
        }

        public async Task DeleteAsync(int id)
        {
            await _movieRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _movieRepository.ExistsAsync(id);
        }
    }
}