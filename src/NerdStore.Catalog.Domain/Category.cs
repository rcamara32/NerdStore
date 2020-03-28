using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace NerdStore.Catalog.Domain
{
    public class Category : Entity
    {
        public string Name { get; private set; }
        public int Code { get; private set; }

        //EF Relation
        public ICollection<Product> Products { get; set; }

        protected Category() { }

        public Category(string name, int code)
        {
            Name = name;
            Code = code;

            Validate();
        }

        public override string ToString()
        {
            return $"{Name} - {Code}";
        }

        public void Validate()
        {
            Validations.ValidateIsEmpty(Name, "The Category Name cannot be empty");
            Validations.IfEquals(Code, 0, "The Category Code cannot be 0");
        }

    }
}
