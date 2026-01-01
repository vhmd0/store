using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entities;
using Store.Repository.Interfaces;

namespace Store.Repository.Repositories;

public class GenericRepository<TEntity, TKey>
    : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    private readonly StoreDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(StoreDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(TKey id)
       => await _dbSet.FindAsync(id);


    public async Task<IReadOnlyList<TEntity>> GetAllAsync()
     => await _dbSet.ToListAsync();


    public async Task CreateAsync(TEntity entity)
     => await _dbSet.AddAsync(entity);


    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
}