using System;

namespace DailyApi.Data.Abstract;

public interface IGenericRepository<T> 
{
    Task<IEnumerable<T>> GetAllAsync();

    Task AddAsync(T entity);

}
