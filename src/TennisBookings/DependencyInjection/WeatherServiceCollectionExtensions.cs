using Microsoft.Extensions.DependencyInjection.Extensions;

namespace TennisBookings.DependencyInjection
{
	public static class WeatherServiceCollectionExtensions
	{
		public static IServiceCollection AddWeatherForecasting(this IServiceCollection services)
		{
			services.TryAddSingleton<IWeatherForecaster, RandomWeatherForecaster>();
			services.Decorate<IWeatherForecaster, CachedWeatherForecaster>(); // Decorator pattern

			return services;
		}
	}
}
