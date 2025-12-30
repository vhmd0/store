using System.ComponentModel.DataAnnotations;

namespace Store.Data.Entities
{
    public class ProductBrand : BaseEntity<int>
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Sku { get; set; } = null!;
    }
}