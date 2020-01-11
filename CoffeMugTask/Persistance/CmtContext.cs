using CoffeeMugTask.Model;
using CoffeeMugTask.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMugTask.Persistance
{
    public class CmtContext : DbContext
    {

        public CmtContext(DbContextOptions<CmtContext> options):base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();

            options.UseSqlServer(configuration["Data:DefaultConnection:ConnectionString"]);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Product>().Configure();
        }
    }
}
