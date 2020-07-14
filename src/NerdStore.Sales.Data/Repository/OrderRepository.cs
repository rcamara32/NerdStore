using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Data;
using NerdStore.Sales.Domain.Entities;
using NerdStore.Sales.Domain.Enums;
using NerdStore.Sales.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Sales.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SalesContext _context;

        public OrderRepository(SalesContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Order> GetById(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetOrderListByClientId(Guid clientId)
        {
            return await _context.Orders.AsNoTracking().Where(p => p.ClientId == clientId).ToListAsync();
        }

        public async Task<Order> GetOrderDraftByClientId(Guid clientId)
        {
            var Order = await _context.Orders.FirstOrDefaultAsync(p => p.ClientId == clientId && p.OrderStatus == OrderStatus.Draft);
            if (Order == null) return null;

            await _context.Entry(Order)
                .Collection(i => i.OrderItems).LoadAsync();

            if (Order.VoucherId != null)
            {
                await _context.Entry(Order)
                    .Reference(i => i.Voucher).LoadAsync();
            }

            return Order;
        }

        public void Add(Order Order)
        {
            _context.Orders.Add(Order);
        }

        public void Update(Order Order)
        {
            _context.Orders.Update(Order);
        }


        public async Task<OrderItem> GetItemById(Guid id)
        {
            return await _context.OrderItems.FindAsync(id);
        }

        public async Task<OrderItem> GetItemByOrderId(Guid orderId, Guid productId)
        {
            return await _context.OrderItems.FirstOrDefaultAsync(p => p.ProductId == productId && p.OrderId == orderId);
        }

        public void AddItem(OrderItem item)
        {
            _context.OrderItems.Add(item);
        }

        public void UpdateItem(OrderItem item)
        {
            _context.OrderItems.Update(item);
        }

        public void RemoveItem(OrderItem item)
        {
            _context.OrderItems.Remove(item);
        }

        public async Task<Voucher> GetVoucherByCode(string code)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(p => p.Code == code);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
