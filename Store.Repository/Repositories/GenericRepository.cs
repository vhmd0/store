using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entities;
using Store.Repository.Interfaces;

namespace Store.Repository.Repositories;

public class GenericRepository<TEntity, TKey>
    : IGenericRepository<TEntity, TKey> where TEntity
    : BaseEntity<TKey>
{
    public readonly StoreDbContext _context;

    public GenericRepository(StoreDbContext context)
    {
        _context = context;
    }


    public async Task<TEntity> GetIdAsync(TKey id)
        => await _context.Set<TEntity>().FindAsync(id);

    public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        => await _context.Set<TEntity>().ToListAsync();

    public async Task CreateAsync(TEntity entity)
        => await _context.Set<TEntity>().AddAsync(entity);

    public void UpdateAsync(TEntity entity)
        => _context.Set<TEntity>().Update(entity);

    public void DeleteAsync(TEntity entity)
        => _context.Set<TEntity>().Remove(entity);
}