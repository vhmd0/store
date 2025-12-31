using Store.Data.Entities;

namespace Store.Repository.Interfaces;

public interface IGenericRepository<TEntity, in TKey> where TEntity : BaseEntity<TKey>
{
    Task<TEntity> GetIdAsync(int? id);

    // ReadOnly List
    Task<IReadOnlyList<TEntity>> GetAllAsync();

    //CRUD
    Task CreateAsync(TEntity entity);
    void UpdateAsync(TEntity entity);
    void DeleteAsync(TEntity entity);
}