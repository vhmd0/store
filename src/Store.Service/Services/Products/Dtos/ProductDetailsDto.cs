using System.ComponentModel.DataAnnotations;

namespace Store.Service.Dtos.Products;

public class ProductDetailsDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string PictureUrl { get; set; }
    public required string BrandName { get; set; }
    public required string TypeName { get; set; }

    [DataType(DataType.Currency)] public decimal Price { get; set; }
}