using Store.Service.Services.Product.Dto;

namespace Store.Service.Services.Product
{
    public interface IProductServices
    {
        Task<ProductDetailsDto> GetProductByIdAsync(int id);

        Task<IReadOnlyList<ProductDetailsDto>> GetAllProductAsync();

        Task<IReadOnlyList<BrandTypeDto>> GetAllTypeAsync();
        Task<IReadOnlyList<BrandTypeDto>> GetAllBrandsAsync();
    }
}