using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.DAL.Entities;
using Talabat.DAL.Entities.Order;

namespace Talabat.DAL.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if(!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Talabat.DAL/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    foreach (var brand in brands)
                        context.ProductBrands.Add(brand);

                    await context.SaveChangesAsync();

                }
                if (!context.ProductTypes.Any())
                {
                    var typesData =
                        File.ReadAllText("../Talabat.DAL/Data/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    foreach (var item in types)
                        context.ProductTypes.Add(item);

                    await context.SaveChangesAsync();
                }
                if (!context.Products.Any())
                {
                    var productsData =
                        File.ReadAllText("../Talabat.DAL/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach (var item in products)
                        context.Products.Add(item);

                    await context.SaveChangesAsync();
                }
                if (!context.DeliveryMethods.Any())
                {
                    var deliverMethodsData =
                        File.ReadAllText("../Talabat.DAL/Data/SeedData/delivery.json");
                    var deliverMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliverMethodsData);
                    foreach (var deliverMethod in deliverMethods)
                        context.DeliveryMethods.Add(deliverMethod);

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
