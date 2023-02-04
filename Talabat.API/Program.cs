using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.DAL.Data;
using Talabat.DAL.Entities.Identity;
using Talabat.DAL.Identity;

namespace Talabat.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = serviceProvider.GetRequiredService<StoreContext>();
                await context.Database.MigrateAsync();

                await StoreContextSeed.SeedAsync(context, loggerFactory);

                var identityContext = serviceProvider.GetRequiredService<AppIdentityDbContext>();
                await identityContext.Database.MigrateAsync();

                var userManger = serviceProvider.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUsersAsync(userManger);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred during migration");
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
