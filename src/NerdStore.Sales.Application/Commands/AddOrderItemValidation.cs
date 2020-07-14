using FluentValidation;
using System;

namespace NerdStore.Sales.Application.Commands
{
    public class AddOrderItemValidation : AbstractValidator<AddOrderItemCommand>
    {
        
        public AddOrderItemValidation()
        {
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Client Id invalid");
            
            RuleFor(x => x.ProductId)
               .NotEqual(Guid.Empty)
               .WithMessage("Product Id invalid");

            RuleFor(x => x.ProductName)
               .NotEmpty()
               .WithMessage("Product name could not be empty");

            RuleFor(x => x.Quantity)
               .GreaterThan(0)
               .WithMessage("Minimum item quantity is 1");

            RuleFor(x => x.Quantity)
               .LessThanOrEqualTo(10)
               .WithMessage("Maximum item quantity is 10");

            RuleFor(x => x.UnitPrice)
               .GreaterThan(0)
               .WithMessage("The item unit proce should be greater than 0");

        }

    }
}
