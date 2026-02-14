using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Repository;

namespace Store.API.Helper;

public class ApplySeeding
{
    public static async Task ApplySeedingAsync(WebApplication app, string? connString)
    {
        using var scoop = app.Services.CreateScope();
        var services = scoop.ServiceProvider;

        var loggerFactory = services.GetRequiredService<ILoggerFactory>();

        try
        {
            var context = services.GetRequiredService<StoreDbContext>();
            await context.Database.MigrateAsync();
            await StoreContextSeed.SeedAsync(context, loggerFactory);
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<StoreContextSeed>();
            logger.LogError(ex.Message);
            throw;
        }
    }
}