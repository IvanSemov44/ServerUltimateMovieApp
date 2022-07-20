using AutoMapper;
using Constracts;
using Entities.DataTransferObjects.Employee;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using UltimateMovieApp.ActionFilters;

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
        public async Task<IActionResult> GetEmployeesFromCompanyAsync(Guid companyId)
        {
            var company =await _repositoryManager.Company.GetCompanyAsync(companyId, trackChange: false);


            if (company == null)
            {
                _logger.LogInformation("Company with id: {companyId} doesn't exist n the database", companyId);
                return NotFound();
            }

            var employeesFromDb = await _repositoryManager.Employee.GetEmployeesAsync(companyId, trackChanges: false);

            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);

            return Ok(employeesDto);
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEmplayeeForCompanyFilter))]
        public IActionResult GetEmployeeFromCompany(Guid companyId, Guid id)
        {
            var employeeDB = HttpContext.Items["employee"] as Employee;

            var employeeDto = _mapper.Map<EmployeeDto>(employeeDB);

            return Ok(employeeDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employee)
        {
            
            var company =await _repositoryManager.Company.GetCompanyAsync(companyId, trackChange: false);
            if (company==null)
            {
                _logger.LogInformation("Company with id: {companyId} doesn't exist in the database",companyId);
                return NotFound();
            }

            var employeeEntity = _mapper.Map<Employee>(employee);

            _repositoryManager.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            await _repositoryManager.SaveAsync();

            var employeeForReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return CreatedAtRoute("GetEmployeeForCompany", new {companyId,id= employeeForReturn.Id}, employeeForReturn);

        }
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEmplayeeForCompanyFilter))]
        public async Task<IActionResult> DeleteEmployeeForCompany([FromRoute] Guid companyId, Guid id)
        {

            var employeeForCompany = HttpContext.Items["employee"] as Employee;

            _repositoryManager.Employee.DeleteEmployee(employeeForCompany);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEmplayeeForCompanyFilter))]
        public async Task<IActionResult> UpdateEmployeeForCompany
            (Guid companyId,Guid id, [FromBody] EmployeeForUpdateDto employee)
        {
            var employeeEntite = HttpContext.Items["employee"] as Employee;

            _mapper.Map(employee, employeeEntite);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        [ServiceFilter(typeof(ValidateEmplayeeForCompanyFilter))]
        public async Task<IActionResult> PartiallyUpdateEmployeeForCompany
            (Guid companyId,Guid id, [FromBody]JsonPatchDocument<EmployeeForUpdateDto> pachDoc)
        {
            if (pachDoc==null)
            {
                _logger.LogError("pachDoc object send from client is null");
                return BadRequest("pachDoc object is null");
            }
            var employeeEntite = HttpContext.Items["employee"] as Employee;

            var employeeToPach = _mapper.Map<EmployeeForUpdateDto>(employeeEntite);

            pachDoc.ApplyTo(employeeToPach, ModelState);

            TryValidateModel(employeeToPach);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the pach Document.");
                return UnprocessableEntity(ModelState);
            }



            _mapper.Map(employeeToPach, employeeEntite);

            await _repositoryManager.SaveAsync();

            return NoContent();
        }

    }
}
