using basic_delivery_api.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace basic_delivery_api.Controllers;

public class HealthCheckController : BaseApiController
{
    private readonly AppDbContext _context;

    public HealthCheckController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("health")]
    public async Task<IActionResult> GetHealthStatus()
    {
        try
        {
            await _context.Database.CanConnectAsync();
            return Ok("Database connection is healthy.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Database connection failed: {ex.Message}");
        }
    }
}
