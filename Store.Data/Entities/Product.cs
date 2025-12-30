using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Data.Entities
{
    public class Product : BaseEntity<int>
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name must be 50 characters or fewer.")]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 500 characters.")]
        public string Description { get; set; } = null!;

        [Column(TypeName = "decimal(10,2)")] public decimal Price { get; set; }

        [Required]
        [Url(ErrorMessage = "PictureUrl must be a valid URL.")]
        public string PictureUrl { get; set; } = null!;

        public ProductBrand Brand { get; set; } = null!;

        [ForeignKey(nameof(Brand))] public int BrandId { get; set; }

        public ProductType Type { get; set; } = null!;
        [ForeignKey(nameof(Type))] public int TypeId { get; set; }

        [Range(0, 1000, ErrorMessage = "Quantity must be between 0 and 1000.")]
        public int QuantityInStock { get; set; }
        public bool IsActive { get; set; } = true;

    }
}