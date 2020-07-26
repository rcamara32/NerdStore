using FluentValidation;
using System;

namespace NerdStore.Sales.Application.Commands
{
    public class ApplyVoucherOrderValidation : AbstractValidator<ApplyVoucherOrderCommand>
    {
        public ApplyVoucherOrderValidation()
        {
            RuleFor(x => x.ClientId)
               .NotEqual(Guid.Empty)
               .WithMessage("Client Id invalid");
          
            RuleFor(x => x.VoucherCode)
               .NotEmpty()
               .WithMessage("Voucher can not be empty");
            
            RuleFor(x => x.VoucherCode)
               .MinimumLength(5)
               .WithMessage("Voucher invalid");

        }
    }
}
