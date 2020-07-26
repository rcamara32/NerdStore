using FluentValidation.Results;
using NerdStore.Core.DomainObjects;
using NerdStore.Sales.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.Sales.Domain.Entities
{
    public class Order : Entity, IAggregateRoot
    {

        public int Code { get; private set; }
        public Guid ClientId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool ClaimedVoucher { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalOrder { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public OrderStatus OrderStatus { get; private set; }

        //prevent users manipulate inappropriately order items list.
        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        //EF Relationship
        public virtual Voucher Voucher { get; private set; }


        public Order(Guid clientId, bool claimedVoucher, decimal discount, decimal totalOrder)
        {
            ClientId = clientId;
            ClaimedVoucher = claimedVoucher;
            Discount = discount;
            TotalOrder = totalOrder;

            _orderItems = new List<OrderItem>();
        }

        protected Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public void CalculteTotalOrder()
        {
            TotalOrder = OrderItems.Sum(i => i.CalculateAmount());
            CalculateDiscount();
        }

        public void CalculateDiscount()
        {

            if (!ClaimedVoucher) return;

            decimal discount = 0;
            var total = TotalOrder;

            if (Voucher.VoucherDiscountType == VoucherDiscountType.Percentage)
            {
                if (Voucher.Percent.HasValue)
                {
                    discount = (total * Voucher.Percent.Value) / 100;
                    total -= discount;
                }
            }
            else
            {
                if (Voucher.DiscountAmount.HasValue)
                {
                    discount = Voucher.DiscountAmount.Value;
                    total -= discount;
                }
            }

            TotalOrder = total < 0 ? 0 : total;
            Discount = discount;
        }

        public ValidationResult ApplyVoucher(Voucher voucher)
        {
            var validation = voucher.Validate();
            if (!validation.IsValid) return validation;

            Voucher = voucher;
            ClaimedVoucher = true;
            CalculteTotalOrder();

            return validation;
        }

        public bool IsAlreadyItemInOrder(OrderItem orderItem)
        {
            return
                _orderItems.Any(i => i.ProductId.Equals(orderItem.ProductId));
        }

        public void AddItemOrder(OrderItem item)
        {
            if (item == null) return;
            if (!item.IsValid()) return;

            item.AssociateOrder(Id);

            if (IsAlreadyItemInOrder(item))
            {
                var existingItem = _orderItems.FirstOrDefault(i => i.ProductId == item.ProductId);
                existingItem.AddUnits(item.Quantity);
                item = existingItem;

                _orderItems.Remove(existingItem);
            }

            item.CalculateAmount();
            _orderItems.Add(item);

            CalculteTotalOrder();
        }

        public void RemoveItem(OrderItem item)
        {
            if (item == null) return;
            if (!item.IsValid()) return;

            var existingItem = _orderItems.FirstOrDefault(i => i.ProductId == item.ProductId);

            if (existingItem == null) throw new DomainException("The item does not exist in this Order");
            _orderItems.Remove(existingItem);

            CalculteTotalOrder();
        }

        public void UpdateItem(OrderItem item)
        {
            if (item == null) return;
            if (!item.IsValid()) return;

            item.AssociateOrder(Id);
            var existingItem = _orderItems.FirstOrDefault(i => i.ProductId == item.ProductId);

            if (existingItem == null) throw new DomainException("The item does not exist in this Order");
            _orderItems.Remove(existingItem);


            _orderItems.Add(item);

            CalculteTotalOrder();
        }

        public void UpdateUnits(OrderItem item, int units)
        {
            if (item == null) return;

            item.UpdateUnits(units);
            UpdateItem(item);
        }

        public void OrderDraft()
        {
            OrderStatus = OrderStatus.Draft;
            CreatedDate = DateTime.Now;
        }

        public void OrderStarted()
        {
            OrderStatus = OrderStatus.Started;
        }

        public void OrderPaid()
        {
            OrderStatus = OrderStatus.Paid;
        }

        public void OrderCanceled()
        {
            OrderStatus = OrderStatus.Canceled;
        }

        public void OrderDelivered()
        {
            OrderStatus = OrderStatus.Delivered;
        }

        public static class OrderFactory
        {
            public static Order NewOrderDraft(Guid clientId)
            {
                var order = new Order
                {
                    ClientId = clientId
                };

                order.OrderDraft();
                return order;
            }
        }

    }
}
