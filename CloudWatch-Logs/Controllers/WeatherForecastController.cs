using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

namespace CloudWatch_Logs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static readonly Random random = new Random();
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            try
            {
                _logger.LogInformation("Method started");

                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = random.Next(-20, 55),
                    Summary = Summaries[random.Next(Summaries.Length)]
                }).ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the weather forecast");
                return Enumerable.Empty<WeatherForecast>();
            }
            finally
            {
                _logger.LogInformation($"Weather forecasts are shown.");
                _logger.LogInformation("Weather forecast method ended");
            }
        }
    }
}
