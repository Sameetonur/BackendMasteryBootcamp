using System;
using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
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

    public async Task DeleteAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if(category != null)
        {
           await _categoryRepository.DeleteAsync(category);
        }
    }

    public async Task<CategoryDTO> GetByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            return null;
        }
    
        var categoryDTO = new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
        return categoryDTO;
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
    {
       IEnumerable<Category> categories =  await _categoryRepository.GetAllAsync();
       var categoryDTOs = categories.Select(x => new CategoryDTO
       {
           Id = x.Id,
           Name = x.Name,
           Description = x.Description
       }).ToList();

       return categoryDTOs;

       
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync(bool isDeleted)
    {
        IEnumerable<Category> categories = await _categoryRepository.GetAllDeletedCategoriesAsync(isDeleted);
        var categoryDTOs = categories.Select(x => new CategoryDTO
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description
        }).ToList();

        return categoryDTOs;
    }

    public async Task<CategoryDTO> UpdateAsync(CategoryUpdateDTO categoryUpdateDTO)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryUpdateDTO.Id);
        if(category==null)
        {
            return null;
        }
        category.Name= categoryUpdateDTO.Name;
        category.Description=categoryUpdateDTO.Description;
        await _categoryRepository.UpdateAsync(category);
        var categoryDTO = new CategoryDTO{
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
        return categoryDTO;
    }
    
}




// foreach(Category category in categories)
//        {
//           categoryDTOs.Add( new CategoryDTO
//           {
//             Id = category.Id,
//             Name = category.Name,
//             Description=category.Description
//           });
//        }
//        return categoryDTOs;