using AutoMapper;
using Store.Data.Entities;

namespace Store.Service.Dtos.Products;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        // Product → ProductDetailsDto
        CreateMap<Product, ProductDetailsDto>()
            .ForMember(dest => dest.BrandName,
                opt => opt.MapFrom(src => src.Brand!.Name))
            .ForMember(dest => dest.TypeName,
                opt => opt.MapFrom(src => src.Type!.Name))
#pragma warning disable CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.
            .ForMember(dest => dest.PictureUrl, opts => opts.MapFrom<ProductUrlResolver>());
#pragma warning restore CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.

        // ProductBrand → BrandTypeDto
        CreateMap<ProductBrand, BrandTypeDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        // ProductType → BrandTypeDto
        CreateMap<ProductType, BrandTypeDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}