using EmployeeManagementSystem.Model.Domain;
using EmployeeManagementSystem.Model.Request;
using EmployeeManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceReviewController : ControllerBase
    {
        private readonly IPerformanceReviewService _performanceReviewService;
        private readonly ILogger<PerformanceReviewController> _logger;

        public PerformanceReviewController(IPerformanceReviewService performanceReviewService, ILogger<PerformanceReviewController> logger)
        {
            _performanceReviewService = performanceReviewService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPerformanceReviews()
        {
            try
            {
                var reviews = await _performanceReviewService.GetAllReviewsAsync();
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all performance reviews.");
                return StatusCode(500, "An error occurred while fetching performance reviews.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerformanceReviewById(int id)
        {
            try
            {
                var review = await _performanceReviewService.GetReviewByIdAsync(id);
                if (review == null) return NotFound();
                return Ok(review);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching performance review by ID: {Id}", id);
                return StatusCode(500, "An error occurred while fetching the performance review.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPerformanceReview([FromBody] CreatePerformanceReviewRequest performanceReview)
        {
            try
            {
                var createdReview = await _performanceReviewService.AddReviewAsync(performanceReview);
                return CreatedAtAction(nameof(GetPerformanceReviewById), new { id = createdReview.ReviewID }, createdReview);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new performance review.");
                return StatusCode(500, "An error occurred while adding the performance review.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerformanceReview(int id, [FromBody] PerformanceReview performanceReview)
        {
            try
            {
                if (id != performanceReview.ReviewID) return BadRequest("Performance review ID mismatch.");
                var updatedReview = await _performanceReviewService.UpdateReviewAsync(performanceReview);
                if (updatedReview == null) return NotFound();
                return Ok(updatedReview);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating performance review with ID: {Id}", id);
                return StatusCode(500, "An error occurred while updating the performance review.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerformanceReview(int id)
        {
            try
            {
                var result = await _performanceReviewService.DeleteReviewAsync(id);
                if (!result) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting performance review with ID: {Id}", id);
                return StatusCode(500, "An error occurred while deleting the performance review.");
            }
        }
    }
}