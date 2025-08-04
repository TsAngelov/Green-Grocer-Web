using GreenGrocerApp.Data.Models.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Services.Core.Interfaces
{
    public interface ICartService
    {
        Task<Cart> GetCartByUserIdAsync(Guid userId);
        Task AddItemAsync(Guid userId, Guid productId, int quantity);
        Task RemoveItemAsync(Guid userId, Guid productId);
        Task ClearCartAsync(Guid userId);
    }
}
