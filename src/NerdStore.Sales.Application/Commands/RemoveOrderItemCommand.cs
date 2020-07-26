using NerdStore.Core.Messages;
using System;

namespace NerdStore.Sales.Application.Commands
{
    public class RemoveOrderItemCommand : Command
    {

        public Guid ClientId { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid OrderId { get; private set; }


        public RemoveOrderItemCommand(Guid clientId, Guid productId, Guid orderId)
        {
            ClientId = clientId;
            ProductId = productId;
            OrderId = orderId;
        }

        public override bool Isvalid()
        {
            ValidationResult = new RemoveOrderItemCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
