using AutoMapper;
using Constracts;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace UltimateMovieApp.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : Controller
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public CompaniesController(IRepositoryManager repositoryManager,ILoggerManager loggerManager, IMapper mapper)
        {
            this._repositoryManager = repositoryManager;
            this._loggerManager = loggerManager;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCompanies()
        {
            try
            {
                var companies = _repositoryManager.Company.GetAllCompanies(trackChanges: false);

                var companiesDto = _mapper.Map<IEnumerable<CompaniesDto>>(companies);

                return Ok(companiesDto);
            }
            catch (Exception ex)
            {

                _loggerManager.LogError($"Something went wrong in the {nameof(GetCompanies)} action{ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
