using AutoMapper;
using Constracts;
using Entities.DataTransferObjects;
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

        [HttpGet]
        public IActionResult GetCompanies()
        {

            var companies = _repositoryManager.Company.GetAllCompanies(trackChanges: false);

            var companiesDto = _mapper.Map<IEnumerable<CompaniesDto>>(companies);

            _loggerManager.LogInformation("Hello from CompContr");

            throw new Exception("Exception");

            return Ok(companiesDto);

          //  _loggerManager.LogError($"Something went wrong in the {nameof(GetCompanies)} action{ex}");
          //  return StatusCode(500, "Internal server error");

        }
    }
}
