using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Reddis_Task.Data;
using Reddis_Task.Entities;
using Reddis_Task.Services.Abstract;

namespace Reddis_Task.Services.Concrete
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IRedisService _redisService;
        private readonly ApplicationContext _context;


        public WeatherService(IRedisService redisService, ApplicationContext context)
        {
            _httpClient = new HttpClient();
            _redisService = redisService;
            _context = context;
        }

        // Lazy Loading
        public async Task<WeatherData> GetWeatherData(string city)
        {
            var redisdata = await _redisService.GetDataAsync(city);
            WeatherData weatherData;

            if (string.IsNullOrEmpty(redisdata))
            {
                weatherData = WeatherData.GenerateRandomData(city);

                // Store the data in cache for 1 hour
                try
                {
                    // TTL
                    TimeSpan oneHour = TimeSpan.FromHours(1);
                    _redisService.AddDataAsync(city, JsonConvert.SerializeObject(weatherData), oneHour);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                weatherData.UpdateSource("API");
                return weatherData;
            }

            weatherData = JsonConvert.DeserializeObject<WeatherData>(redisdata);
            weatherData.UpdateSource("Redis");
            return weatherData;
        }

        // Write Through
        public async Task<WeatherData> UpdateWeatherData(WeatherData weatherData)
        {
            try
            {
                weatherData.UpdateSource("API");

                var weatherdatainDB = _context.WeatherDatas.FindAsync(weatherData.Id);

                if (weatherdatainDB.Result == null)
                {
                    throw new Exception("Data not found");
                } 

                // Write to DB
                _context.WeatherDatas.Update(weatherData);
                await _context.SaveChangesAsync();

                // Update Redis
                TimeSpan oneHour = TimeSpan.FromHours(1);
                _redisService.EditDataAsync(weatherData.City, JsonConvert.SerializeObject(weatherData), oneHour);

                return _context.WeatherDatas.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
