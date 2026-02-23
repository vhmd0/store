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

    /// <summary>
    /// Initializes a new instance of ProductServices with the required dependencies.
    /// </summary>
    /// <param name="unitOfWork">Unit of work providing repository access and transactional operations.</param>
    /// <param name="mapper">Object-object mapper used to convert entities to DTOs.</param>
    /// <param name="hybridCache">Caching provider used to store and retrieve product-related data.</param>
    public ProductServices(IUnitOfWork unitOfWork, IMapper mapper, HybridCache hybridCache)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _hybridCache = hybridCache;
    }

    /// <summary>
    /// Retrieves the product details for the specified product id, using a cached value when available.
    /// </summary>
    /// <param name="id">The product identifier; if null, the method returns null.</param>
    /// <returns>A <see cref="ProductDetailsDto"/> for the specified product, or null if the id is null or the product is not found.</returns>
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

    /// <summary>
    /// Retrieves a paginated list of products that match the provided specification.
    /// </summary>
    /// <param name="input">Specification containing paging, filtering, and sorting criteria.</param>
    /// <returns>A PaginatedResultDto containing the page index, page size, total item count, and the mapped list of ProductDetailsDto.</returns>
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
    /// <summary>
    /// Retrieve all product types from cache or the data store.
    /// </summary>
    /// <returns>A read-only list of BrandTypeDto representing all product types.</returns>
    public async Task<IReadOnlyList<BrandTypeDto>> GetAllTypeAsync()
    {
        return await _hybridCache.GetOrCreateAsync("all_types", async token =>
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IReadOnlyList<BrandTypeDto>>(types);
        });
    }

    /// <summary>
    /// Retrieves all product brands mapped to BrandTypeDto.
    /// </summary>
    /// <returns>A read-only list of BrandTypeDto representing all product brands.</returns>
    public async Task<IReadOnlyList<BrandTypeDto>> GetAllBrandsAsync()
    {
        return await _hybridCache.GetOrCreateAsync("all_brands", async token =>
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IReadOnlyList<BrandTypeDto>>(brands);
        });
    }


}