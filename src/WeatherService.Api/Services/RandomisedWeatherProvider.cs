using WeatherService.Api.Models;

namespace WeatherService.Api.Services
{
    public class RandomisedWeatherProvider : IWeatherProvider
    {
        private readonly Random _random = new();

        public WeatherResult GetLatestWeather(string city)
        {
            // create some random weather
            var condition = _random.Next(1, 4);
            
            var currentWeather = condition switch
            {
                1 => new WeatherResult
                {
                    City = city,
                    Weather = new WeatherCondition
                    {
                        Description = "Sun",
                        Temperature = new Temperature { Min = 26, Max = 32 },
                        Wind = new Wind { Degrees = 190, Speed = 6 }
                    }
                },
                2 => new WeatherResult
                {
                    City = city,
                    Weather = new WeatherCondition
                    {
                        Description = "Rain",
                        Temperature = new Temperature { Min = 8, Max = 14 },
                        Wind = new Wind { Degrees = 80, Speed = 3 }
                    }
                },
                3 => new WeatherResult
                {
                    City = city,
                    Weather = new WeatherCondition
                    {
                        Description = "Cloud",
                        Temperature = new Temperature { Min = 12, Max = 18 },
                        Wind = new Wind { Degrees = 10, Speed = 1 }
                    }
                },
                _ => new WeatherResult
                {
                    City = city,
                    Weather = new WeatherCondition
                    {
                        Description = "Snow",
                        Temperature = new Temperature { Min = -2, Max = 1 },
                        Wind = new Wind { Degrees = 240, Speed = 8 }
                    }
                },
            };

            return currentWeather;
        }
    }
}
