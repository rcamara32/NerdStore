using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain.Entities
{
    public class Dimensions
    {
        public decimal Height { get; private set; }
        public decimal Width { get; private set; }
        public decimal Depth { get; private set; }

        public Dimensions(decimal height, decimal width, decimal depth)
        {

            Height = height;
            Width = width;
            Depth = depth;

            Validate();
        }

        public void Validate()
        {
            Validations.ValidateLessThan(Height, 1, "The Height cannot be less or equals zero");
            Validations.ValidateLessThan(Width, 1, "The Width cannot be less or equals zero");
            Validations.ValidateLessThan(Depth, 1, "The Depth cannot be less or equals zero");
        }

        public string FormattedSize()
        {
            return $"WxHxD {Width} x {Height} x {Depth}";
        }
        public override string ToString()
        {
            return FormattedSize();
        }

    }
}
