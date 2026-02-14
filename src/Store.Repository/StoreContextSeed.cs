using System.Text.Json;
using Microsoft.Extensions.Logging;
using Store.Data.Context;
using Store.Data.Entities;

namespace Store.Repository;

public class StoreContextSeed
{
    public static async Task SeedAsync(
        StoreDbContext context,
        ILoggerFactory loggerFactory)
    {
        try
        {
            if (!context.ProductBrands.Any())
            {
                var basePath = Path.GetDirectoryName(typeof(StoreContextSeed).Assembly.Location);
                var brandData = await File.ReadAllTextAsync(Path.Combine(basePath!, "SeedData", "brands.json"));

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                if (brands is not null)
                {
                    await context.ProductBrands.AddRangeAsync(brands);
                    await context.SaveChangesAsync();
                }
            }

            if (!context.ProductTypes.Any())
            {
                var productType = await File.ReadAllTextAsync(@"../Store.Repository/SeedData/types.json");

                var types = JsonSerializer.Deserialize<List<ProductType>>(productType);

                if (types is not null)
                {
                    await context.ProductTypes.AddRangeAsync(types);
                    await context.SaveChangesAsync();
                }
            }

            if (!context.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync(@"../Store.Repository/SeedData/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products is not null)
                {
                    await context.Products.AddRangeAsync(products);
                    await context.SaveChangesAsync();
                }
            }
        }

        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<StoreContextSeed>();
            logger.LogError(ex.Message);
        }
    }
}