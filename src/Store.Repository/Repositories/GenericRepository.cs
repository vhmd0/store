using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Repository.Specification;
using Store.Repository.Specification.Products;

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
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<TEntity?> GetDataWithSpecificationAsync(ISpecification<TEntity> spec)
        => await SpecificationEvaluator<TEntity, TKey>.GetQuery(_dbSet, spec)
            .FirstOrDefaultAsync();

    public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        => await _dbSet.ToListAsync();

    public async Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> spec)
        => await query(spec).ToListAsync();
    public async Task<TEntity> GetByIdSpecificationAsync(ISpecification<TEntity> spec)
        => await query(spec).FirstOrDefaultAsync();

    public async Task<int> CountAsync(ISpecification<TEntity> spec)
        => await query(spec).CountAsync();

    public async Task CreateAsync(TEntity entity)
        => await _dbSet.AddAsync(entity);


    public void Update(TEntity entity)
        => _dbSet.Update(entity);

    public void Delete(TEntity entity)
        => _dbSet.Remove(entity);

    private IQueryable<TEntity> query(ISpecification<TEntity> spec)
        => SpecificationEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), spec);
}