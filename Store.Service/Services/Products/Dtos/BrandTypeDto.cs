namespace Store.Service.Dtos.Products;

public class BrandTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}