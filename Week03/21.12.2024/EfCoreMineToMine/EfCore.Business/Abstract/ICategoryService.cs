using System;
using EfCore.Shared.Dtos;


namespace EfCore.Business.Abstract;

public interface ICategoryService 
{
    Task<CategoryDTO> CreateAsync(CategoryCreateDTO categoryCreateDTO);

    Task<CategoryDTO> UpdateAsync(CategoryUpdateDTO categoryUpdateDTO);

    Task DeleteAsync(int id);

    Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
    Task<IEnumerable<CategoryDTO>> GetCategoriesAsync(bool isDeleted);

    Task<CategoryDTO> GetByIdAsync(int id);




}
