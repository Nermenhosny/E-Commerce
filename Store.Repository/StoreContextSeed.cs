using Microsoft.Extensions.Logging;
using Store.Data.Context;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext context ,ILoggerFactory loggerFactory )
        {
            try 
            {
                if (context.ProductBrands != null && !context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Store.Repository/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    if (brands is not null)
                    {
                        foreach (var brand in brands)
                            await context.ProductBrands.AddRangeAsync(brands);
                        await context.SaveChangesAsync();
                    }
                }

                if (context.ProductTypes != null && !context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText("../Store.Repository/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    if (types is not null)
                    {
                        //foreach (var brand in types)
                        await context.ProductTypes.AddRangeAsync(types);
                        await context.SaveChangesAsync();
                    }
                }
                if (context.Products != null && !context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Store.Repository/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    if (products is not null)
                    {
                        //foreach (var product in products)
                        await context.Products.AddRangeAsync(products);
                        await context.SaveChangesAsync();
                    }
                }

                if (context.DeliveryMethods != null && !context.DeliveryMethods.Any())
                {
                    var DeliveryMethodsData = File.ReadAllText("../Store.Repository/SeedData/delivery.json");
                    var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);
                    if (DeliveryMethods is not null)
                    {
                        //foreach (var product in products)
                        await context.DeliveryMethods.AddRangeAsync(DeliveryMethods);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch(Exception ex) 
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
           
        }

      
    }
}
