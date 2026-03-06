using MyApp.Interfaces;

namespace MyApp.Services
{
    public class HealthService : IHealthService
    {
        public string GetStatus() => "OK";
    }
} 