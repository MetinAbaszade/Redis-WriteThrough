using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Reddis_Task.Entities
{

    public class WeatherData
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string City { get; set; }
        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
        public double Pressure { get; set; }

        public string Source { get; set; }

        public WeatherData UpdateSource(string source)
        {
            Source = source;
            return this;
        }

        public static WeatherData GenerateRandomData(string city)
        {
            Random random = new Random();
            List<WeatherData> data = new List<WeatherData>();


            DateTime date = DateTime.Now.AddDays(random.Next(1, 5));
            double temperature = random.Next(-20, 50) + random.NextDouble();
            double humidity = random.NextDouble() * 100;
            double windSpeed = random.NextDouble() * 30;
            double pressure = random.NextDouble() * 1000 + 900;

            var NewWeatherdata = new WeatherData()
            {
                City = city,
                Date = date,
                Temperature = temperature,
                Humidity = humidity,
                WindSpeed = windSpeed,
                Pressure = pressure
            };

            return NewWeatherdata;
        }
    }
}
