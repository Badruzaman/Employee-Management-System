using EmployeeManagementSystem.DataAccess.Interfaces;
using EmployeeManagementSystem.Model.Domain;
using EmployeeManagementSystem.Model.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.DataAccess.Implementations
{
    public class PerformanceReviewRepository : IPerformanceReviewRepository
    {
        private readonly EmpMgtSysContext _context;
        private readonly ILogger<PerformanceReviewRepository> _logger;
        public PerformanceReviewRepository(EmpMgtSysContext context, ILogger<PerformanceReviewRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<PerformanceReview>> GetAllAsync()
        {
            try
            {
                return await _context.PerformanceReviews.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all performance reviews.");
                throw;
            }
        }

        public async Task<PerformanceReview> GetByIdAsync(int id)
        {
            try
            {
                return await _context.PerformanceReviews.Include(r => r.Employee)
                    .FirstOrDefaultAsync(r => r.ReviewID == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching performance review by ID: {Id}", id);
                throw;
            }
        }

        public async Task<PerformanceReview> AddAsync(CreatePerformanceReviewRequest request)
        {
            try
            {
                var performanceReview = new PerformanceReview
                {
                    EmployeeID = request.EmployeeID,
                    ReviewDate = request.ReviewDate,
                    ReviewNotes = request.ReviewNotes,
                    ReviewScore = request.ReviewScore,
                    CreatedDate = DateTime.Now
                };
                _context.PerformanceReviews.Add(performanceReview);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Performance review added successfully with ID: {ReviewID}", performanceReview.ReviewID);
                return performanceReview;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new performance review.");
                throw;
            }
        }

        public async Task<PerformanceReview> UpdateAsync(PerformanceReview performanceReview)
        {
            try
            {
                var existing = await _context.PerformanceReviews.FindAsync(performanceReview.ReviewID);
                if (existing == null)
                {
                    _logger.LogWarning("Performance review not found with ID: {ReviewID}", performanceReview.ReviewID);
                    return null;
                }

                existing.ReviewDate = performanceReview.ReviewDate;
                existing.ReviewScore = performanceReview.ReviewScore;
                existing.ReviewNotes = performanceReview.ReviewNotes;
                existing.UpdatedDate = DateTime.Now;

                await _context.SaveChangesAsync();
                _logger.LogInformation("Performance review updated successfully with ID: {ReviewID}", performanceReview.ReviewID);
                return existing;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating performance review with ID: {ReviewID}", performanceReview.ReviewID);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var performanceReview = await _context.PerformanceReviews.FindAsync(id);
                if (performanceReview == null)
                {
                    _logger.LogWarning("Performance review not found with ID: {ReviewID}", id);
                    return false;
                }

                _context.PerformanceReviews.Remove(performanceReview);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Performance review deleted successfully with ID: {ReviewID}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting performance review with ID: {ReviewID}", id);
                throw;
            }
        }
    }
}