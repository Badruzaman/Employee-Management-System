using EmployeeManagementSystem.DataAccess.Interfaces;
using EmployeeManagementSystem.Model.Domain;
using EmployeeManagementSystem.Model.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeManagementSystem.DataAccess.Implementations
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly EmpMgtSysContext _context;
        private readonly ILogger<DepartmentRepository> _logger;

        public DepartmentRepository(EmpMgtSysContext context, ILogger<DepartmentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            try
            {
                return await _context.Departments.Include(d => d.Manager).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all departments.");
                throw;
            }
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Departments.Include(d => d.Manager)
                    .FirstOrDefaultAsync(d => d.DepartmentID == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching department by ID: {Id}", id);
                throw;
            }
        }

        public async Task<Department> AddAsync(CreateDepartmentRequest request)
        {
            try
            {
                var department = new Department
                {
                    DepartmentName  = request.DepartmentName,
                    ManagerID = request.ManagerID,
                    CreatedDate = DateTime.Now
                };
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Department added successfully with ID: {DepartmentID}", department.DepartmentID);
                return department;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new department.");
                throw;
            }
        }

        public async Task<Department> UpdateAsync(Department department)
        {
            try
            {
                var existing = await _context.Departments.FindAsync(department.DepartmentID);
                if (existing == null)
                {
                    _logger.LogWarning("Department not found with ID: {DepartmentID}", department.DepartmentID);
                    return null;
                }

                existing.DepartmentName = department.DepartmentName;
                existing.ManagerID = department.ManagerID;
                existing.Budget = department.Budget;
                existing.UpdatedDate = DateTime.Now;

                await _context.SaveChangesAsync();
                _logger.LogInformation("Department updated successfully with ID: {DepartmentID}", department.DepartmentID);
                return existing;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating department with ID: {DepartmentID}", department.DepartmentID);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var department = await _context.Departments.FindAsync(id);
                if (department == null)
                {
                    _logger.LogWarning("Department not found with ID: {DepartmentID}", id);
                    return false;
                }

                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Department deleted successfully with ID: {DepartmentID}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting department with ID: {DepartmentID}", id);
                throw;
            }
        }
    }
}
