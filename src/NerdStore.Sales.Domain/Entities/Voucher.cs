using NerdStore.Core.DomainObjects;
using NerdStore.Sales.Domain.Enums;
using System;
using System.Collections.Generic;

namespace NerdStore.Sales.Domain.Entities
{
    public class Voucher : Entity
    {
        public string Code { get; private set; }
        public decimal? Percent { get; private set; }
        public decimal? DiscountAmount { get; private set; }
        public int Quantity { get; private set; }
        public VoucherDiscountType VoucherDiscountType { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? ClaimedDate { get; private set; }
        public DateTime DueDate { get; private set; }
        public bool Active { get; private set; }
        public bool Claimed { get; private set; }

        //EF Relationship       
        public ICollection<Order> Orders { get; set; }




    }
}
