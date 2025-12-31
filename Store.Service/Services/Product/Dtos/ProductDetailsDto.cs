using System.ComponentModel.DataAnnotations;

namespace Store.Service.Services.Product.Dto
{
    public class ProductDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string PictureUrl { get; set; } = string.Empty;
        public string BrandName { get; set; } = null!;
        public string TypeName { get; set; } = null!;

        [DataType(DataType.Currency)] public decimal Price { get; set; }
    }
}