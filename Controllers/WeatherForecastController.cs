using ClimaCast.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClimaCast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        public WeatherForecastController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeatherAsync(string city)
        {
            var data = await _weatherService.GetWeatherDataAsync(city);

            if (data?.Temperature != null)
            {
                return Ok(data);
            }
            else
            {
                return NotFound();
            }
        }
    }
}