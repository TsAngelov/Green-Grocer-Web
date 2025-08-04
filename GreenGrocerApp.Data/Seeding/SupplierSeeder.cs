using GreenGrocerApp.Data.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Data.Seeding
{
    public class SupplierSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.Suppliers.Any())
            {
                return;
            }

            await dbContext.Suppliers.AddRangeAsync(
                new Supplier { Name = "FreshGreens Ltd", ContactInfo = "freshgreens@example.com" },
                new Supplier { Name = "HealthyLife Co", ContactInfo = "healthylife@example.com" }
            );
        }
    }
}
