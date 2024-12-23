using System;
using System.Runtime.CompilerServices;
using EfCore.Data.Abstract;
using EfCore.Data.Concrate.Context;
using EfCore.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace EfCore.Data.Concrate.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync(bool isDeleted = true)
    {
         var products = await _appDbContext.Products.Where(x=>x.IsDeleted==isDeleted).ToListAsync();
         return products;
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        var products = await _appDbContext.Products.Include(x=>x.ProductCategories).ThenInclude(y=>y.Category)
                            .Where(x=>x.ProductCategories.Any(y=>y.CategoryId==categoryId)).ToListAsync();

        return products;
    }
}
