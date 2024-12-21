using System;
using EfCore.Business.Abstract;
using EfCore.Shared.Dtos;

namespace EfCore.Business.Contrete;

public class ProductService : IProductService
{
    public Task<ProductDTO> CreateAsync(ProductCreateDTO productCreateDTO)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ProductDTO> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductDTO>> GetProductsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductDTO>> GetProductsAsync(bool isDeleted)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductDTO>> GetProductsByCategoryIdAsync(int categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<ProductDTO> UpdateAsync(ProductUpdateDTO productUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
