using SampleConsoleApp;
using TennisBookings.Shared.Weather;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

serviceCollection.AddSingleton<IWeatherForecaster, RandomWeatherForecaster>();
serviceCollection.AddSingleton<TemperatureProvider>();

await using var serviceProvider = serviceCollection.BuildServiceProvider();

var temperatureProvider = serviceProvider.GetRequiredService<TemperatureProvider>();
var maxTemp = await temperatureProvider.GetMaximumTemperatureAsync();

Console.WriteLine($"Maximum temperature is {maxTemp:n2}°C");
