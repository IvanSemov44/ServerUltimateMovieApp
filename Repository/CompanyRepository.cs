using Constracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CompanyRepository :RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges)
            => await FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToListAsync();

        public async Task<Company?> GetCompanyAsync(Guid companyId, bool trackChange) 
            => await FindByConition(c => c.Id.Equals(companyId), trackChange)
                    .SingleOrDefaultAsync();

        public void CreateCompany(Company company) => Create(company);

        public async Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        => await FindByConition(c => ids.Contains(c.Id), trackChanges).ToListAsync();

        public void DeleteCompany(Company company)
        {
            Delete(company);
        }
    }
}
