using Microsoft.EntityFrameworkCore;
using Reddis_Task.Data;
using Reddis_Task.Extensions;
using Reddis_Task.Services.Abstract;
using Reddis_Task.Services.Concrete;
using System;

namespace Reddis_Task
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationContext>(o => o.UseInMemoryDatabase("WeatherInMemoryDatabase"));
            builder.Services.AddTransient<IWeatherService, WeatherService>();
            builder.Services.AddRedisService("localhost");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}