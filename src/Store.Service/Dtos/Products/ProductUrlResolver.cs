using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Data.Entities;

namespace Store.Service.Dtos.Products;

public class ProductUrlResolver : IValueResolver<Product, ProductDetailsDto, string?>
{
    public ProductUrlResolver(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public string? Resolve(Product source, ProductDetailsDto destination, string? destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.PictureUrl))

            return Configuration["BaseUrl"] + source.PictureUrl;

        return null;
    }
}