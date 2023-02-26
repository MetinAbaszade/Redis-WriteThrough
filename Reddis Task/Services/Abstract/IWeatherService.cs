using Reddis_Task.Entities;

namespace Reddis_Task.Services.Abstract
{
    public interface IWeatherService
    {
        public Task<WeatherData> GetWeatherData(string city);
        public Task<WeatherData> UpdateWeatherData(WeatherData weatherdata);
    }
}
