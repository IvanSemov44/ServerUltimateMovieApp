using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constracts
{
    public interface IMovieRepository
    {
        public Task<IEnumerable<Movie>> GetAllMovieAsync(bool trackChanges);

        public Task<Movie?> GetMovieAsync(Guid movieId, bool trackChange);

        public void CreateMovie(Movie movie);

        public Task<IEnumerable<Movie>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);

        public void DeleteMovie(Movie movie);
    }
}
