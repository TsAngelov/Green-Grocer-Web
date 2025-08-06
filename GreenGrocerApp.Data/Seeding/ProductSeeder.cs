using GreenGrocerApp.Data.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Data.Seeding
{
    public class ProductSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            _ = serviceProvider;

            if (dbContext.Products.Any())
            {
                return;
            }

            var fruitsCategory = dbContext.Categories.FirstOrDefault(c => c.Name == "Fruits");
            var vegetablesCategory = dbContext.Categories.FirstOrDefault(c => c.Name == "Vegetables");

            var supplier = dbContext.Suppliers.FirstOrDefault();

            if (fruitsCategory != null && supplier != null)
            {
                await dbContext.Products.AddAsync(new Product
                {
                    Name = "Orange",
                    Price = 1.20m,
                    QuantityInStock = 100,
                    CategoryId = fruitsCategory.Id
                });
            }

            if (vegetablesCategory != null && supplier != null)
            {
                await dbContext.Products.AddAsync(new Product
                {
                    Name = "Cucumber",
                    Price = 0.80m,
                    QuantityInStock = 200,
                    CategoryId = vegetablesCategory.Id
                });
            }
        }
    }
}
