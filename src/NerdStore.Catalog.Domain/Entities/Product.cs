using NerdStore.Core.DomainObjects;
using System;

namespace NerdStore.Catalog.Domain.Entities
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
        
        public Dimensions Dimensions { get; private set; }
        
        //EF Relation
        public Category Category { get; private set; }

        protected Product() { }
        public Product(string name, string description, bool isActive, decimal price,
            Guid categoryId, DateTime createdDate, string image, Dimensions dimensions)
        {
            Name = name;
            Description = description;
            IsActive = isActive;
            Price = price;
            CategoryId = categoryId;
            CreatedDate = createdDate;
            Image = image;
            Dimensions = dimensions;

            Validate();
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
            Validations.ValidateIsEmpty(description, "The Product Description cannot be empty");
            Description = description;
        }

        public void StockDebit(int quantity)
        {
            if (quantity < 0)
                quantity *= -1;

            if (!HasStock(quantity))
            {
                throw new DomainException($"{Name} product stock is insufficient");
            }

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

        public void Validate()
        {
            Validations.ValidateIsEmpty(Name, "The Product Name cannot be empty");
            Validations.ValidateIsEmpty(Description, "The Product Description cannot be empty");
            Validations.IfDifferent(CategoryId, Guid.Empty, "The Product Category Id cannot be empty");
            Validations.ValidateLessThan(Price, 1, "The Product Price cannot be less or equals zero");
            Validations.ValidateIsEmpty(Image, "The Product image cannot be empty");
        }

    }
}
