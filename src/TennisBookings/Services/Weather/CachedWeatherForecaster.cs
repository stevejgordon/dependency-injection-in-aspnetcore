namespace TennisBookings.Shared.Weather;

public class CachedWeatherForecaster : IWeatherForecaster
{
	private readonly IWeatherForecaster _weatherForecaster;
	private readonly IDistributedCache<WeatherResult> _cache;

	public CachedWeatherForecaster(IWeatherForecaster weatherForecaster,
		IDistributedCache<WeatherResult> cache)
	{
		_weatherForecaster = weatherForecaster;
		_cache = cache;
	}

	public async Task<WeatherResult> GetCurrentWeatherAsync(string city)
	{
		var cacheKey = $"{city}_current_weather_{DateTime.UtcNow:yyyy_MM_dd}";

		var (isCached, forecast) = await _cache.TryGetValueAsync(cacheKey);

		if (isCached)
			return forecast!;

		var result = await _weatherForecaster.GetCurrentWeatherAsync(city);

		await _cache.SetAsync(cacheKey, result, 60);

		return result;
	}
}
