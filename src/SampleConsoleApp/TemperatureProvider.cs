using TennisBookings.Shared.Weather;

namespace SampleConsoleApp
{
	public class TemperatureProvider
	{
		private readonly IWeatherForecaster _weatherForecaster;

		public TemperatureProvider(IWeatherForecaster weatherForecaster)
		{
			_weatherForecaster = weatherForecaster;
		}

		public async Task<float> GetMaximumTemperatureAsync()
		{
			var weatherResult = await _weatherForecaster.GetCurrentWeatherAsync("Eastbourne");
			return weatherResult.Weather.Temperature.Max;
		}
	}
}
