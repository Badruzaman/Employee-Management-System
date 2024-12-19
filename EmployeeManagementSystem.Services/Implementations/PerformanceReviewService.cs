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
    public class PerformanceReviewService : IPerformanceReviewService
    {
        private readonly IPerformanceReviewRepository _performanceReviewRepository;
        private readonly ILogger<PerformanceReviewService> _logger;

        public PerformanceReviewService(IPerformanceReviewRepository performanceReviewRepository, ILogger<PerformanceReviewService> logger)
        {
            _performanceReviewRepository = performanceReviewRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<PerformanceReview>> GetAllReviewsAsync()
        {
            try
            {
                return await _performanceReviewRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all performance reviews in service layer.");
                throw;
            }
        }

        public async Task<PerformanceReview> GetReviewByIdAsync(int id)
        {
            try
            {
                return await _performanceReviewRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching performance review by ID in service layer: {Id}", id);
                throw;
            }
        }

        public async Task<PerformanceReview> AddReviewAsync(CreatePerformanceReviewRequest performanceReview)
        {
            try
            {
                return await _performanceReviewRepository.AddAsync(performanceReview);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new performance review in service layer.");
                throw;
            }
        }

        public async Task<PerformanceReview> UpdateReviewAsync(PerformanceReview performanceReview)
        {
            try
            {
                return await _performanceReviewRepository.UpdateAsync(performanceReview);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating performance review in service layer.");
                throw;
            }
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            try
            {
                return await _performanceReviewRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting performance review in service layer with ID: {Id}", id);
                throw;
            }
        }
    }
}
