using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Reddis_Task.Entities;
using Reddis_Task.Models;
using Reddis_Task.Services.Abstract;
using Reddis_Task.Services.Concrete;
using System.Text;

namespace Reddis_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly IRedisService _redisService;

        public WeatherForecastController(IWeatherService weatherService, IRedisService redisService)
        {
            _weatherService = weatherService;
            _redisService = redisService;
        }

        [HttpPost("GetWeatherData")]
        public async Task<ActionResult<WeatherData>> GetWeatherData(string City)
        {
            try
            {
                var WeatherData = await _weatherService.GetWeatherData(City);
                return WeatherData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("UpdateWeatherData")]
        public async Task<ActionResult<string>> UpdateWeatherData(WeatherDataInsertModel weatherdataModel)
        {
            WeatherData weatherData = new WeatherData()
            {
                City = weatherdataModel.City,
                Date = weatherdataModel.Date,
                Pressure = weatherdataModel.Pressure,
                Humidity = weatherdataModel.Humidity,
                Temperature = weatherdataModel.Temperature,
                WindSpeed = weatherdataModel.WindSpeed
            };

            try
            {
                var WeatherData = await _weatherService.UpdateWeatherData(weatherData);
                return "Successfully Updated";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}






