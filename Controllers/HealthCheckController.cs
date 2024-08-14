using basic_delivery_api.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace basic_delivery_api.Controllers
{
    public class HealthCheckController : BaseApiController
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HealthCheckController> _logger;

        public HealthCheckController(AppDbContext context, ILogger<HealthCheckController> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        /// <summary>
        /// Check the health status of the API.
        /// </summary>
        /// <returns>Health status of the API.</returns>
        [HttpGet("health")]
        public async Task<IActionResult> GetHealthStatus()
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync();
                if (canConnect)
                {
                    return Ok("Database connection is healthy.");
                }
                else
                {
                    _logger.LogWarning("Database connection isn't available.");
                    return StatusCode(503, "Database connection isn't available.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database connection failed.");
                return StatusCode(500, "An error occurred while checking the database connection.");
            }
        }
    }
}