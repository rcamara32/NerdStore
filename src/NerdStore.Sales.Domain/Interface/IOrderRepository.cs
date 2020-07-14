using NerdStore.Core.Data;
using NerdStore.Sales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.Sales.Domain.Interface
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        Task<Order> GetById(Guid id);
        Task<IEnumerable<Order>> GetOrderListByClientId(Guid clientId);
        Task<Order> GetOrderDraftByClientId(Guid clientId);
        void Add(Order Order);
        void Update(Order Order);

        Task<OrderItem> GetItemById(Guid id); 
        Task<OrderItem> GetItemByOrderId(Guid pedidoId, Guid productId);
        void AddItem(OrderItem iem);
        void UpdateItem(OrderItem item);
        void RemoveItem(OrderItem item);

        Task<Voucher> GetVoucherByCode(string code);
    }
}
