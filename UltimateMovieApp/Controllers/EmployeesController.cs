using AutoMapper;
using Constracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace UltimateMovieApp.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IRepositoryManager repositoryManager,
            ILogger<EmployeesController> logger, IMapper mapper)
        {
            this._mapper = mapper;
            this._repositoryManager = repositoryManager;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult GetEmployeesFromCompany(Guid companyId)
        {
            var company = _repositoryManager.Company.GetCompany(companyId, trackChange: false);


            if (company == null)
            {
                _logger.LogInformation("Company with id: {companyId} doesn't exist n the database", companyId);
                return NotFound();
            }

            var employeesFromDb = _repositoryManager.Employee.GetEmployees(companyId, trackChanges: false);

            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);

            return Ok(employeesDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeFromCompany(Guid companyId, Guid id)
        {
            var company = _repositoryManager.Company.GetCompany(companyId, false);

            if (company==null)
            {
                _logger.LogInformation("Company with id: {companyId} doesn't exist in the database", companyId);
                return NotFound();
            }

            var employeeDB = _repositoryManager.Employee.GetEmployee(companyId, id, trackChanges: false);
            if (employeeDB==null)
            {
                _logger.LogInformation("Employee with id: {id}  doesn't exist in the database", id);
                return NotFound();
            }

            var employeeDto = _mapper.Map<EmployeeDto>(employeeDB);

            return Ok(employeeDto);
        }
    }
}
