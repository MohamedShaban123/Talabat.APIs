using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync( StoreContext _dbContext )
        {
            //this condition for make seeding 1 time only
            if (  _dbContext.ProductBrands.Count() == 0)
            {
                // will read file as a string
                var brandsData = File.ReadAllText("../Talabat.Repository/DataSeed/brands.json");
                //this method will convert json file into another type
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands?.Count() > 0)
                {

                    foreach (var brand in brands)
                    {
                        _dbContext.Set<ProductBrand>().Add(brand);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }

            //this condition for make seeding 1 time only
            if (_dbContext.ProductCategories.Count() == 0)
            {
                // will read file as a string
                var categoriesData = File.ReadAllText("../Talabat.Repository/DataSeed/categories.json");
                //this method will convert json file into another type
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);
                if (categories?.Count() > 0)
                {

                    foreach (var category in categories)
                    {
                        _dbContext.Set<ProductCategory>().Add(category);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }

            //this condition for make seeding 1 time only
            if (_dbContext.Products.Count() == 0)
            {
                // will read file as a string
                var productsData = File.ReadAllText("../Talabat.Repository/DataSeed/products.json");
                //this method will convert json file into another type
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products?.Count() > 0)
                {

                    foreach (var product in products)
                    {
                        _dbContext.Set<Product>().Add(product);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }


            //this condition for make seeding 1 time only
            if (_dbContext.DeliveryMethods.Count() == 0)
            {
                // will read file as a string
                var deliveryData = File.ReadAllText("../Talabat.Repository/DataSeed/delivery.json");
                //this method will convert json file into another type
                var deliveries = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                if (deliveries?.Count() > 0)
                {

                    foreach (var delivery in deliveries)
                    {
                        _dbContext.Set<DeliveryMethod>().Add(delivery);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }


        }
    }
}
