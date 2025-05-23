using System;
using System.Linq.Expressions;
using EShop.Data.Abstract;
using EShop.Data.Concrete.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EShop.Data.Concrete.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly EShopDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(EShopDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public void BatchUpdate(IEnumerable<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
    }

    public async Task<int> CountAsync()
    {
        return await _dbSet.CountAsync();
    }

    public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.CountAsync(predicate);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null!, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!,
    bool showIsDeleted = false, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;
        if (showIsDeleted)
        {
            query = query.IgnoreQueryFilters();
        }
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        if (orderBy != null)
        {
            query = orderBy(query);
        }
        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => include(current));
        }
        return await query.ToListAsync();
    }

    public async Task<TEntity> GetAsync(int id)
    {
        var result = await _dbSet.FindAsync(id);
        return result!;
    }

    public async Task<TEntity> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes
        )
    {
        IQueryable<TEntity> query = _dbSet;
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => include(current));
        }

        var result = await query.FirstOrDefaultAsync();
        return result!;

    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
        //UPDATE 
    }


}

