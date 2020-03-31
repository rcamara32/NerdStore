using MediatR;
using NerdStore.Catalog.Domain.Interface.Repostory;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Domain.Events
{
    public class ProductEventHandler : INotificationHandler<LowStockEvent>
    {

        private readonly IProductRepository productRepository;

        public ProductEventHandler(IProductRepository _productRepository)
        {
            productRepository = productRepository;
        }

        public async Task Handle(LowStockEvent notification, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetById(notification.AggregateId);

            //TODO:
            //alert, send an email, open task, new product order
            //integration to another systems...

        }
    }
}
