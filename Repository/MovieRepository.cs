using Constracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class MovieRepository : RepositoryBase<Movie>, IMovieRepository

    {
        public MovieRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateMovie(Movie movie)
        {
            Create(movie);
        }

        public void DeleteMovie(Movie movie)
        {
            Delete(movie);
        }

        public async Task<PageList<Movie>> GetMoviesAsync(MovieParameters movieParameters, bool trackChanges)
        {
            var movies = await FindAll(trackChanges)
                    //.OrderBy(c => c.Title)
                    //.Skip((movieParameters.PageNumber - 1) * movieParameters.PageSize)
                    //.Take(movieParameters.PageSize)
                    .ToListAsync();

            return PageList<Movie>
                .ToPageList(movies, movieParameters.PageNumber, movieParameters.PageSize);
        }
        public async Task<Movie?> GetMovieByIdsAsync(Guid id, bool trackChanges)
            => await FindByConition(c => c.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        // public async Task<Movie?> getMovieByMovieOwnerIdAsync(string id,)


        public async Task<List<Movie>> GetMoviesByMovieOwnerIdAsync(string movieOwnerId, bool trackChanges)
        => await FindAll(trackChanges)
            .Where(x=>x.MovieOwnerId==movieOwnerId)
            .ToListAsync();
    }
}
