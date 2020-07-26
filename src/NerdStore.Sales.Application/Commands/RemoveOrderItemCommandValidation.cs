using FluentValidation;
using System;

namespace NerdStore.Sales.Application.Commands
{
    public class RemoveOrderItemCommandValidation : AbstractValidator<RemoveOrderItemCommand>
    {
        public RemoveOrderItemCommandValidation()
        {
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Client Id invalid");

            RuleFor(x => x.ProductId)
               .NotEqual(Guid.Empty)
               .WithMessage("Product Id invalid");

            RuleFor(x => x.OrderId)
               .NotEqual(Guid.Empty)
               .WithMessage("Order Id invalid");

        }
    }
}
