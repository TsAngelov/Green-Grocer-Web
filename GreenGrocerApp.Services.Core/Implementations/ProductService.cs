using AutoMapper;
using GreenGrocerApp.Data;
using GreenGrocerApp.Data.Models.Products;
using GreenGrocerApp.Services.Core.Interfaces;
using GreenGrocerApp.Web.ViewModels.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Services.Core.Implementations
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public ProductService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() =>
            await dbContext.Products
                .Where(p => !p.IsDeleted)
                .Include(p => p.Category)
                .AsNoTracking()
                .ToListAsync();

        public async Task<Product?> GetByIdAsync(Guid id) =>
            await dbContext.Products
                .Where(p => !p.IsDeleted && p.Id == id)
                .Include(p => p.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId) =>
            await dbContext.Products
                .Where(p => !p.IsDeleted && p.CategoryId == categoryId)
                .Include(p => p.Category)
                .AsNoTracking()
                .ToListAsync();

        public async Task<Guid> CreateAsync(ProductInputModel model)
        {
            var entity = mapper.Map<Product>(model);
            await dbContext.Products.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task UpdateAsync(Guid id, ProductInputModel model)
        {
            var entity = await dbContext.Products
                .Where(p => !p.IsDeleted && p.Id == id)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new ArgumentException("Product not found.", nameof(id));
            }

            mapper.Map(model, entity);
            dbContext.Products.Update(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await dbContext.Products
                .Where(p => !p.IsDeleted && p.Id == id)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new ArgumentException("Product not found.", nameof(id));
            }

            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
            dbContext.Products.Update(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
