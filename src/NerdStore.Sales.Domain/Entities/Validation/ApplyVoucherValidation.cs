using FluentValidation;
using System;

namespace NerdStore.Sales.Domain.Entities.Validation
{
    public class ApplyVoucherValidation : AbstractValidator<Voucher>
    {

        public ApplyVoucherValidation()
        {
            RuleFor(c => c.DueDate)
                .Must(DueDateGreaterThanToday)
                .WithMessage("This voucher is expired");

            RuleFor(c => c.Active)
                .Equal(true)
                .WithMessage("This voucher is no longer valid.");

            RuleFor(c => c.Claimed)
                .Equal(false)
                .WithMessage("This voucher has already been used.");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("This voucher is no longer available");
        }

        protected static bool DueDateGreaterThanToday(DateTime dueDate)
        {
            return dueDate >= DateTime.Now;
        }
    }

}
