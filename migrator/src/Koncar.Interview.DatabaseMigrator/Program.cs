namespace Koncar.Interview.DatabaseMigrator;

using Koncar.Interview.Server.Infrastructure.Db.Postgres;
using Microsoft.EntityFrameworkCore;

public class Program
{
    static async Task Main(string[] args)
    {
        DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseNpgsql(Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection"))
            .Options;

        using DatabaseContext dbContext = new(options);
        const int maxRetryCount = 10;
        const int retryDelaySeconds = 5;

        for (int i = 0; i < maxRetryCount; ++i)
        {
            try
            {
                Console.WriteLine("Attempting to run migrations...");
                await dbContext.Database.MigrateAsync();
                break;
            } 
            catch (Exception) when ( i < maxRetryCount)
            {
                Console.WriteLine($"Failed to run migrations, retrying in {retryDelaySeconds}s.");
                await Task.Delay(TimeSpan.FromSeconds(retryDelaySeconds));
            }
        }

        Console.WriteLine("Migrations applied successfully.");
    }
}
