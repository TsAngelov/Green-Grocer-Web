using System;
using System.Threading.Tasks;
using GreenGrocerApp.Data;
using GreenGrocerApp.Data.Models.Carts;
using GreenGrocerApp.Data.Models.Products;
using GreenGrocerApp.Services.Core.Implementations;
using Microsoft.EntityFrameworkCore;

namespace GreenGrocerApp.Services.Core.Tests
{
    public class OrderServiceTests
    {
        [Test]
        public async Task CreateOrderAsync_ShouldReduceProductStock()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationDbContext(options);
            var cartService = new CartService(context);
            var orderService = new OrderService(context, cartService);
            var userId = Guid.NewGuid();

            var category = new Category { Name = "Fruits" };
            var product = new Product
            {
                Name = "Apple",
                Price = 1.5m,
                QuantityInStock = 10,
                Category = category
            };

            context.Categories.Add(category);
            context.Products.Add(product);

            var cart = new Cart { ApplicationUserId = userId };
            cart.CartItems.Add(new CartItem
            {
                Product = product,
                ProductId = product.Id,
                Quantity = 3
            });

            context.Carts.Add(cart);
            await context.SaveChangesAsync();

            await orderService.CreateOrderAsync(userId);

            var updatedProduct = await context.Products.FirstAsync();
            Assert.That(updatedProduct.QuantityInStock, Is.EqualTo(7));
        }
    }
}
