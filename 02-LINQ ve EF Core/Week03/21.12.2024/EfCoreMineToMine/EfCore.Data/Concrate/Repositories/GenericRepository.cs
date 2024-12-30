using System;
using EfCore.Data.Abstract;
using EfCore.Data.Concrate.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EfCore.Data.Concrate.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{

    protected readonly AppDbContext _appDbContext; // prodectede dışarıya açık ama sadece miras alanlar kullanabilir.
    private readonly DbSet<TEntity> _dbset;

    public GenericRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        _dbset= _appDbContext.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbset.AddAsync(entity);

        await _appDbContext.SaveChangesAsync();

        //buraya TEntity Category olarak gönderilmişse üst satırdaki komut şöyle çalışıcak : appDbContext.Categories.AddAsync(entity);
        //buraya TEntity Product olarak gönderilmişse üst satırdaki komut şöyle çalışacak: appDbContext.Product.AddAsync(entity);


    }

    public async Task DeleteAsync(TEntity entity)
    {
         _dbset.Remove(entity);
       await  _appDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
         var entities =  await _dbset.ToListAsync();
         return entities; // int olarak 1 döner başırılı olursa başarısız olusa  0 döner bunu sorgulatabiliriz.
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
         var entities = await _dbset.FindAsync(id);
         return  entities;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbset.Update(entity);
        await _appDbContext.SaveChangesAsync();
    }
}
