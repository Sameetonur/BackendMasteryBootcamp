using System;
using EShop.Shared.Dtos;
using EShop.Shared.Dtos.ResponseDtos;

namespace EShop.Services.Abstract;

public interface ICategoryService
{
    Task<ResponseDto<CategoryDto>>GetAsync(int id);

}
