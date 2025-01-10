using System;
using DailyApi.Data.Abstract;
using DailyApi.Data.Concrete.Context;
using Microsoft.EntityFrameworkCore;

namespace DailyApi.Data.Concrete.Repositories;


public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _appdbcontext;
    
 private readonly DbSet<T> _dbset;
    public GenericRepository(AppDbContext appdbcontext)
    {
        _appdbcontext = appdbcontext;
        _dbset = _appdbcontext.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await _dbset.AddAsync(entity);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var Entity = await _dbset.ToListAsync();
        return Entity;
    }
}
