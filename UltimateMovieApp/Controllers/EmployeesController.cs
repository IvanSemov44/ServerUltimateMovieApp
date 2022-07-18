using AutoMapper;
using Constracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet("{id:Guid}", Name = "GetEmployeeForCompany")]
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

        [HttpPost]
        public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employee)
        {
            if (employee==null)
            {
                _logger.LogInformation("EmployeeForCreationDto object sent from client is null");
                return BadRequest("EmployeeForCreationDto object is null");
            } 

            if (!ModelState.IsValid )
            {
                _logger.LogError("Invalid model state for the EmployeeForCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var company = _repositoryManager.Company.GetCompany(companyId, trackChange: false);
            if (company==null)
            {
                _logger.LogInformation("Company with id: {companyId} doesn't exist in the database",companyId);
                return NotFound();
            }

            var employeeEntity = _mapper.Map<Employee>(employee);

            _repositoryManager.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            _repositoryManager.Save();

            var employeeForReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return CreatedAtRoute("GetEmployeeForCompany", new {companyId,id= employeeForReturn.Id}, employeeForReturn);

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployeeForCompany([FromRoute] Guid companyId, Guid id)
        {
            var company = _repositoryManager.Company.GetCompany(companyId, trackChange: false);
            if (company==null)
            {
                _logger.LogInformation("Company with id: {companyId} doesn't exist in the database", companyId);
                return NotFound();
            }
            var employeeForCompany = _repositoryManager.Employee.GetEmployee(companyId, id,trackChanges: false);

            if (employeeForCompany==null)
            {
                _logger.LogInformation("Employee with id: {id} doesn't exist in the database", id);
                return NotFound();
            }

            _repositoryManager.Employee.DeleteEmployee(employeeForCompany);
            _repositoryManager.Save();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult  UpdateEmployeeForCompany
            (Guid companyId,Guid id, [FromBody] EmployeeForUpdateDto employee)
        {
            if (employee==null)
            {
                _logger.LogError("EmployeeForUpdateDto object sent from client is null");
                return BadRequest("EmployeeForUpdateDto object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForUpdateDto object.");
                return UnprocessableEntity(ModelState);
            }

            var company = _repositoryManager.Company.GetCompany(companyId, trackChange: false);
            if (company==null)
            {
                _logger.LogInformation("Company with id: {companyId} doesn't exist in the database", companyId);
                return NotFound();
            }

            var employeeEntite = _repositoryManager.Employee.GetEmployee(companyId,id,trackChanges: true);

            if (employeeEntite==null)
            {
                _logger.LogInformation("Employes with id: {id} doesn't exist in the database",id);
                return NotFound();
            }

            _mapper.Map(employee, employeeEntite);
            _repositoryManager.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateEmployeeForCompany
            (Guid companyId,Guid id, [FromBody]JsonPatchDocument<EmployeeForUpdateDto> pachDoc)
        {
            if (pachDoc==null)
            {
                _logger.LogError("pachDoc object send from client is null");
                return BadRequest("pachDoc object is null");
            }

            var company = _repositoryManager.Company.GetCompany(companyId,trackChange: false);
            if (company == null)
            {
                _logger.LogInformation("Company with id: {companyId} doesn't exist in the database.",companyId);
                return NotFound();
            }

            var employeeEntite = _repositoryManager.Employee.GetEmployee(companyId, id, trackChanges: true);
            if (employeeEntite==null)
            {
                _logger.LogInformation("Employee with id: {id} doesn't exist in the database",id);
                return NotFound();
            }

            var employeeToPach = _mapper.Map<EmployeeForUpdateDto>(employeeEntite);

            pachDoc.ApplyTo(employeeToPach, ModelState);

            TryValidateModel(employeeToPach);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the pach Document.");
                return UnprocessableEntity(ModelState);
            }



            _mapper.Map(employeeToPach, employeeEntite);

            _repositoryManager.Save();

            return NoContent();
        }

    }
}
