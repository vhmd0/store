using Store.Service.Services.Product.Dto;
using Store.Service.Services.Product.Dtos;

namespace Store.Service.Services.Product
{
    public interface IProductServices
    {
        Task<ProductDetailsDto> GetProductByIdAsync(int? id);

        Task<IReadOnlyList<ProductDetailsDto>> GetAllProductAsync();

        Task<IReadOnlyList<BrandTypeDto>> GetAllTypeAsync();
        Task<IReadOnlyList<BrandTypeDto>> GetAllBrandsAsync();
    }
}