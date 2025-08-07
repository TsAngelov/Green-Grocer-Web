using GreenGrocerApp.Data;
using GreenGrocerApp.Data.Models.Carts;
using GreenGrocerApp.Services.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Services.Core.Implementations
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _dbContext;

        public CartService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Cart> GetCartByUserIdAsync(Guid userId)
        {
            var cart = await _dbContext.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.ApplicationUserId == userId);

            if (cart == null)
            {
                cart = new Cart { ApplicationUserId = userId };
                _dbContext.Carts.Add(cart);
                await _dbContext.SaveChangesAsync();
            }

            return cart;
        }

        public async Task AddItemAsync(Guid userId, Guid productId, int quantity)
        {
            var cart = await GetCartByUserIdAsync(userId);

            var product = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == productId && !p.IsDeleted);
            if (product == null)
            {
                throw new ArgumentException("Product not found.", nameof(productId));
            }

            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            var requestedQuantity = quantity + (existingItem?.Quantity ?? 0);
            if (requestedQuantity > product.QuantityInStock)
            {
                throw new InvalidOperationException("Insufficient stock available.");
            }

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity
                });
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveItemAsync(Guid userId, Guid productId)
        {
            var cart = await GetCartByUserIdAsync(userId);

            var item = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (item != null)
            {
                cart.CartItems.Remove(item);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(Guid userId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            _dbContext.CartItems.RemoveRange(cart.CartItems);
            await _dbContext.SaveChangesAsync();
        }
    }
}
