using ClimaCast.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;

namespace ClimaCast.Services
{
    public class WeatherService
    {
        private readonly IConfiguration _config;

        public WeatherService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<WeatherData?> GetWeatherDataAsync(string city)
        {
            // Make request to API ...
            string apiKey = _config.GetValue<string>("WeatherApiKey");
            string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={apiKey}";

            var client = new HttpClient();
            var response = await client.GetAsync(apiUrl);
            Debug.WriteLine(response.Content);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                // JObject.Parse() method is used to convert the JSON string into a JObject instance,
                // which allows us to easily extract data from the JSON object using the[] index operator.
                JObject data = JObject.Parse(content);
                WeatherData weatherData = new WeatherData
                {
                    Weather = data["weather"][0]["main"].Value<string>(),
                    WeatherDescription = data["weather"][0]["description"].Value<string>(),
                    Temperature = data["main"]["temp"].Value<decimal>(),
                    FeelsLike = data["main"]["feels_like"].Value<decimal>(),
                    Humidity = data["main"]["humidity"].Value<decimal>(),
                    MinTemperature = data["main"]["temp_min"].Value<decimal>(),
                    MaxTemperature = data["main"]["temp_max"].Value<decimal>(),

                };

                return weatherData;
            }
            else
            {
                return null;
            }
        }
    }
}
