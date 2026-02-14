namespace Store.Repository.Specification.Products;

public class ProductSpecification
{
    public int? BrandId { get; set; }
    public int? TypeId { get; set; }

    public string? Sort { get; set; }
    public string? Search { get; set; }

    public int PageIndex { get; set; } = 1;

    private int _pageSize = 6;

    private const int MAXPAGENATION = 20;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MAXPAGENATION) ? MAXPAGENATION : (value <= 0 ? 1 : value);
    }
}
