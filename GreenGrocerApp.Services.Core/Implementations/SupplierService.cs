using AutoMapper;
using GreenGrocerApp.Data;
using GreenGrocerApp.Data.Models.Products;
using GreenGrocerApp.Services.Core.Interfaces;
using GreenGrocerApp.Web.ViewModels.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenGrocerApp.Services.Core.Implementations
{
    public class SupplierService : ISupplierService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public SupplierService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync() =>
            await dbContext.Suppliers
                .Where(s => !s.IsDeleted)
                .AsNoTracking()
                .ToListAsync();

        public async Task<Supplier?> GetByIdAsync(Guid id) =>
            await dbContext.Suppliers
                .Where(s => !s.IsDeleted && s.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

        public async Task<Guid> CreateAsync(SupplierInputModel model)
        {
            var entity = mapper.Map<Supplier>(model);
            await dbContext.Suppliers.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task UpdateAsync(Guid id, SupplierInputModel model)
        {
            var entity = await dbContext.Suppliers
                .Where(s => !s.IsDeleted && s.Id == id)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new ArgumentException("Supplier not found.", nameof(id));
            }

            mapper.Map(model, entity);
            dbContext.Suppliers.Update(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await dbContext.Suppliers
                .Where(s => !s.IsDeleted && s.Id == id)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new ArgumentException("Supplier not found.", nameof(id));
            }

            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
            dbContext.Suppliers.Update(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}