using System.Text.Json;
using Microsoft.Extensions.Logging;
using Store.Data.Context;
using Store.Data.Entities;

namespace Store.Repository.SeedData
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext context, ILogger<StoreContextSeed> logger)
        {
            // The Main Logic here to Cheek if database have data or not
            // if not run that code
            try
            {
                // Any return true while have Any data
                // So I Need To Make Sure I Don't have any data-first
                if (!context.ProductBrands.Any())
                {
                    // Get Data from JSON file
                    var productDate = await File.ReadAllTextAsync("brands.json");
                    // De-Serialization
                    var product = JsonSerializer.Deserialize<List<Product>>(productDate);

                    if (product is not null)
                    {
                        await context.Products.AddRangeAsync(product);
                        await context.SaveChangesAsync();
                    }
                }
            }

            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }
}