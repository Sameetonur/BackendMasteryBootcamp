using System;
using System.Security.Cryptography.X509Certificates;
using EfCore.Data.Abstract;
using EfCore.Data.Concrate.Context;
using EfCore.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace EfCore.Data.Concrate.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<IEnumerable<Category>> GetAllDeletedCategoriesAsync(bool isDeleted = true)
    {
        var categories = await _appDbContext.Categories.Where(x => x.IsDeleted == isDeleted).ToListAsync();

        return categories;
    }
}
