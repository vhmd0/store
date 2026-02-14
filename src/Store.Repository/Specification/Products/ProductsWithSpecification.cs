using Store.Data.Entities;

namespace Store.Repository.Specification.Products;

public class ProductsWithSpecification : BaseSpecification<Product>
{
    public ProductsWithSpecification(ProductSpecification spec)
        : base(product =>
            (!spec.BrandId.HasValue || product.BrandId == spec.BrandId.Value)
            && (!spec.TypeId.HasValue || product.TypeId == spec.TypeId.Value)
            && (string.IsNullOrEmpty(spec.Search) || (product.Name != null && product.Name.ToLower().Contains(spec.Search.ToLower())) || (product.Description != null && product.Description.ToLower().Contains(spec.Search.ToLower())))
        )
    {
        AddInclude(p => p.Brand!);
        AddInclude(p => p.Type!);
        AddOrderBy(p => p.Name!);
        ApplyPaging(spec.PageSize * (spec.PageIndex - 1), spec.PageSize);

        if (!string.IsNullOrEmpty(spec.Sort))
        {
            switch (spec.Sort)
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;
                case "priceDesc":
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Name!);
                    break;
            }
        }
    }

    public ProductsWithSpecification(int id)
        : base(product => product.Id == id)
    {
        AddInclude(p => p.Brand);
        AddInclude(p => p.Type);
    }
}
