using AutoMapper;
using Store.Data.Entities;

namespace Store.Service.Services.Product.Dtos;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Data.Entities.Product, ProductDetailsDto>()
            .ForMember(dest => dest.BrandName,
                opt => opt.MapFrom(src => src.Brand!.Name))
            .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Type));

        CreateMap<ProductBrand, ProductDetailsDto>();
        CreateMap<ProductType, ProductDetailsDto>();
    }
}