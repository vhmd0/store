using System.Linq.Expressions;

namespace Store.Repository.Specification;

public abstract class BaseSpecification<T> : ISpecification<T>
{
    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<T, bool>> Criteria { get; set; }


    public List<Expression<Func<T, object>>> Includes { get; set; } = new();

    protected void AddInclude(Expression<Func<T, object>> include)
    {
        Includes.Add(include);
    }
}