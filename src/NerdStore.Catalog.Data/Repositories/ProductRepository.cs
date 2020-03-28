using Microsoft.EntityFrameworkCore;
using NerdStore.Catalog.Domain;
using NerdStore.Catalog.Domain.Interface.Repostory;
using NerdStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly CatalogContext context;

        public ProductRepository(CatalogContext _catalogContext)
        {
            context = _catalogContext;
        }

        public IUnitOfWork UnitOfWork => context;


        public async Task<IEnumerable<Product>> GetAll()
        {
            return await context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategory(int code)
        {
            return await context.Products.AsNoTracking()
                .Include(p => p.Category)
                .Where(c => c.Category.Code == code).ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await context.Products.FindAsync(id);
        }

        public void Add(Product product)
        {
            context.Products.Add(product);
        }

        public void Add(Category category)
        {
            context.Categories.Add(category);
        }

        public void Update(Product product)
        {
            context.Products.Update(product);
        }

        public void Update(Category category)
        {
            context.Categories.Update(category);
        }

        public void Dispose()
        {
            context?.Dispose();
        }

    }
}
