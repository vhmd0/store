using Store.Data.Context;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using System.Collections;

namespace Store.Repository.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly StoreDbContext _context;
    private Hashtable _repositories;

    public UnitOfWork(StoreDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>()
        where TEntity : BaseEntity<TKey>
    {
        _repositories ??= new Hashtable();

        var key = typeof(TEntity).FullName!;

        if (!_repositories.ContainsKey(key))
        {
            var repositoryType = typeof(GenericRepository<,>);
            var repositoryInstance = Activator.CreateInstance(
                repositoryType.MakeGenericType(typeof(TEntity), typeof(TKey)),
                _context
            );

            _repositories.Add(key, repositoryInstance!);
        }

        return (IGenericRepository<TEntity, TKey>)_repositories[key]!;
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}