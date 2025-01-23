using System;
using AutoMapper;
using Azure;
using EShop.Data.Abstract;
using EShop.Entity.Concrete;
using EShop.Services.Abstract;
using EShop.Shared.Dtos;
using EShop.Shared.Dtos.ResponseDtos;
using Microsoft.AspNetCore.Http;

namespace EShop.Services.Concrete;

public class CategoryManager : ICategoryService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Category> _categoryRepository;

    public CategoryManager(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
        _categoryRepository = _uow.GetRepository<Category>();
    }

    public async Task<ResponseDto<CategoryDto>> AddAsync(CategoryCreateDto categoryCreateDto)
    {
        //mapper ekledik AutoMapper 
        try
        {
            var İsExists = await _categoryRepository.ExistsAsync(x => x.Name.ToLower()==categoryCreateDto.Name.ToLower());
            if (İsExists)
            {
                return ResponseDto<CategoryDto>.Fail("Bu adda kategory mevcut", StatusCodes.Status400BadRequest);
            }
            var category = _mapper.Map<Category>(categoryCreateDto);
            await _categoryRepository.AddAsync(category);

            var result = await _uow.SaveAsync();
            if (result < 1)
            {
                return ResponseDto<CategoryDto>.Fail("Kategori eklenirken bir hata oluştu", StatusCodes.Status500InternalServerError);
            }
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return ResponseDto<CategoryDto>.Success(categoryDto, StatusCodes.Status201Created);
        }
        catch (System.Exception ex)
        {
            return ResponseDto<CategoryDto>.Fail(ex.Message, StatusCodes.Status500InternalServerError);

        }
    }

    public async Task<ResponseDto<int>> CountAsync()
    {
        try
        {
            var count = await _categoryRepository.CountAsync();
            return ResponseDto<int>.Success(count, StatusCodes.Status200OK);
        }
        catch (System.Exception ex)
        {

            return ResponseDto<int>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
        }

    }

    public async Task<ResponseDto<int>> CountAsync(bool? isActive)
    {
        try
        {
            var count = await _categoryRepository.CountAsync(x => x.IsActive == isActive);
            return ResponseDto<int>.Success(count, StatusCodes.Status200OK);
        }
        catch (System.Exception ex)
        {
            return ResponseDto<int>.Fail(ex.Message, StatusCodes.Status500InternalServerError);

        }
    }

    public async Task<ResponseDto<IEnumerable<CategoryDto>>> GetAllAsync()
    {
        try
        {
            var categories = await _categoryRepository.GetAllAsync();
            if (!categories.Any())
            {
                return ResponseDto<IEnumerable<CategoryDto>>.Fail("Kategori bulunamadı!", StatusCodes.Status404NotFound);
            }
            var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return ResponseDto<IEnumerable<CategoryDto>>.Success(categoryDtos, StatusCodes.Status200OK);
        }
        catch (System.Exception ex)
        {

            return ResponseDto<IEnumerable<CategoryDto>>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ResponseDto<IEnumerable<CategoryDto>>> GetAllAsync(bool? isActive)
    {

        try
        {
            var categories = await _categoryRepository.GetAllAsync(x => x.IsActive == isActive);
            if (!categories.Any())
            {
                var messageTitle = isActive == true ? "Aktif" : "Pasif";
                return ResponseDto<IEnumerable<CategoryDto>>.Fail($"{messageTitle} kategori bulunamadı!", StatusCodes.Status404NotFound);
            }
            var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return ResponseDto<IEnumerable<CategoryDto>>.Success(categoryDtos, StatusCodes.Status200OK);
        }
        catch (System.Exception ex)
        {

            return ResponseDto<IEnumerable<CategoryDto>>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ResponseDto<CategoryDto>> GetAsync(int id)
    {
        try
        {
            var category = await _categoryRepository.GetAsync(x => x.Id == id);
            if (category == null)
            {

                return ResponseDto<CategoryDto>.Fail(" kategori bulunamadı!", StatusCodes.Status404NotFound);
            }
            var categoryDto = _mapper.Map<CategoryDto>(id);
            return ResponseDto<CategoryDto>.Success(categoryDto, StatusCodes.Status200OK);
        }
        catch (System.Exception ex)
        {

            return ResponseDto<CategoryDto>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
        }

    }

    public async Task<ResponseDto<NoContent>> HardDeleteAsync(int id)
    {
        try
        {
            var category = await _categoryRepository.GetAsync(x => x.Id == id);
            if (category == null)
            {
                return ResponseDto<NoContent>.Fail("Kategori bulunamadığı için silinme işlemi gerekleşemedi!", StatusCodes.Status404NotFound);
            }
            var hasproducts = await _uow.GetRepository<ProductCategory>().ExistsAsync(x => x.CategoryId == id);
            if (hasproducts)
            {
                return ResponseDto<NoContent>.Fail("Bu kategoriye ait ürünler olduğu için silinemez önce ürünleri silmeniz gerekir ya da kategorisini değiştiriniz.!", StatusCodes.Status404NotFound);
            }
            _categoryRepository.Delete(category);
            var result = await _uow.SaveAsync();
            if (result < 1)
            {
                return ResponseDto<NoContent>.Fail("Kategori silinirken bir hata oluştu", StatusCodes.Status500InternalServerError);
            }
            return ResponseDto<NoContent>.Success(StatusCodes.Status200OK);
        }
        catch (System.Exception ex)
        {

            return ResponseDto<NoContent>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ResponseDto<NoContent>> SoftDeleteAsync(int id)
    {
        try
        {
            var category = await _categoryRepository.GetAsync(x => x.Id == id);
            if (category == null)
            {
                return ResponseDto<NoContent>.Fail("Kategori bulunamadığı için silme ya da geri alma işlemi yapılamadı.!", StatusCodes.Status400BadRequest);
            }
            var hasproducts = await _uow.GetRepository<ProductCategory>().ExistsAsync(x => x.CategoryId == id);
            if (hasproducts)
            {
                return ResponseDto<NoContent>.Fail("Bu kategoriye ait ürünler olduğu için silme işlemi gerekleşmedi!", StatusCodes.Status400BadRequest);
            }
            category.IsDeleted = !category.IsDeleted;
            if(category.IsDeleted == true) category.IsActive=false;
            _categoryRepository.Update(category);

            var result = await _uow.SaveAsync();
            if (result < 1)
            {
                return ResponseDto<NoContent>.Fail("Kategori silinmeye ya da geri alınmaya alışırken bir hata oluştu", StatusCodes.Status500InternalServerError);
            }
            return ResponseDto<NoContent>.Success(StatusCodes.Status200OK);
        }
        catch (System.Exception ex)
        {

            return ResponseDto<NoContent>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ResponseDto<NoContent>> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
    {
        try
        {
            var category = await _categoryRepository.GetAsync(x => x.Id == categoryUpdateDto.Id);
            if (category == null)
            {
                return ResponseDto<NoContent>.Fail("Kategori bulunamadığı için güncellenemedi!", StatusCodes.Status404NotFound);
            }
            if (!category.IsActive)
            {
                return ResponseDto<NoContent>.Fail("Pasif kategoriler güncellenemez! önce güncellemek istediğiniz kategoriyi aktif hale getirmeniz gerekir.!", StatusCodes.Status404NotFound);
            }
            var existsingCategoryName = await _categoryRepository.ExistsAsync(x => x.Name.Equals(category.Name, StringComparison.CurrentCultureIgnoreCase));
            if (existsingCategoryName)
            {
                return ResponseDto<NoContent>.Fail("Bu adda kategory mevcut!", StatusCodes.Status400BadRequest);
            }
            _categoryRepository.Update(category);
            var result = await _uow.SaveAsync();
            if (result < 1)
            {
                return ResponseDto<NoContent>.Fail("Kategori silinirken bir hata oluştu", StatusCodes.Status500InternalServerError);
            }
            return ResponseDto<NoContent>.Success(StatusCodes.Status204NoContent);
        }
        catch (System.Exception ex)
        {

            return ResponseDto<NoContent>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<ResponseDto<bool>> UpdateIsActiveAsync(int id)
    {
        try
        {
            var category = await _categoryRepository.GetAsync(x => x.Id == id);
            if (category == null)
            {
                return ResponseDto<bool>.Fail("Kategori bulunamadığı için aktiflik durumu güncellenemedi.!", StatusCodes.Status400BadRequest);
            }
            var hasproducts = await _uow.GetRepository<ProductCategory>().ExistsAsync(x => x.CategoryId == id);
            if (hasproducts)
            {
                return ResponseDto<bool>.Fail("Bu kategoriye ait ürünler olduğu için pasif hale getirilemez!", StatusCodes.Status400BadRequest);
            }
            category.IsActive = !category.IsActive;
            _categoryRepository.Update(category);

            var result = await _uow.SaveAsync();
            if (result < 1)
            {
                return ResponseDto<bool>.Fail("Kategori silinirken bir hata oluştu", StatusCodes.Status500InternalServerError);
            }
            return ResponseDto<bool>.Success(category.IsActive, StatusCodes.Status200OK);
        }
        catch (System.Exception ex)
        {

            return ResponseDto<bool>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
        }

    }
}
