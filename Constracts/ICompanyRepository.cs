using Entities.Models;

namespace Constracts
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges);

        public Task<Company?> GetCompanyAsync(Guid companyId, bool trackChange);

        public void CreateCompany(Company company);

        public Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids,bool trackChanges);

        public void DeleteCompany(Company company);
    }
}
