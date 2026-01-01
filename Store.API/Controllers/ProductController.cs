using Microsoft.AspNetCore.Mvc;
using Store.Service.Services.Products;
using Store.Service.Services.Products.Dtos;

namespace Store.API.Controllers;

[Route("api/shop/[controller]/[action]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductServices _services;

    public ProductController(IProductServices services)
    {
        _services = services;
    }
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductDetailsDto>>> GetAllProducts()
    {
        return Ok(await _services.GetAllProductAsync());
    }


    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BrandTypeDto>>> GetAllBrands()
    {
        return Ok(await _services.GetAllBrandsAsync());
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BrandTypeDto>>> GetAllTypes()
    {
        return Ok(await _services.GetAllTypeAsync());
    }


    [HttpGet]
    public async Task<ActionResult<ProductDetailsDto>> GetProductById([FromQuery] int? id)
    {
        if (id == 0) return BadRequest();

        var product = await _services.GetProductByIdAsync(id);

        if (product is null)
            return NotFound();

        return Ok(product);
    }
}