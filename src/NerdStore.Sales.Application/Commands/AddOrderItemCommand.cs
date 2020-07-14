using NerdStore.Core.Messages;
using System;

namespace NerdStore.Sales.Application.Commands
{
    public class AddOrderItemCommand : Command
    {

        public Guid ClientId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }


        public AddOrderItemCommand(Guid clientId, Guid productId, string productName, int quantity, decimal unitPrice)
        {
            ClientId = clientId;
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public override bool Isvalid()
        {
            ValidationResult = new AddOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }


    }
}
