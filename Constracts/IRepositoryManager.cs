namespace Constracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }

        IEmployeeRepository Employee { get; }

        IMovieRepository Movie { get; }

       Task SaveAsync();
    }
}
