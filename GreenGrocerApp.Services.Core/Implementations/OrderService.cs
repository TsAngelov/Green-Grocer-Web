using GreenGrocerApp.Data;
using GreenGrocerApp.Data.Models.Orders;
using GreenGrocerApp.Services.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Services.Core.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICartService _cartService;

        public OrderService(ApplicationDbContext dbContext, ICartService cartService)
        {
            _dbContext = dbContext;
            _cartService = cartService;
        }

        public async Task<Order> CreateOrderAsync(Guid userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);

            if (!cart.CartItems.Any())
                throw new InvalidOperationException("Cart is empty.");

            var order = new Order
            {
                ApplicationUserId = userId,
                OrderDate = DateTime.UtcNow,
                TotalPrice = cart.CartItems.Sum(ci => ci.Product.Price * ci.Quantity)
            };

            foreach (var item in cart.CartItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                });
            }

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            await _cartService.ClearCartAsync(userId);

            return order;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId)
        {
            return await _dbContext.Orders
                .Where(o => o.ApplicationUserId == userId && !o.IsDeleted)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            return await _dbContext.Orders
                .Where(o => o.Id == orderId && !o.IsDeleted)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
