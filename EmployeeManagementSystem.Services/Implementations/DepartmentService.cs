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
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ILogger<DepartmentService> _logger;
        public DepartmentService(IDepartmentRepository departmentRepository, ILogger<DepartmentService> logger)
        {
            _departmentRepository = departmentRepository;
            _logger = logger;
        }
        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            try
            {
                return await _departmentRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all departments in service layer.");
                throw;
            }
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            try
            {
                return await _departmentRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching department by ID in service layer: {Id}", id);
                throw;
            }
        }

        public async Task<Department> AddDepartmentAsync(CreateDepartmentRequest department)
        {
            try
            {
                return await _departmentRepository.AddAsync(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new department in service layer.");
                throw;
            }
        }

        public async Task<Department> UpdateDepartmentAsync(Department department)
        {
            try
            {
                return await _departmentRepository.UpdateAsync(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating department in service layer.");
                throw;
            }
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            try
            {
                return await _departmentRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting department in service layer with ID: {Id}", id);
                throw;
            }
        }
    }
}
