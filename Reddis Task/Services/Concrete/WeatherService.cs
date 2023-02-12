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

        public async Task<WeatherData> GetWeatherData(string city)
        {

            string apiKey = "f8c667e0bd93b1e29e75c3e7520410d0";
            string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(responseString);

            // Store the data in cache for 1 hour
            try
            {
               await _context.WeatherDatas.AddAsync(weatherData);
               await _context.SaveChangesAsync();
               return _context.WeatherDatas.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
    }
}
