using System;
using EShop.MVC.Areas.Admin.Models;
using EShop.MVC.Models;
using EShop.MVC.Services.Interfaces;
using Newtonsoft.Json;
using NToastNotify;

namespace EShop.MVC.Services;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _client;
    

    public CategoryService(IHttpClientFactory  httpClientFactory)
    {
        _client = httpClientFactory.CreateClient("API");
    }

    public Task<ResponseModel<int>> CountAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<int>> CountAsync(bool isActive)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<CategoryModel>> CreateAsync(CategoryCreateModel categoryCreateModel)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<CategoryModel>> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseModel<List<CategoryModel>>> GettAllActivesAsync()
    {
       var httpResponseMessage = await _client.GetAsync("categories");
       var contentResponse = await httpResponseMessage.Content.ReadAsStringAsync();
       var response = JsonConvert.DeserializeObject<ResponseModel<List<CategoryModel>>>(contentResponse);
       return response;
    }

    public Task<ResponseModel<List<CategoryModel>>> GettAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<List<CategoryModel>>> GettAllPassivesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<NoContent>> HardDeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<NoContent>> SoftDeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<NoContent>> UpdateAsync(CategoryUpdateModel categoryUpdateModel)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<bool>> UpdateIsActiveAsync(int id)
    {
        throw new NotImplementedException();
    }
}
