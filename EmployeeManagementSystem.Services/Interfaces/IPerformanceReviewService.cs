using EmployeeManagementSystem.Model.Domain;
using EmployeeManagementSystem.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Services.Interfaces
{
    public interface IPerformanceReviewService
    {
        Task<IEnumerable<PerformanceReview>> GetAllReviewsAsync();
        Task<PerformanceReview> GetReviewByIdAsync(int id);
        Task<PerformanceReview> AddReviewAsync(CreatePerformanceReviewRequest performanceReview);
        Task<PerformanceReview> UpdateReviewAsync(PerformanceReview performanceReview);
        Task<bool> DeleteReviewAsync(int id);
    }
}
