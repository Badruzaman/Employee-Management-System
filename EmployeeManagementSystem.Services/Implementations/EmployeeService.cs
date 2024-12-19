using EmployeeManagementSystem.DataAccess.Interfaces;
using EmployeeManagementSystem.Model.Domain;
using EmployeeManagementSystem.Model.Request;
using EmployeeManagementSystem.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;
        public EmployeeService(IEmployeeRepository employeeRepository, ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            try
            {
                return await _employeeRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all employees in service layer.");
                throw;
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetByIdAsync(id);
                if (employee == null)
                {
                    _logger.LogWarning("No employee found with ID: {EmployeeID}", id);
                }
                return employee;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching employee by ID: {EmployeeID} in service layer.", id);
                throw;
            }
        }

        public async Task<Employee> AddEmployeeAsync(CreateEmployeeRequest employee)
        {
            try
            {
                return await _employeeRepository.AddAsync(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new employee in service layer.");
                throw;
            }
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                var updatedEmployee = await _employeeRepository.UpdateAsync(employee);
                if (updatedEmployee == null)
                {
                    _logger.LogWarning("Update failed. Employee not found or already deleted with ID: {EmployeeID}", employee.EmployeeID);
                }
                return updatedEmployee;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating employee with ID: {EmployeeID} in service layer.", employee.EmployeeID);
                throw;
            }
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            try
            {
                var isDeleted = await _employeeRepository.DeleteAsync(id);
                if (!isDeleted)
                {
                    _logger.LogWarning("Delete operation failed. Employee not found or already deleted with ID: {EmployeeID}", id);
                }
                return isDeleted;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting employee with ID: {EmployeeID} in service layer.", id);
                throw;
            }
        }
    }
}
