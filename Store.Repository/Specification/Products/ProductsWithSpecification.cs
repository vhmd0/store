using Store.Data.Entities;

namespace Store.Repository.Specification.Products;

public class ProductsWithSpecification : BaseSpecification<Product>
{
    public ProductsWithSpecification(ProductSpecification spes) :
        base(product => !spes.BrandId.HasValue || (product.BrandId == spes.BrandId.Value
                                                   && !spes.TypeId.HasValue) || product.TypeId == spes.TypeId!.Value)
    {
        AddInclude(b => b.Brand!);
        AddInclude(t => t.Type!);
    }

    public ProductsWithSpecification(int id) : base(product => product.Id == id)
    {
        AddInclude(b => b.Brand!);
        AddInclude(t => t.Type!);
    }
}