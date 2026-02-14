using Microsoft.EntityFrameworkCore;
using Store.Data.Entities;

namespace Store.Repository.Specification.Products;

public class SpecificationEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
    {
        var query = inputQuery;

        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }

        if (spec.Includes != null)
        {
            query = spec.Includes.Aggregate(query,
                (current, include) => current.Include(include));
        }

        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        if (spec.IsPagingEnabled)
            query = query.Skip(spec.Skip).Take(spec.Take);

        return query;
    }
}