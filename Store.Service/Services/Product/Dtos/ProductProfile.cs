using AutoMapper;
using Store.Data.Entities;
using Store.Service.Services.Product.Dto;

namespace Store.Service.Services.Product.Dtos;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Data.Entities.Product, ProductDetailsDto>()
            // Get the Data From the ForeignKey Brand
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand!.Name))
            // Get the Data From the ForeignKey Type
            .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Type));

        CreateMap<ProductBrand, ProductDetailsDto>();
        CreateMap<ProductType, ProductDetailsDto>();
    }
}