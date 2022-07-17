using AutoMapper;
using Constracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using UltimateMovieApp.ModelBinders;

namespace UltimateMovieApp.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : Controller
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILogger<CompaniesController> _loggerManager;
        private readonly IMapper _mapper;

        public CompaniesController(IRepositoryManager repositoryManager, ILogger<CompaniesController> loggerManager, IMapper mapper)
        {
            this._repositoryManager = repositoryManager;
            this._loggerManager = loggerManager;
            this._mapper = mapper;
        }


        [HttpDelete("{companyId}")]
        public IActionResult DeleteCompany([FromRoute]Guid companyId)
        {
            var company = _repositoryManager.Company.GetCompany(companyId, trackChange: false);
            if (company == null)
            {
                _loggerManager.LogInformation("Company with id: {companyId} doesn't exist in the database.", companyId);
                return NotFound();
            }

            _repositoryManager.Company.DeleteCompany(company);
            _repositoryManager.Save();

            return NoContent();
        }

        public IActionResult GetCompanies()
        {

            var companies = _repositoryManager.Company.GetAllCompanies(trackChanges: false);

            var companiesDto = _mapper.Map<IEnumerable<CompaniesDto>>(companies);

            _loggerManager.LogInformation("Hello from CompContr");


            return Ok(companiesDto);
        }

        [HttpGet("{id}", Name = "companyById")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _repositoryManager.Company.GetCompany(id, trackChange: false);

            if (company == null)
            {
                _loggerManager.LogInformation("Company with id:{id} doesn't exist in the database",id);
                return NotFound();
            }
            else
            {
                var companyDto = _mapper.Map<CompaniesDto>(company);
                return Ok(companyDto);
            }
        }

        [HttpGet("/collection/({ids})", Name ="CompanyCollection")]
        public IActionResult GeCompanytCollection
            ([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids==null)
            {
                _loggerManager.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            var companiesEntities = _repositoryManager.Company.GetByIds(ids, trackChanges: false);

            if (ids.Count()!=companiesEntities.Count())
            {
                _loggerManager.LogError("Some ids are not valid in a collection");
                return NotFound();
            }

            var campaniesForReturn = _mapper.Map<IEnumerable<CompaniesDto>>(companiesEntities);

            return Ok(campaniesForReturn);
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if (company==null)
            {
                _loggerManager.LogInformation("CompanyForCreationDto object send from client is null");
                return BadRequest("CompanyForCreationDto object is null");
            }

            var companyEntity = _mapper.Map<Company>(company);

            _repositoryManager.Company.CreateCompany(companyEntity);
            _repositoryManager.Save();

            var companyToReturn = _mapper.Map<CompaniesDto>(companyEntity);

            return CreatedAtRoute("CompanyById", new { id = companyToReturn.Id }, companyToReturn);
        }

        [HttpPost("collection")]
        public IActionResult CreateCompanyCollection([FromBody] IEnumerable<CompanyForCreationDto> companyCollection)
        {
            if (companyCollection == null)
            {
                _loggerManager.LogError("Company collection sent from client is null");
                return BadRequest("Company collection is null");
            }

            var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);

            foreach (var companyEntity in companyEntities)
            {
                _repositoryManager.Company.CreateCompany(companyEntity);
            }

            _repositoryManager.Save();

            var companyCollectionToReturn = _mapper.Map<IEnumerable<CompaniesDto>>(companyEntities);
            var ids = string.Join(",", companyCollectionToReturn.Select(x => x.Id));

            return CreatedAtRoute("CompanyCollection", new { ids }, companyCollectionToReturn);
        }

        [HttpPut("{id}")]
        public IActionResult CompanyUpdate(Guid id, [FromBody] CompanyForUpdateDto company)
        {
            if (company == null)
            {
                _loggerManager.LogError("CompanyForUpdateDto object send from client is null");
                return BadRequest("CompanyForUpdateDto object is null");
            }

            var companyEntite = _repositoryManager.Company.GetCompany(id, trackChange: true);

            if (companyEntite == null)
            {
                _loggerManager.LogInformation("Complany with id: {id} doesn't exist in the database", id);
                return NotFound();
            }

            _mapper.Map(company, companyEntite);
            _repositoryManager.Save();

            return NoContent();
        }

    }
}
