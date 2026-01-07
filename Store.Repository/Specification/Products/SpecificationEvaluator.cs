using Microsoft.EntityFrameworkCore;
using Store.Data.Entities;

namespace Store.Repository.Specification.Products;

public class SpecificationEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
    {
        var query = inputQuery;

        query = query.Where(spec.Criteria);

        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }
}