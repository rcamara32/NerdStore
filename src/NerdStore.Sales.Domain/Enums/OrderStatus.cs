using System;
using System.Collections.Generic;
using System.Text;

namespace NerdStore.Sales.Domain.Enums
{
    public enum OrderStatus
    {
        Draft = 0,
        Started = 1,
        //2
        //3
        Paid = 4,
        Delivered = 5,
        Canceled = 6,
    }
}
