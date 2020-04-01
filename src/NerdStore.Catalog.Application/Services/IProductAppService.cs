using NerdStore.Catalog.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Application.Services
{
    public interface IProductAppService : IDisposable
    {
        Task<IEnumerable<ProductDto>> GetByCategory(int code);
        Task<ProductDto> GetById(Guid id);
        Task<IEnumerable<ProductDto>> GetAllProducts();
        Task<IEnumerable<CategoryDto>> GetAllCategories();

        Task AddProduct(ProductDto productDto);
        Task UpdateProduct(ProductDto productDto);

        Task<ProductDto> DebitStock(Guid id, int quantity);
        Task<ProductDto> StockReplacement(Guid id, int quantity);
    }

}
