using System;
using System.Linq.Expressions;
using EShop.Data.Abstract;
using EShop.Data.Concrete.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace EShop.Data.Concrete.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly EShopDbContext _dbcontext;
    private readonly DbSet<TEntity> _dbset;

    public GenericRepository(EShopDbContext dbcontext)
    {
        _dbcontext = dbcontext;
        _dbset= _dbcontext.Set<TEntity>();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbset.AddAsync(entity);
        return entity;
    }

    public async Task<int> CountAsync()
    {
        return await _dbset.CountAsync();
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbset.CountAsync(predicate);
    }

    public void Delete(TEntity entity)
    {
        _dbset.Remove(entity);
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbset.AnyAsync(predicate);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbset.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
    {
        IQueryable<TEntity> query = _dbset;
        if (predicate != null)
        {
            query=query.Where(predicate);
            
        }if (orderBy != null)
        {
            query=orderBy(query);
            
        }if (includes != null)
        {
            query = includes.Aggregate(query,(current,include) =>include (current));
        }
        var result = await query.ToListAsync();
        return result;
    }

    public async Task<TEntity> GetAsync(int id)
    {
        return await  _dbset.FindAsync(id);
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
    {
        IQueryable<TEntity> query = _dbset;
        if(predicate != null)
        {
            query = query.Where(predicate);
        }if(includes!=null)
        {
            query = includes.Aggregate(query,(current, includes)=> includes(current));
        }
            var result = await query.FirstOrDefaultAsync();
        return result;

        //GetAsync Metodun yaptığı işi daha anlamlı anlatan kod.
        /*
        _dbset = context.Product();
        query = context.Product();
        predicate = p=>p.IsDeleted==true
        query = contextçProducts.Where(p=>p.IsDeleted==false)
        includes[]=[Include(x=>x.Category),Include(x=>x.Brand)]

        query =context
        .Products
        .Where(p=>p.Isdeleted==false)
        .Include(x=>x.Category)
        .Include(x=>x.Brand)
        */
    }

    public void Update(TEntity entity)
    {
        _dbset.Update(entity);
        _dbset.Entry(entity).State=EntityState.Modified;
    }
}
