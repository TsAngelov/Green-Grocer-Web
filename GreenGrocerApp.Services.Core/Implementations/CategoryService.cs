using AutoMapper;
using GreenGrocerApp.Data;
using GreenGrocerApp.Data.Models.Products;
using GreenGrocerApp.Services.Core.Interfaces;
using GreenGrocerApp.Web.ViewModels.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGrocerApp.Services.Core.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public CategoryService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Category>> GetAllAsync() =>
            await dbContext.Categories
                .Where(c => !c.IsDeleted)
                .AsNoTracking()
                .ToListAsync();

        public async Task<Category?> GetByIdAsync(Guid id) =>
            await dbContext.Categories
                .Where(c => !c.IsDeleted && c.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<Category>> GetAllWithProductsAsync() =>
            await dbContext.Categories
                .Where(c => !c.IsDeleted)
                .Include(c => c.Products.Where(p => !p.IsDeleted))
                .AsNoTracking()
                .ToListAsync();

        public async Task<Guid> CreateAsync(CategoryInputModel model)
        {
            var entity = mapper.Map<Category>(model);
            await dbContext.Categories.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task UpdateAsync(Guid id, CategoryInputModel model)
        {
            var entity = await dbContext.Categories
                .Where(c => !c.IsDeleted && c.Id == id)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new ArgumentException("Category not found.", nameof(id));
            }

            mapper.Map(model, entity);
            dbContext.Categories.Update(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await dbContext.Categories
                .Where(c => !c.IsDeleted && c.Id == id)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new ArgumentException("Category not found.", nameof(id));
            }

            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
            dbContext.Categories.Update(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
