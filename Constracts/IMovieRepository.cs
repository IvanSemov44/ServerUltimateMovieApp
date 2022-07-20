using Entities.Models;

namespace Constracts
{
    public interface IMovieRepository
    {
        public Task<IEnumerable<Movie>> GetAllMovieAsync(bool trackChanges);

        public Task<Movie?> GetMovieByIdsAsync(Guid movieId, bool trackChange);

        public void CreateMovie(Movie movie);

        public void DeleteMovie(Movie movie);
    }
}
