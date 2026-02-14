using Amazon.Runtime;
using Microsoft.AspNetCore.Mvc;
using Store.Repository.Specification.Products;
using Store.Service.Services.Products;
using Store.Service.Services.Products.Dtos;

namespace Store.API.Controllers;

[Route("api/shop/[controller]/[action]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductServices _services;

    public ProductController(IProductServices services) => _services = services;

    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<IReadOnlyList<ProductDetailsDto>>>> GetAllProducts([FromQuery] ProductSpecification input)
    => Ok(await _services.GetAllProductAsync(input));



    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BrandTypeDto>>> GetAllBrands() => Ok(await _services.GetAllBrandsAsync());


    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BrandTypeDto>>> GetAllTypes() => Ok(await _services.GetAllTypeAsync());



    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDetailsDto>> GetProductById(int id)
    {
        if (id <= 0) return BadRequest();

        var product = await _services.GetProductByIdAsync(id);

        if (product == null) return NotFound();

        return Ok(product);
    }
}