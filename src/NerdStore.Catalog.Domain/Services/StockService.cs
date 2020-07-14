using NerdStore.Catalog.Domain.Events;
using NerdStore.Catalog.Domain.Interface.Repostory;
using NerdStore.Catalog.Domain.Interface.Service;
using NerdStore.Core.Communication.Mediator;
using System;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Domain.Services
{
    public class StockService : IStockService
    {

        private readonly IProductRepository _productRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public StockService(IProductRepository productRepository,
            IMediatorHandler mediatorHandler)
        {
            _productRepository = productRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Debit(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null)
            {
                return false;
            }

            if (!product.HasStock(quantity))
            {
                return false;
            }

            product.StockDebit(quantity);

            //TODO: parametrize this value
            if (product.StockQuantity < 5)
            {
                await _mediatorHandler.PublishEvent(new LowStockEvent(product.Id, product.StockQuantity));
            }

            _productRepository.Update(product);
            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Replenishment(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null)
            {
                return false;
            }

            product.StockReplenishment(quantity);
            _productRepository.Update(product);
            return await _productRepository.UnitOfWork.Commit();
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
                    _productRepository.Dispose();
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
