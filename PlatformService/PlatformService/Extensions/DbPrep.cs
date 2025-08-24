using PlatformService.Data;
using PlatformService.Models;

namespace PlatformService.Extensions
{
    public static class DbPrep
    {
        public static void PrepPlatforms(this IApplicationBuilder app)
        {
            using(var scope=app.ApplicationServices.CreateScope())
            {
                using var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
                if (!dbContext.Platforms.Any())
                {
                    Console.WriteLine("Preparing platforms...");
                    dbContext.Platforms.AddRange(
                        new Platform { Name = "dot net", Publisher = "Microsoft", Cost = 2000.00M },
                        new Platform { Name = "linux", Publisher = "Scribus", Cost = 4000.00M },
                        new Platform { Name = "debian", Publisher = "Scribus", Cost = 3000.00M }
                        );
                    dbContext.SaveChanges();
                }
                else
                {
                    Console.WriteLine("platforms already exists");
                }
            }
        }


    }
}
