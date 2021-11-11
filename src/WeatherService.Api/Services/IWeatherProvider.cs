using WeatherService.Api.Models;

namespace WeatherService.Api.Services
{
    public interface IWeatherProvider
    {
        WeatherResult GetLatestWeather(string city);
    }
}