using System;
using System.Threading.Tasks;
using GreenGrocerApp.Data;
using GreenGrocerApp.Data.Models.Products;
using GreenGrocerApp.Services.Core.Implementations;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace GreenGrocerApp.Services.Core.Tests
{
    public class CartServiceTests
    {
        [Test]
        public void AddItemAsync_ShouldThrow_WhenQuantityExceedsStock()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationDbContext(options);
            var cartService = new CartService(context);
            var userId = Guid.NewGuid();

            var category = new Category { Name = "Fruits" };
            var product = new Product
            {
                Name = "Banana",
                Price = 1.0m,
                QuantityInStock = 5,
                Category = category
            };

            context.Categories.Add(category);
            context.Products.Add(product);
            context.SaveChanges();

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await cartService.AddItemAsync(userId, product.Id, 6));
        }
    }
}