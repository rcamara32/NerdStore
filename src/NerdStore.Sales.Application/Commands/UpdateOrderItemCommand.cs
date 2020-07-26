using NerdStore.Core.Messages;
using System;

namespace NerdStore.Sales.Application.Commands
{
    public class UpdateOrderItemCommand : Command
    {

        public Guid ClientId { get; private set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }

        public UpdateOrderItemCommand(Guid clientId, Guid orderId, Guid productId, int quantity)
        {
            ClientId = clientId;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }

        public override bool Isvalid()
        {
            ValidationResult = new UpdateOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
