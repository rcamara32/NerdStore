using NerdStore.Core.Messages;
using System;

namespace NerdStore.Sales.Application.Commands
{
    public class ApplyVoucherOrderCommand : Command
    {

        public Guid ClientId { get; private set; }
        public string VoucherCode { get; set; }

        public ApplyVoucherOrderCommand(Guid clientId, string voucherCode)
        {
            ClientId = clientId;
            VoucherCode = voucherCode;
        }

        public override bool Isvalid()
        {
            ValidationResult = new ApplyVoucherOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
