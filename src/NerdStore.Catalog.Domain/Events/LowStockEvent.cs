using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

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
