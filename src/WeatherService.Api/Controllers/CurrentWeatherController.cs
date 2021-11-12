using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TennisBookings.Shared.Weather;

namespace WeatherService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentWeatherController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public CurrentWeatherController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet("{city}")]
        public async Task<ActionResult<WeatherResult>> Get(string city)
        {
            if (_memoryCache.TryGetValue(city, out var weather))
            {
                if (weather is WeatherResult result)
                {
                    return result;
                }
            }

            var weatherForecaster = new RandomWeatherForecaster();
            var currentWeather = await weatherForecaster.GetCurrentWeatherAsync(city);

            _memoryCache.Set(city, currentWeather, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(60 * 12)));

            return currentWeather;
        }
    }
}
