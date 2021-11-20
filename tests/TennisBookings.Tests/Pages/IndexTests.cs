namespace TennisBookings.Tests.Pages;

public class IndexTests
{
	[Fact]
	public async Task ReturnsExpectedViewModel_WhenWeatherIsSun()
	{
		var sut = new IndexModel(new SunnyForecaster(), NullLogger<IndexModel>.Instance);

		await sut.OnGet();

		Assert.Contains("It's sunny right now.", sut.WeatherDescription);
	}

	[Fact]
	public async Task ReturnsExpectedViewModel_WhenWeatherIsRain()
	{
		var sut = new IndexModel(new RainyForecaster(), NullLogger<IndexModel>.Instance);

		await sut.OnGet();

		Assert.Contains("We're sorry but it's raining here.", sut.WeatherDescription);
	}

	private class SunnyForecaster : IWeatherForecaster
	{
		public Task<WeatherResult?> GetCurrentWeatherAsync(string city)
			=> Task.FromResult<WeatherResult?>(new WeatherResult
			{
				Weather = new WeatherCondition
				{
					Summary = "Sun",
					Temperature = new Temperature(26, 28),
					Wind = new Wind(2, 25)
				},
				City = city
			});
	}

	private class RainyForecaster : IWeatherForecaster
	{
		public Task<WeatherResult?> GetCurrentWeatherAsync(string city)
			=> Task.FromResult<WeatherResult?>(new WeatherResult
			{
				Weather = new WeatherCondition
				{
					Summary = "Rain",
					Temperature = new Temperature(21, 23),
					Wind = new Wind(6, 130)
				},
				City = city
			});
	}
}
