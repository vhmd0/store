using Store.Service.Services.Products.Dtos;

namespace Store.Service.Services.Products
{
    public interface IProductServices
    {
        Task<ProductDetailsDto> GetProductByIdAsync(int? id);

        Task<IReadOnlyList<ProductDetailsDto>> GetAllProductAsync();

        Task<IReadOnlyList<BrandTypeDto>> GetAllTypeAsync();
        Task<IReadOnlyList<BrandTypeDto>> GetAllBrandsAsync();
    }
}