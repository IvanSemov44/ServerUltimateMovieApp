using Constracts;
using Entities;
namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private ICompanyRepository? _companyRepository;
        private IEmployeeRepository? _employeeRepository;
        private IMovieRepository? _movieRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            this._repositoryContext = repositoryContext;
        }

        public ICompanyRepository Company
        {
            get
            {
                if (_companyRepository == null)
                    _companyRepository = new CompanyRepository(_repositoryContext);

                return _companyRepository;
            }
        }

        public IEmployeeRepository Employee
        {
            get
            {
                if (_employeeRepository == null)
                    _employeeRepository = new EmployeeRepository(_repositoryContext);

                return _employeeRepository;
            }
        }

        public IMovieRepository Movie
        {
            get
            {
                if (_movieRepository == null)
                    _movieRepository = new MovieRepository(_repositoryContext);

                return _movieRepository;
            }
        }

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
