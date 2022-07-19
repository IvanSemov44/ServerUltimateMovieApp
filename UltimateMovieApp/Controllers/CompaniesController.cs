using AutoMapper;
using Constracts;
using Entities.DataTransferObjects.Company;
using Entities.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UltimateMovieApp.ActionFilters;
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
        [ServiceFilter(typeof(ValidateCompanyExistAttribute))]
        public async Task<IActionResult> DeleteCompany([FromRoute] Guid companyId)
        {
            var company = HttpContext.Items["company"] as Company;

            _repositoryManager.Company.DeleteCompany(company);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {

            var companies = await _repositoryManager.Company.GetAllCompaniesAsync(trackChanges: false);

            var companiesDto = _mapper.Map<IEnumerable<CompaniesDto>>(companies);

            _loggerManager.LogInformation("Hello from CompContr");


            return Ok(companiesDto);
        }

        [HttpGet("{id}", Name = "companyById")]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var company = await _repositoryManager.Company.GetCompanyAsync(id, trackChange: false);

            if (company == null)
            {
                _loggerManager.LogInformation("Company with id:{id} doesn't exist in the database", id);
                return NotFound();
            }
            else
            {
                var companyDto = _mapper.Map<CompaniesDto>(company);
                return Ok(companyDto);
            }
        }

        [HttpGet("/collection/({ids})", Name = "CompanyCollection")]
        public async Task<IActionResult> GeCompanytCollection
            ([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _loggerManager.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            var companiesEntities = await _repositoryManager.Company.GetByIdsAsync(ids, trackChanges: false);

            if (ids.Count() != companiesEntities.Count())
            {
                _loggerManager.LogError("Some ids are not valid in a collection");
                return NotFound();
            }

            var campaniesForReturn = _mapper.Map<IEnumerable<CompaniesDto>>(companiesEntities);

            return Ok(campaniesForReturn);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if (company == null)
            {
                _loggerManager.LogInformation("CompanyForCreationDto object send from client is null");
                return BadRequest("CompanyForCreationDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _loggerManager.LogError("Invalid model state for the CompanyForCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var companyEntity = _mapper.Map<Company>(company);

            _repositoryManager.Company.CreateCompany(companyEntity);
            await _repositoryManager.SaveAsync();

            var companyToReturn = _mapper.Map<CompaniesDto>(companyEntity);

            return CreatedAtRoute("CompanyById", new { id = companyToReturn.Id }, companyToReturn);
        }

        [HttpPost("collection")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCompanyCollection([FromBody] IEnumerable<CompanyForCreationDto> companyCollection)
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

            await _repositoryManager.SaveAsync();

            var companyCollectionToReturn = _mapper.Map<IEnumerable<CompaniesDto>>(companyEntities);
            var ids = string.Join(",", companyCollectionToReturn.Select(x => x.Id));

            return CreatedAtRoute("CompanyCollection", new { ids }, companyCollectionToReturn);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateCompanyExistAttribute))]
        public async Task<IActionResult> CompanyUpdate(Guid id, [FromBody] CompanyForUpdateDto company)
        {

            var companyEntity = HttpContext.Items["company"] as Company; 

            _mapper.Map(company, companyEntity);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }

    }
}
