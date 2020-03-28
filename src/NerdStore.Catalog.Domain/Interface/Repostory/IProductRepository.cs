using NerdStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Domain.Interface.Repostory
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(Guid id);
        Task<IEnumerable<Product>> GetByCategory(int code);
        Task<IEnumerable<Category>> GetAllCategories();

        void Add(Product product);
        void Update(Product product);

        void Add(Category category);
        void Update(Category category);


    }
}
