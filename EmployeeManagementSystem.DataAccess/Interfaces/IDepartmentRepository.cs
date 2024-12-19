using EmployeeManagementSystem.Model.Domain;
using EmployeeManagementSystem.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.DataAccess.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department> GetByIdAsync(int id);
        Task<Department> AddAsync(CreateDepartmentRequest department);
        Task<Department> UpdateAsync(Department department);
        Task<bool> DeleteAsync(int id);
    }
}
