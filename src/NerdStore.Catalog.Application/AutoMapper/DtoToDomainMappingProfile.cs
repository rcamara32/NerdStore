using AutoMapper;
using NerdStore.Catalog.Application.Dtos;
using NerdStore.Catalog.Domain.Entities;

namespace NerdStore.Catalog.Application.AutoMapper
{
    public class DtoToDomainMappingProfile : Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<ProductDto, Product>()
                .ConstructUsing(p =>
                    new Product(p.Name, p.Description, p.IsActive,
                        p.Price, p.CategoryId, p.CreatedDate,
                        p.Image, new Dimensions(p.Height, p.Width, p.Depth)));

            CreateMap<CategoryDto, Category>()
                .ConstructUsing(c => new Category(c.Name, c.Code));
        }
    }
}
