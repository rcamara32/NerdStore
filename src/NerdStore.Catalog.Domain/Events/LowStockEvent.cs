using NerdStore.Core.Messages.CommonMessages.Domainevents;
using System;

namespace NerdStore.Catalog.Domain.Events
{
    public class LowStockEvent : DomainEvent
    {
        public int QuantityStockRemaining { get; private set; }

        public LowStockEvent(Guid aggregateId, int quantityStockRemaining)
            : base(aggregateId)
        {
            QuantityStockRemaining = quantityStockRemaining;
        }

    }
}
