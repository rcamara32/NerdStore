using NerdStore.Core.Messages;
using System;

namespace NerdStore.Sales.Application.Events
{
    public class OrderDraftStartedEvent : Event
    {
        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
    
        public OrderDraftStartedEvent(Guid clientId, Guid orderId)
        {
            AggregateId = orderId;
            ClientId = clientId;
            OrderId = orderId;
        }

    }
}
