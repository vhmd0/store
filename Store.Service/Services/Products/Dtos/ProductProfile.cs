using AutoMapper;
using Store.Data.Entities;

namespace Store.Service.Services.Products.Dtos;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        // Product → ProductDetailsDto
        CreateMap<Product, ProductDetailsDto>()
            .ForMember(dest => dest.BrandName,
                opt => opt.MapFrom(src => src.Brand != null ? src.Brand.Name : null))
            .ForMember(dest => dest.TypeName,
                opt => opt.MapFrom(src => src.Type != null ? src.Type.Name : null))
            .ForMember(dest => dest.PictureUrl, opts => opts.MapFrom<ProductUrlResolver>());

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