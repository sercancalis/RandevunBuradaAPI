using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts
{
    public class BaseDbCtxDesignInit//: IDesignTimeDbContextFactory<BaseDbContext>
    { 
        public BaseDbContext CreateDbContext(string[] args)
        {

            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var optionBuilder = new DbContextOptionsBuilder<BaseDbContext>();
            var connString = config.GetConnectionString("RentACarConnectionString");
            optionBuilder.UseSqlServer(connString);
            return new BaseDbContext(config);
        }
    }
}

