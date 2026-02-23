using AutoMapper;
using Microsoft.Extensions.Caching.Hybrid;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Repository.Specification.Products;
using Store.Service.Helper;
using Store.Service.Services.Products.Dtos;

namespace Store.Service.Services.Products;

public class ProductServices : IProductServices
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly HybridCache _hybridCache;

    public ProductServices(IUnitOfWork unitOfWork, IMapper mapper, HybridCache hybridCache)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _hybridCache = hybridCache;
    }

    public async Task<ProductDetailsDto?> GetProductByIdAsync(int? id)
    {
        if (id is null)
            return null;

        var cacheKey = $"product_{id}";

        return await _hybridCache.GetOrCreateAsync(cacheKey, async token =>
        {
            var spc = new ProductsWithSpecification(id.Value);

            var product = await _unitOfWork
                .Repository<Product, int>()
                .GetDataWithSpecificationAsync(spc);

            return _mapper.Map<ProductDetailsDto>(product);
        });
    }

    public async Task<PaginatedResultDto<IReadOnlyList<ProductDetailsDto>>> GetAllProductAsync(ProductSpecification input)
    {
        var cacheKey = $"products_{input.PageIndex}_{input.PageSize}_{input.Search}_{input.TypeId}_{input.BrandId}_{input.Sort}";

        return await _hybridCache.GetOrCreateAsync(cacheKey, async token =>
        {
            var spcs = new ProductsWithSpecification(input);

            var allProducts = await _unitOfWork.Repository<Product, int>().GetAllWithSpecificationAsync(spcs);

            var mappedProducts = _mapper.Map<IReadOnlyList<ProductDetailsDto>>(allProducts);

            var count = await _unitOfWork.Repository<Product, int>().CountAsync(spcs);

            return new PaginatedResultDto<IReadOnlyList<ProductDetailsDto>>(
                input.PageIndex,
                input.PageSize,
                count,
                mappedProducts);
        });
    }
    public async Task<IReadOnlyList<BrandTypeDto>> GetAllTypeAsync()
    {
        return await _hybridCache.GetOrCreateAsync("all_types", async token =>
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IReadOnlyList<BrandTypeDto>>(types);
        });
    }

    public async Task<IReadOnlyList<BrandTypeDto>> GetAllBrandsAsync()
    {
        return await _hybridCache.GetOrCreateAsync("all_brands", async token =>
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IReadOnlyList<BrandTypeDto>>(brands);
        });
    }


}
