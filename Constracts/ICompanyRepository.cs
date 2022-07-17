using Entities.Models;

namespace Constracts
{
    public interface ICompanyRepository
    {
        public IEnumerable<Company> GetAllCompanies(bool trackChanges);
        Company? GetCompany(Guid companyId, bool trackChange);

        public void CreateCompany(Company company);

        public IEnumerable<Company> GetByIds(IEnumerable<Guid> ids,bool trackChanges);

        public void DeleteCompany(Company company);
    }
}
