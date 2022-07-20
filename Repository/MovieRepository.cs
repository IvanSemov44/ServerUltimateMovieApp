using Constracts;
using Entities;
using Entities.Models;
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

        public async Task<IEnumerable<Movie>> GetAllMovieAsync(bool trackChanges)
            => await FindAll(trackChanges)
                    .OrderBy(c => c.Year)
                    .ToListAsync();

        public async Task<Movie?> GetMovieByIdsAsync(Guid id, bool trackChanges)
            => await FindByConition(c => c.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
    }
}
