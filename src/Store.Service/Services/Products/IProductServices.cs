using Store.Repository.Specification.Products;
using Store.Service.Helper;
using Store.Service.Services.Products.Dtos;

namespace Store.Service.Services.Products;

public interface IProductServices
{
    Task<ProductDetailsDto?> GetProductByIdAsync(int? id);

    Task<PaginatedResultDto<IReadOnlyList<ProductDetailsDto>>> GetAllProductAsync(ProductSpecification input);

    Task<IReadOnlyList<BrandTypeDto>> GetAllTypeAsync();
    Task<IReadOnlyList<BrandTypeDto>> GetAllBrandsAsync();

}