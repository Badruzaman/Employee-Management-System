using EmployeeManagementSystem.Model.Domain;
using EmployeeManagementSystem.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.DataAccess.Interfaces
{
    public interface IPerformanceReviewRepository
    {
        Task<IEnumerable<PerformanceReview>> GetAllAsync();
        Task<PerformanceReview> GetByIdAsync(int id);
        Task<PerformanceReview> AddAsync(CreatePerformanceReviewRequest performanceReview);
        Task<PerformanceReview> UpdateAsync(PerformanceReview performanceReview);
        Task<bool> DeleteAsync(int id);
    }
}
