using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Sales.Application.Events;
using NerdStore.Sales.Domain.Entities;
using NerdStore.Sales.Domain.Interface;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Sales.Application.Commands
{
    public class OrderCommandHandler :
        IRequestHandler<AddOrderItemCommand, bool>,
        IRequestHandler<ApplyVoucherOrderCommand, bool>,
        IRequestHandler<RemoveOrderItemCommand, bool>,
        IRequestHandler<UpdateOrderItemCommand, bool>
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public OrderCommandHandler(IOrderRepository orderRepository,
             IMediatorHandler mediatorHandler)
        {
            _orderRepository = orderRepository;
            _mediatorHandler = mediatorHandler;
        }


        public async Task<bool> Handle(AddOrderItemCommand message, CancellationToken cancellationToken)
        {
            if (!IsCommandValid(message)) return false;

            var order = await _orderRepository.GetOrderDraftByClientId(message.ClientId);
            var item = new OrderItem(message.ProductId, message.ProductName, message.Quantity, message.UnitPrice);

            if (order == null)
            {
                order = Order.OrderFactory.NewOrderDraft(message.ClientId);
                order.AddItemOrder(item);

                _orderRepository.Add(order);
                order.AddEvents(new OrderDraftStartedEvent(message.ClientId, order.Id));
            }
            else
            {
                var existingItem = order.IsAlreadyItemInOrder(item);
                order.AddItemOrder(item);

                if (existingItem)
                {
                    _orderRepository.UpdateItem(order.OrderItems.FirstOrDefault(i => i.ProductId == item.ProductId));
                }
                else
                {
                    _orderRepository.AddItem(item);
                }

                order.AddEvents(new OrderUpdatedEvent(message.ClientId, order.Id, order.TotalOrder));
            }

            order.AddEvents(new OrderItemAddedEvent(message.ClientId, order.Id, message.ProductId,
                        message.ProductName, message.UnitPrice, message.Quantity));

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(ApplyVoucherOrderCommand message, CancellationToken cancellationToken)
        {
            if (!IsCommandValid(message)) return false;

            var order = await _orderRepository.GetOrderDraftByClientId(message.ClientId);

            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Order", "Order not found!"));
                return false;
            }

            if (order.ClaimedVoucher)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Order", "This order has already claimed a voucher"));
                return false;
            }

            var voucher = await _orderRepository.GetVoucherByCode(message.VoucherCode);

            if (voucher == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Order", "Voucher not found!"));
                return false;
            }
            
            var appliedVoucher = order.ApplyVoucher(voucher);
            if (!appliedVoucher.IsValid)
            {
                foreach (var error in appliedVoucher.Errors)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }

                return false;
            }

            order.AddEvents(new VoucherAppliedEvent(message.ClientId, order.Id, voucher.Id));
            _orderRepository.Update(order);
            
            return await _orderRepository.UnitOfWork.Commit();
        }

        public Task<bool> Handle(RemoveOrderItemCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        private bool IsCommandValid(Command message)
        {
            if (message.Isvalid()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
