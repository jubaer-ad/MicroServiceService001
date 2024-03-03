using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProduction)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProduction);
        }

        private async static void SeedData(AppDbContext? context, bool isProduction)
        {
            ArgumentNullException.ThrowIfNull(context);
            if (isProduction)
            {
                System.Console.WriteLine("---> Attempting to applying migration...");
                try
                {
                    await context.Database.MigrateAsync();
                    System.Console.WriteLine("---> Migration applied successfully...");
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"---> Could not run migration: {ex.Message}");
                }
            }
            if (!context.Platforms.Any())
            {
                System.Console.WriteLine("######### Seeding data #########");
                context.Platforms.AddRange(
                    new Platform()
                    {
                        Name = "Dot Net",
                        Publisher = "Microsoft",
                        Cost = "Free"
                    },
                    new Platform()
                    {
                        Name = "SQL Server",
                        Publisher = "Microsoft",
                        Cost = "Free"
                    },
                    new Platform()
                    {
                        Name = "Kubernetes",
                        Publisher = "Cloud Native Computing Foundation",
                        Cost = "Free"
                    }
                );

                context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("######### Already have data #########");
            }
        }
    }
}