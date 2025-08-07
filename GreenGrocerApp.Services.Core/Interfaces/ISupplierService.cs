using GreenGrocerApp.Data.Models.Products;
using GreenGrocerApp.Web.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreenGrocerApp.Services.Core.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetAllAsync();
        Task<Supplier?> GetByIdAsync(Guid id);

        Task<Guid> CreateAsync(SupplierInputModel model);
        Task UpdateAsync(Guid id, SupplierInputModel model);
        Task DeleteAsync(Guid id);
    }
}
