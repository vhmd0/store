using System.ComponentModel.DataAnnotations;

namespace Store.Data.Entities;

public class Product : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public string PictureUrl { get; set; } = string.Empty;
    // ForeignKey For Brand

    public ProductBrand? Brand { get; set; }

    public int BrandId { get; set; }

    // ForeignKey for Types
    public ProductType? Type { get; set; }
    public int TypeId { get; set; }

    [DataType(DataType.Currency)] public decimal Price { get; set; }
}