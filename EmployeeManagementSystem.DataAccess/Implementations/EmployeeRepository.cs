using EmployeeManagementSystem.DataAccess.Interfaces;
using EmployeeManagementSystem.Model.Domain;
using EmployeeManagementSystem.Model.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeManagementSystem.DataAccess.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmpMgtSysContext _context;
        private readonly ILogger<EmployeeRepository> _logger;
        public EmployeeRepository(EmpMgtSysContext context, ILogger<EmployeeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            try
            {
                return await _context.Employees.Where(e => !e.Deleted).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all employees.");
                throw; // Re-throw exception to preserve stack trace
            }
        }
        public async Task<Employee> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeID == id && !e.Deleted);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching employee by ID: {Id}", id);
                throw;
            }
        }

        public async Task<Employee> AddAsync(CreateEmployeeRequest request)
        {
            try
            {
                using var dbTransaction = _context.Database.BeginTransaction();
                try
                {
                    var employee1 = new Employee
                    {
                        Name = request.Name,
                        Email = request.Email,
                        Phone = request.Phone,
                        Position = request.Position,
                        DepartmentID = request.DepartmentID,
                        JoiningDate = request.JoiningDate,
                        CreatedDate = DateTime.Now
                    };
                    _context.Employees.Add(employee1);
                    await _context.SaveChangesAsync();
                    await dbTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                   await dbTransaction.RollbackAsync();
                }
                var employee = new Employee
                {
                    Name = request.Name,
                    Email = request.Email,
                    Phone = request.Phone,
                    Position = request.Position,
                    DepartmentID = request.DepartmentID,
                    JoiningDate = request.JoiningDate,
                    CreatedDate = DateTime.Now
                };
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Employee added successfully with ID: {EmployeeID}", employee.EmployeeID);
                return employee;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new employee.");
                throw;
            }
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            try
            {
                var existing = await _context.Employees.FindAsync(employee.EmployeeID);

                if (existing == null || existing.Deleted)
                {
                    _logger.LogWarning("Employee not found or already deleted with ID: {EmployeeID}", employee.EmployeeID);
                    return null;
                }

                existing.Name = employee.Name;
                existing.Email = employee.Email;
                existing.Phone = employee.Phone;
                existing.DepartmentID = employee.DepartmentID;
                existing.Position = employee.Position;
                existing.JoiningDate = employee.JoiningDate;
                existing.IsActive = employee.IsActive;
                existing.UpdatedDate = DateTime.Now;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Employee updated successfully with ID: {EmployeeID}", employee.EmployeeID);
                return existing;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating employee with ID: {EmployeeID}", employee.EmployeeID);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);

                if (employee == null || employee.Deleted)
                {
                    _logger.LogWarning("Employee not found or already deleted with ID: {EmployeeID}", id);
                    return false;
                }

                employee.Deleted = true;
                employee.UpdatedDate = DateTime.Now;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Employee soft-deleted successfully with ID: {EmployeeID}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while soft-deleting employee with ID: {EmployeeID}", id);
                throw;
            }
        }
    }
}
