using System;
using System.ComponentModel.DataAnnotations;

namespace NerdStore.Catalog.Application.Dtos
{
    public class CategoryDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public int Code { get; set; }
    }
}
