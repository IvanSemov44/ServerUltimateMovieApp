using Constracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CompanyRepository :RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public IEnumerable<Company> GetAllCompanies(bool trackChanges)
            => FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToList();

        public Company? GetCompany(Guid companyId, bool trackChange) 
            => FindByConition(c => c.Id.Equals(companyId), trackChange)
                    .SingleOrDefault();

        public void CreateCreate(Company company) => Create(company);
    }
}
