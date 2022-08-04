using Entities.Models;
using Entities.RequestFeatures;

namespace Constracts
{
    public interface IMovieRepository
    {
        public Task<PageList<Movie>> GetMoviesAsync(MovieParameters movieParameters, bool trackChanges);

        public Task<Movie?> GetMovieByIdsAsync(Guid movieId, bool trackChange);

        public Task<List<Movie?>> GetMoviesByMovieOwnerIdAsync(string movieOwnerId, bool trackChanges);

        public void CreateMovie(Movie movie);

        public void DeleteMovie(Movie movie);
    }
}
