using AutoMapper;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Service.Services.Product.Dto;
using Store.Service.Services.Product.Dtos;

namespace Store.Service.Services.Product
{
    public class ProductServices : IProductServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDetailsDto> GetProductByIdAsync(int? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var products = await _unitOfWork.Repository<Data.Entities.Product, int>().GetIdAsync(id);
            return _mapper.Map<ProductDetailsDto>(products);
        }

        public async Task<IReadOnlyList<ProductDetailsDto>> GetAllProductAsync()
        {
            var products = await _unitOfWork.Repository<Data.Entities.Product, int>().GetAllAsync();
            return _mapper.Map<IReadOnlyList<ProductDetailsDto>>(products);
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
}