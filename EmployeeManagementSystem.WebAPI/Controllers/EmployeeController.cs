using EmployeeManagementSystem.DataAccess.Interfaces;
using EmployeeManagementSystem.Model.Domain;
using EmployeeManagementSystem.Model.Request;
using EmployeeManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployeesAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all employees.");
                return StatusCode(500, "An error occurred while fetching employees.");
            }
        }

        // GET: api/Employee/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                if (employee == null) return NotFound();
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching employee by ID: {Id}", id);
                return StatusCode(500, "An error occurred while fetching the employee.");
            }
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] CreateEmployeeRequest employee)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid employee data.");

                var createdEmployee = await _employeeService.AddEmployeeAsync(employee);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.EmployeeID }, createdEmployee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new employee.");
                return StatusCode(500, "An error occurred while adding the employee.");
            }
        }

        // PUT: api/Employee/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            try
            {
                if (id != employee.EmployeeID)
                    return BadRequest("Employee ID mismatch.");

                if (!ModelState.IsValid)
                    return BadRequest("Invalid employee data.");

                var updatedEmployee = await _employeeService.UpdateEmployeeAsync(employee);
                if (updatedEmployee == null) return NotFound();
                return Ok(updatedEmployee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating employee with ID: {Id}", id);
                return StatusCode(500, "An error occurred while updating the employee.");
            }
        }

        // DELETE: api/Employee/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var result = await _employeeService.DeleteEmployeeAsync(id);
                if (!result) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting employee with ID: {Id}", id);
                return StatusCode(500, "An error occurred while deleting the employee.");
            }
        }
    }
}
