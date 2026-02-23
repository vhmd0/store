using AutoMapper;
using Microsoft.Extensions.Caching.Hybrid;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Repository.Specification.Products;
using Store.Service.Helper;
using Store.Service.Services.Products.Dtos;

namespace Store.Service.Services.Products;

public class ProductServices(IUnitOfWork unitOfWork, IMapper mapper, HybridCache hybridCache)
    : IProductServices
{
    public async Task<ProductDetailsDto?> GetProductByIdAsync(int? id)
    {
        if (id is null)
            return null;

        var cacheKey = $"product_{id}";


        return await hybridCache.GetOrCreateAsync(cacheKey, async token =>
        {
            var spc = new ProductsWithSpecification(id.Value);
            var product = await unitOfWork
                .Repository<Product, int>()
                .GetDataWithSpecificationAsync(spc);

            return mapper.Map<ProductDetailsDto>(product);
        });
    }


    public async Task<PaginatedResultDto<IReadOnlyList<ProductDetailsDto>>> GetAllProductAsync(
        ProductSpecification input)
    {
        var cacheKey =
            $"products:page:{input.PageIndex}:size:{input.PageSize}:search:{input.Search}:type:{input.TypeId}:brand:{input.BrandId}:sort:{input.Sort}";
        return await hybridCache.GetOrCreateAsync(cacheKey, async token =>
        {
            var spcs = new ProductsWithSpecification(input);

            var allProducts = await unitOfWork.Repository<Product, int>().GetAllWithSpecificationAsync(spcs);

            var mappedProducts = mapper.Map<IReadOnlyList<ProductDetailsDto>>(allProducts);

            var count = await unitOfWork.Repository<Product, int>().CountAsync(spcs);

            return new PaginatedResultDto<IReadOnlyList<ProductDetailsDto>>(
                input.PageIndex,
                input.PageSize,
                count,
                mappedProducts);
        });
    }

    public async Task<IReadOnlyList<BrandTypeDto>> GetAllTypeAsync()
    {
        return await hybridCache.GetOrCreateAsync("all_types", async token =>
        {
            var types = await unitOfWork.Repository<ProductType, int>().GetAllAsync();
            return mapper.Map<IReadOnlyList<BrandTypeDto>>(types);
        });
    }

    public async Task<IReadOnlyList<BrandTypeDto>> GetAllBrandsAsync()
    {
        return await hybridCache.GetOrCreateAsync("all_brands", async token =>
        {
            var brands = await unitOfWork.Repository<ProductBrand, int>().GetAllAsync();
            return mapper.Map<IReadOnlyList<BrandTypeDto>>(brands);
        });
    }
}