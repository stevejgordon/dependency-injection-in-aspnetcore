using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using TennisBookings.Shared.Weather;

namespace TennisBookings.Tests.Pages
{
    public class IndexTests
    {
        [Fact]
        public async Task ReturnsExpectedViewModel_WhenWeatherIsSun()
        {
			var mockWeatherForecaster = new Mock<IWeatherForecaster>();
			mockWeatherForecaster.Setup(w => w.GetCurrentWeatherAsync(It.IsAny<string>())).ReturnsAsync(new WeatherResult
			{
				Weather = new WeatherCondition
				{
					Summary = "Sun",
					Temperature = new Temperature(26, 28),
					Wind = new Wind(2, 25)
				},
				City = "city"
			});

			var sut = new IndexModel(mockWeatherForecaster.Object, NullLogger<IndexModel>.Instance);

			await sut.OnGet();

			Assert.Contains("It's sunny right now.", sut.WeatherDescription);
        }

        [Fact]
        public async Task ReturnsExpectedViewModel_WhenWeatherIsRain()
        {
			var mockWeatherForecaster = new Mock<IWeatherForecaster>();
			mockWeatherForecaster.Setup(w => w.GetCurrentWeatherAsync(It.IsAny<string>())).ReturnsAsync(new WeatherResult
			{
				Weather = new WeatherCondition
				{
					Summary = "Rain",
					Temperature = new Temperature(21, 23),
					Wind = new Wind(6, 130)
				},
				City = "city"
			});

			var sut = new IndexModel(mockWeatherForecaster.Object, NullLogger<IndexModel>.Instance);

			await sut.OnGet();

			Assert.Contains("We're sorry but it's raining here.", sut.WeatherDescription);
		}
    }
}
