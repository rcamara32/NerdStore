using NerdStore.Catalog.Domain.Interface.Repostory;
using NerdStore.Catalog.Domain.Interface.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Domain.Services
{
    public class StockService : IStockService
    {

        private readonly IProductRepository productRepository;

        public StockService(IProductRepository _productRepository)
        {
            productRepository = _productRepository;
        }

        public async Task<bool> Debit(Guid productId, int quantity)
        {
            var product = await productRepository.GetById(productId);

            if (product == null)
            {
                return false;
            }

            if (!product.HasStock(quantity))
            {
                return false;
            }

            product.StockDebit(quantity);
            productRepository.Update(product);
            return await productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Replenishment(Guid productId, int quantity)
        {
            var product = await productRepository.GetById(productId);

            if (product == null)
            {
                return false;
            }

            product.StockReplenishment(quantity);
            productRepository.Update(product);
            return await productRepository.UnitOfWork.Commit();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    productRepository.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~StockService()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion





    }
}
