using AutoMapper;
using NerdStore.Catalog.Application.Dtos;
using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.Interface.Repostory;
using NerdStore.Catalog.Domain.Interface.Service;
using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Application.Services
{
    public class ProductAppService : IProductAppService
    {

        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;

        public ProductAppService(IProductRepository productRepository,
                         IMapper mapper,
                         IStockService stockService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _stockService = stockService;
        }

        public async Task AddProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _productRepository.Add(product);

            await _productRepository.UnitOfWork.Commit();
        }

        public async Task<ProductDto> DebitStock(Guid id, int quantity)
        {
            if (!_stockService.Debit(id, quantity).Result)
            {
                throw new DomainException("An error occurred while debit stock");
            }

            return _mapper.Map<ProductDto>(await _productRepository.GetById(id));
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            return _mapper.Map<IEnumerable<CategoryDto>>(await _productRepository.GetAllCategories());
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            return _mapper.Map<IEnumerable<ProductDto>>(await _productRepository.GetAll());
        }

        public async Task<IEnumerable<ProductDto>> GetByCategory(int code)
        {
            return _mapper.Map<IEnumerable<ProductDto>>(await _productRepository.GetByCategory(code));
        }

        public async Task<ProductDto> GetById(Guid id)
        {
            return _mapper.Map<ProductDto>(await _productRepository.GetById(id));
        }

        public async Task<ProductDto> StockReplacement(Guid id, int quantity)
        {
            if (!_stockService.Replenishment(id, quantity).Result)
            {
                throw new DomainException("An error occurred while restoring stock");
            }

            return _mapper.Map<ProductDto>(await _productRepository.GetById(id));
        }

        public async Task UpdateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _productRepository.Update(product);

            await _productRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
            _stockService?.Dispose();
        }

    }
}
