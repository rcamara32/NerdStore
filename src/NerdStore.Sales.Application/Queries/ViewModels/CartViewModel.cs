using System;
using System.Collections.Generic;

namespace NerdStore.Sales.Application.Queries.ViewModels
{

    public class CartViewModel
    {
        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalOrder { get; set; }
        public decimal Discount { get; set; }
        public string VoucherCode { get; set; }

        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();
        public CartPaymentViewModel Payment { get; set; }

    }
}
