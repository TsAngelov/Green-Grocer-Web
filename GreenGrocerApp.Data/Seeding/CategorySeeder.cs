using GreenGrocerApp.Data.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Data.Seeding
{
    public class CategorySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddRangeAsync(
                new Category { Name = "Fruits", Description = "Fresh fruits" },
                new Category { Name = "Vegetables", Description = "Fresh vegetables" },
                new Category { Name = "Dairy", Description = "Milk and dairy products" }
            );
        }
    }
}
