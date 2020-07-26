using NerdStore.Sales.Application.Queries.ViewModels;
using NerdStore.Sales.Domain.Enums;
using NerdStore.Sales.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Sales.Application.Queries
{

    public class OrderQueries : IOrderQueries
    {
        private readonly IOrderRepository _pedidoRepository;

        public OrderQueries(IOrderRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<CartViewModel> GetCartByClientId(Guid clientId)
        {
            var order = await _pedidoRepository.GetOrderDraftByClientId(clientId);
            if (order == null) return null;

            var cart = new CartViewModel
            {
                ClientId = order.ClientId,
                TotalOrder = order.TotalOrder,
                OrderId = order.Id,
                Discount = order.Discount,
                SubTotal = order.Discount + order.TotalOrder
            };

            if (order.VoucherId != null)
            {
                cart.VoucherCode = order.Voucher.Code;
            }

            foreach (var item in order.OrderItems)
            {
                cart.Items.Add(new CartItemViewModel
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Total = item.UnitPrice * item.Quantity
                });
            }

            return cart;
        }

        public async Task<IEnumerable<OrderViewModel>> GetClientOrders(Guid clientId)
        {
            var orders = await _pedidoRepository.GetOrderListByClientId(clientId);

            orders = orders.Where(p => p.OrderStatus == OrderStatus.Paid || p.OrderStatus == OrderStatus.Canceled)
                .OrderByDescending(p => p.Code);

            if (!orders.Any()) return null;

            var pedidosView = new List<OrderViewModel>();

            foreach (var pedido in orders)
            {
                pedidosView.Add(new OrderViewModel
                {
                    Id = pedido.Id,
                    TotalOrder = pedido.TotalOrder,
                    OrderStatus = (int)pedido.OrderStatus,
                    Code = pedido.Code,
                    CreatedDate = pedido.CreatedDate
                });
            }

            return pedidosView;
        }
    }
}
