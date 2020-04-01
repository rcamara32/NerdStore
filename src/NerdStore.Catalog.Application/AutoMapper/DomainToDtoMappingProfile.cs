using AutoMapper;
using NerdStore.Catalog.Application.Dtos;
using NerdStore.Catalog.Domain.Entities;

namespace NerdStore.Catalog.Application.AutoMapper
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.Height, o => o.MapFrom(s => s.Dimensions.Height))
                .ForMember(d => d.Width, o => o.MapFrom(s => s.Dimensions.Width))
                .ForMember(d => d.Depth, o => o.MapFrom(s => s.Dimensions.Depth));

            CreateMap<Category, CategoryDto>();
        }


    }
}
