using System;
using EfCore.Entity.Concrete;

namespace EfCore.Data.Abstract;

public interface ICategoryRepository : IGenericRepository<Category>
{
    // Task<IEnumerable<Category>> GetAllAsync();
    // Task<Category> GetByIdAsync(int id);
    // Task AddAsync(Category entity);
    // Task UpdateAsync(Category entity);
    // Task DeleteAsync(Category entity);
    //IGenericRepository'den Category için miras alarak, bu interface içinde yukarıdaki metotların imzalarının olmasını sağladık.(TEntity yerine Category yazdık)
    Task<IEnumerable<Category>> GetAllDeletedCategoriesAsync(bool isDeleted=true);
    

    
}
