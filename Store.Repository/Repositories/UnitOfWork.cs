using System.Collections;
using Store.Data.Context;
using Store.Data.Entities;
using Store.Repository.Interfaces;

namespace Store.Repository.Repositories;

public class UnitOfWork
    : IUnitOfWork
{
    private readonly StoreDbContext _context;
    private Hashtable _repositories;

    public UnitOfWork(StoreDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<TEntity, TKey>
        Repository<TEntity, TKey>() where TEntity
        : BaseEntity<TKey>
    {
        if (_repositories is null) _repositories = new Hashtable();

        var key = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(key))
        {
            var repositoryType = typeof(IGenericRepository<TEntity, TKey>);
            var repositoryInstance =
                Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
            _repositories.Add(key, repositoryInstance);
        }

        return (IGenericRepository<TEntity, TKey>)_repositories[key];
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }


    public void Dispose()
    {
        _context.Dispose();
    }
}