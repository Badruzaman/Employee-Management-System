using EmployeeManagementSystem.Model.Domain;
using EmployeeManagementSystem.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task<Department> GetDepartmentByIdAsync(int id);
        Task<Department> AddDepartmentAsync(CreateDepartmentRequest department);
        Task<Department> UpdateDepartmentAsync(Department department);
        Task<bool> DeleteDepartmentAsync(int id);
    }
}
