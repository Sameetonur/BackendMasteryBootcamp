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



    public async Task<IEnumerable<Product>> GetAllDeleteProductsAsync(bool isDeleted = true)
    {
         var products = await _appDbContext.Products.Where(x=>x.IsDeleted==isDeleted).ToListAsync();
         return products;
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        var products = await _appDbContext.Products.Include(x=>x.ProductCategories).ThenInclude (y=>y.Category)
                            .Where(x=>x.ProductCategories.Any(y=>y.CategoryId==categoryId)).ToListAsync();

        return products;
    }

    public async Task<Product> GetProductWithCategoriesAsync(int id)
    {
        var product = await _appDbContext
                        .Products
                        .Where(x => x.Id == id)
                        .Include(x => x.ProductCategories)
                        .ThenInclude(y => y.Category)
                        .FirstOrDefaultAsync(); //FirstOrDefaultAsync() => tek bir kayıt döner. 
        return product;
    }

    public async Task<IEnumerable<Product>> GetProductsWithCategoriesAsync()
    {
        var products = await _appDbContext.Products
        .Include(x => x.ProductCategories)
        .ThenInclude(y => y.Category)
        .ToListAsync();

        return products;

    }
}

