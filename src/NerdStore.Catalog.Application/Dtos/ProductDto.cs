using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NerdStore.Catalog.Application.Dtos
{
    public class ProductDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string Image { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The field {0} value must be minimun of {1}")]
        [Required(ErrorMessage = "The field {0} is required")]
        public int StockQuantity { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The field {0} value must be minimun of {1}")]
        [Required(ErrorMessage = "The field {0} is required")]
        public int Height { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The field {0} value must be minimun of {1}")]
        [Required(ErrorMessage = "The field {0} is required")]
        public int Width { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The field {0} value must be minimun of {1}")]
        [Required(ErrorMessage = "The field {0} is required")]
        public int Depth { get; set; }

        public IEnumerable<CategoryDto> Categories { get; set; }
    }
}
