using System;

namespace NerdStore.Sales.Application.Queries.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public decimal TotalOrder { get; set; }
        public DateTime CreatedDate { get; set; }
        public int OrderStatus { get; set; }

    }
}
