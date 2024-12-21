using System;
using EfCore.Business.Abstract;
using EfCore.Data.Abstract;
using EfCore.Entity.Concrete;
using EfCore.Shared.Dtos;

namespace EfCore.Business.Contrete;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDTO> CreateAsync(CategoryCreateDTO categoryCreateDTO)
    {
        if(categoryCreateDTO == null)
        {
            
           return null; 
        }
            var category = new Category
            {
                Name = categoryCreateDTO.Name,
                Description = categoryCreateDTO.Description,
                IsDeleted=false
            };

            await _categoryRepository.AddAsync(category);

             var categoryDTO = new CategoryDTO
             {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
             };

             return categoryDTO;

            
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryDTO> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CategoryDTO>> GetCategoriesAsync(bool isDeleted)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryDTO> UpdateAsync(CategoryUpdateDTO categoryUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
