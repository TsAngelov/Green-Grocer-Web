using GreenGrocerApp.Data.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Services.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid id);
        Task<IEnumerable<Category>> GetAllWithProductsAsync();

        Task<Guid> CreateAsync(CategoryInputModel model);
        Task UpdateAsync(Guid id, CategoryInputModel model);
        Task DeleteAsync(Guid id);
    }
}
