using Microsoft.AspNetCore.Mvc;
using MyApp.Interfaces;

namespace MyApp.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class HealthController : ControllerBase
    {
        private readonly IHealthService _healthService;
        private readonly ILogger<HealthController> _logger;

        public HealthController(IHealthService healthService, ILogger<HealthController> logger)
        {
            _healthService = healthService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // log the check
            _logger.LogInformation("Health check requested");
            var status = _healthService.GetStatus();
            var version = System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? "unknown";
            return Ok(new { status, version });
        }
    }
}