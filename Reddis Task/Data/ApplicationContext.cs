using static Reddis_Task.Data.ApplicationContext;
using System.Collections.Generic;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using Reddis_Task.Entities;
using Microsoft.Extensions.Options;

namespace Reddis_Task.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<WeatherData> WeatherDatas { get; set; }
    }
}
