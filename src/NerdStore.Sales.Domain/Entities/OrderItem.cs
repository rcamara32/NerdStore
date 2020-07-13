using NerdStore.Core.DomainObjects;
using System;

namespace NerdStore.Sales.Domain.Entities
{
    public class OrderItem : Entity
    {
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        //EF Relationship
        public virtual Order Order{ get; private set; }

        public OrderItem(Guid productId,  string productName, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;

        }

        protected OrderItem() { }

        internal void AssociateOrder(Guid orderId)
        {
            OrderId = orderId;
        }

        public decimal CalculateAmount()
        {
            return Quantity * UnitPrice;
        }

        internal void AddUnits(int units)
        {
            Quantity += units;
        }

        internal void UpdateUnits(int units)
        {
            Quantity = units;
        }

        internal void RemoveUnits(int units)
        {
            Quantity -= units;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
