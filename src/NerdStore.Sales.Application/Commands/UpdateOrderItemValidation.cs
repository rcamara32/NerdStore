using FluentValidation;
using System;

namespace NerdStore.Sales.Application.Commands
{
    public class UpdateOrderItemValidation : AbstractValidator<UpdateOrderItemCommand>
    {
        public UpdateOrderItemValidation()
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

            RuleFor(x => x.Quantity)
               .GreaterThan(0)
               .WithMessage("Minimum item quantity is 1");

            RuleFor(x => x.Quantity)
               .LessThanOrEqualTo(10)
               .WithMessage("Maximum item quantity is 10");
            

        }
    }
}
