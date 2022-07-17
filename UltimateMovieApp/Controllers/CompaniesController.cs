using AutoMapper;
using Constracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
                _loggerManager.LogInformation($"Company with id:{id} doesn't exist in the database");
                return NotFound();
            }
            else
            {
                var companyDto = _mapper.Map<CompaniesDto>(company);
                return Ok(companyDto);
            }
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

            _repositoryManager.Company.CreateCreate(companyEntity);
            _repositoryManager.Save();

            var companyToReturn = _mapper.Map<CompaniesDto>(companyEntity);

            return CreatedAtRoute("CompanyById", new { id = companyToReturn.Id }, companyToReturn);
        }
    }
}
