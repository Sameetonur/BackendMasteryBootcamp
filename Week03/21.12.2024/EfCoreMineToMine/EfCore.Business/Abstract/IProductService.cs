using System;
using EfCore.Shared.Dtos;

namespace EfCore.Business.Abstract;

public interface IProductService
{
    Task<ProductDTO> CreateAsync(ProductCreateDTO productCreateDTO);

    Task<ProductDTO> UpdateAsync(ProductUpdateDTO productUpdateDTO);

    Task DeleteAsync(int id);

    Task<IEnumerable<ProductDTO>> GetProductsAsync();
    Task<IEnumerable<ProductDTO>> GetProductsAsync(bool isDeleted);

    Task<IEnumerable<ProductDTO>> GetProductsByCategoryIdAsync(int categoryId);

    Task<ProductDTO> GetByIdAsync(int id);

}
