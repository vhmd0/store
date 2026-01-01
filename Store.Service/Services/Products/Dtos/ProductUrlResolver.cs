using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Data.Entities;

namespace Store.Service.Services.Products.Dtos;

public class ProductUrlResolver : IValueResolver<Product, ProductDetailsDto, string>
{
    public ProductUrlResolver(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    string? IValueResolver<Product, ProductDetailsDto, string>.Resolve(Product source, ProductDetailsDto destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.PictureUrl))

            return Configuration["BaseUrl"] + source.PictureUrl;

        return null;
    }
}