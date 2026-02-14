using Store.Data.Entities;
using Store.Repository.Specification;

namespace Store.Repository.Interfaces;

public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    Task<TEntity?> GetByIdAsync(TKey id);

    Task<TEntity?> GetDataWithSpecificationAsync(ISpecification<TEntity> spec);
    Task<IReadOnlyList<TEntity>> GetAllAsync();
    Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> spec);

    Task CreateAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}