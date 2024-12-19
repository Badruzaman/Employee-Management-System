using EmployeeManagementSystem.Model.Domain;
using EmployeeManagementSystem.Model.Request;
using EmployeeManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger)
        {
            _departmentService = departmentService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            try
            {
                var departments = await _departmentService.GetAllDepartmentsAsync();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all departments.");
                return StatusCode(500, "An error occurred while fetching departments.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var department = await _departmentService.GetDepartmentByIdAsync(id);
                if (department == null) return NotFound();
                return Ok(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching department by ID: {Id}", id);
                return StatusCode(500, "An error occurred while fetching the department.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] CreateDepartmentRequest department)
        {
            try
            {
                var createdDepartment = await _departmentService.AddDepartmentAsync(department);
                return CreatedAtAction(nameof(GetDepartmentById), new { id = createdDepartment.DepartmentID }, createdDepartment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new department.");
                return StatusCode(500, "An error occurred while adding the department.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] Department department)
        {
            try
            {
                if (id != department.DepartmentID) return BadRequest("Department ID mismatch.");
                var updatedDepartment = await _departmentService.UpdateDepartmentAsync(department);
                if (updatedDepartment == null) return NotFound();
                return Ok(updatedDepartment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating department with ID: {Id}", id);
                return StatusCode(500, "An error occurred while updating the department.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                var result = await _departmentService.DeleteDepartmentAsync(id);
                if (!result) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting department with ID: {Id}", id);
                return StatusCode(500, "An error occurred while deleting the department.");
            }
        }
    }
}
