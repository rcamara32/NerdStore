using NerdStore.Core.Messages;
using System;

namespace NerdStore.Sales.Application.Events
{
    public class OrderUpdatedEvent : Event
    {
        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public decimal TotalOrder { get; private set; }

        public OrderUpdatedEvent(Guid clientId, Guid orderId, decimal totalOrder)
        {
            AggregateId = orderId;

            ClientId = clientId;
            OrderId = orderId;
            TotalOrder = totalOrder;

        }

    }
}
