using System;
using EShop.Data.Abstract;
using EShop.Data.Concrete.Context;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Data.Concrete;

public class UnitOfWork : IUnitOfWork
{
    private readonly EShopDbContext _dbcontext;
    private readonly IServiceProvider _serviceProvider;
    public void Dispose()
    {
        _dbcontext.Dispose();
    }

    public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        var repository = _serviceProvider.GetRequiredService<IGenericRepository<TEntity>>();
        return repository;
    }

    public int Save()
    {
        return _dbcontext.SaveChanges();
    }

    public async Task<int> SaveAsync()
    {
        return await _dbcontext.SaveChangesAsync();
    }
}
