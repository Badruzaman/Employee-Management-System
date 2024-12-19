using EmployeeManagementSystem.Model.Domain;
using EmployeeManagementSystem.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.DataAccess.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int id);
        Task<Employee> AddAsync(CreateEmployeeRequest employee);
        Task<Employee> UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(int id);
    }
}
