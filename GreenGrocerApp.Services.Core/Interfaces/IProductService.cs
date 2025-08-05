using GreenGrocerApp.Data.Models.Products;
using GreenGrocerApp.Web.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Services.Core.Interfaces
{
    public interface IProductService
    {
        public interface IProductService
        {
            Task<IEnumerable<Product>> GetAllAsync();
            Task<Product?> GetByIdAsync(Guid id);
            Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId);

            Task<Guid> CreateAsync(ProductInputModel model);
            Task UpdateAsync(Guid id, ProductInputModel model);
            Task DeleteAsync(Guid id);
        }
    }
}
