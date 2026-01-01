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
                opt => opt.MapFrom(src => src.Brand!.Name))
            .ForMember(dest => dest.TypeName,
                opt => opt.MapFrom(src => src.Type.Name))
            .ForMember(dest => dest.PictureUrl, opts => opts.MapFrom<ProductUrlResolver>()); // ensure Type has Name

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