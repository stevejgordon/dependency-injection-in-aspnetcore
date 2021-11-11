using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WeatherService.Api.Models;
using WeatherService.Api.Services;

namespace WeatherService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentWeatherController : ControllerBase
    {
        private readonly IWeatherProvider _weatherProvider;
        private readonly IMemoryCache _memoryCache;

        private readonly Random _random = new();

        public CurrentWeatherController(IWeatherProvider weatherProvider, IMemoryCache memoryCache)
        {
            _weatherProvider = weatherProvider;
            _memoryCache = memoryCache;
        }

        [HttpGet("{city}")]
        public async Task<ActionResult<WeatherResult>> Get(string city)
        {
            // simulate a slow API response
            await Task.Delay(_random.Next(50, 300));

            if (_memoryCache.TryGetValue(city, out var weather))
            {
                if (weather is WeatherResult result)
                {
                    return result;
                }
            }

            var currentWeather = _weatherProvider.GetLatestWeather(city);

            _memoryCache.Set(city, currentWeather, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(60 * 12)));

            return currentWeather;
        }
    }
}
