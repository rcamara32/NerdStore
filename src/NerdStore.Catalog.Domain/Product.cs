using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace NerdStore.Catalog.Domain
{
    public class Product : Entity, IAggregateRoot
    {
        public Guid CategoryId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }
        public decimal Price { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string Image { get; private set; }
        public int StockQuantity { get; private set; }

        public virtual Category Category { get; set; }

        public Product(string name, string description, bool isActive, decimal price,
            Guid categoryId, DateTime createdDate, string image)
        {
            Name = name;
            Description = description;
            IsActive = isActive;
            Price = price;
            CategoryId = categoryId;
            CreatedDate = createdDate;
            Image = image;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;

        public void ChangeCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
        }

        public void ChangeDescription(string description)
        {
            Description = description;
        }

        public void StockDebit(int quantity) 
        {
            if (quantity < 0)
                quantity *= -1;

            StockQuantity -= quantity;
        }

        public void StockReplenishment(int quantity)
        {
            StockQuantity += quantity;
        }

        public bool HasStock(int quantity) 
        {
            return StockQuantity >= quantity;
        }

        public void Valid()
        { 
            
        }

    }
}
