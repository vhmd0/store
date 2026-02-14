using AutoMapper;
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

    public ProductServices(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductDetailsDto?> GetProductByIdAsync(int? id)
    {
        if (id is null)
            return null;

        var spc = new ProductsWithSpecification(id.Value);

        var product = await _unitOfWork
            .Repository<Product, int>()
            .GetDataWithSpecificationAsync(spc);

        return _mapper.Map<ProductDetailsDto>(product);
    }

    public async Task<PaginatedResultDto<IReadOnlyList<ProductDetailsDto>>> GetAllProductAsync(ProductSpecification input)
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
    }
    public async Task<IReadOnlyList<BrandTypeDto>> GetAllTypeAsync()
    {
        var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();
        return _mapper.Map<IReadOnlyList<BrandTypeDto>>(types);
    }

    public async Task<IReadOnlyList<BrandTypeDto>> GetAllBrandsAsync()
    {
        var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();
        return _mapper.Map<IReadOnlyList<BrandTypeDto>>(brands);
    }


}
