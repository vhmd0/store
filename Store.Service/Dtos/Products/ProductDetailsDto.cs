using System.ComponentModel.DataAnnotations;

namespace Store.Service.Dtos.Products;

public class ProductDetailsDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string? PictureUrl { get; set; }
    public string? BrandName { get; set; }
    public string? TypeName { get; set; }

    [DataType(DataType.Currency)] public decimal Price { get; set; }
}