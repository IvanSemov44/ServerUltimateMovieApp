using Entities.Models;

namespace Constracts
{
    public interface ICompanyRepository
    {
        public IEnumerable<Company> GetAllCompanies(bool trackChanges);
        Company? GetCompany(Guid companyId, bool trackChange);

        public void CreateCreate(Company company);
    }
}
