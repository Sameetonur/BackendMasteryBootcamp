using System;
using System.Net.Http;
using EShop.MVC.Areas.Admin.Models;
using EShop.MVC.Models;
using EShop.MVC.Services.Interfaces;
using Newtonsoft.Json;

namespace EShop.MVC.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IHttpClientService _HttpClientService;

        public CategoryService(IHttpClientService httpClientService)
        {
            _HttpClientService = httpClientService;
        }

        public Task<ResponseModel<int>> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<int>> CountAsync(bool isActive)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<CategoryModel>> CreateAsync(CategoryCreateModel categoryCreateModel)
        {
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(categoryCreateModel.Name),"Name");
           if(!string.IsNullOrEmpty(categoryCreateModel.Description))
           {
                formData.Add(new StringContent(categoryCreateModel.Description), "Description");
            }
            if (categoryCreateModel.Image != null)
            {
                var imageContent = new StreamContent(categoryCreateModel.Image.OpenReadStream());
                formData.Add(imageContent,"Image",categoryCreateModel.Image.FileName);

            }
            else
            {
                //varsayılan görseli gönder
                var defaultImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","images" ,"no-image.png");
                var imageContent = new StreamContent(File.OpenRead(defaultImagePath));
                formData.Add(imageContent,"Image", "no-image.png");
            }
            var response = await _HttpClientService.PostFormAsync<ResponseModel<CategoryModel>>("categories",formData);
            return response!;
        }

        public async Task<ResponseModel<List<CategoryModel>>> GetAllActivesAsync()
        {
            var response = await _HttpClientService.GetAsync<ResponseModel<List<CategoryModel>>>("categories/actives");

            return response!;
        }

        public async Task<ResponseModel<List<CategoryModel>>> GetAllAsync()
        {
            var response = await _HttpClientService.GetAsync<ResponseModel<List<CategoryModel>>>("categories");

            return response!;
        }

        public async Task<ResponseModel<List<CategoryModel>>> GetAllPassivesAsync()
        {
            var response = await _HttpClientService.GetAsync<ResponseModel<List<CategoryModel>>>("categories/passives");

            return response!;
        }

        public async Task<ResponseModel<CategoryModel>> GetAsync(int id)
        {
            var response = await _HttpClientService.GetAsync<ResponseModel<CategoryModel>>($"categories/{id}");
            return response!;
        }

        public async Task<ResponseModel<NoContent>> HardDeleteAsync(int id)
        {
            var response = await _HttpClientService.DeleteAsync<ResponseModel<NoContent>>($"categories/harddelete/{id}");
            return response!;
        }

        public async Task<ResponseModel<NoContent>> SoftDeleteAsync(int id)
        {
           var response = await _HttpClientService.DeleteAsync<ResponseModel<NoContent>>($"categories/softdelete/{id}");
           return response!;
        }

        public async Task<ResponseModel<NoContent>> UpdateAsync(CategoryUpdateModel categoryUpdateModel)
        {
            var formData= new MultipartFormDataContent();
            formData.Add(new StringContent(categoryUpdateModel.Id.ToString()));
            formData.Add(new StringContent(categoryUpdateModel.Name),"Name");
            if (!string.IsNullOrEmpty(categoryUpdateModel.Description))
            {
                formData.Add(new StringContent(categoryUpdateModel.Description), "Description");
            }
            formData.Add(new StringContent(categoryUpdateModel.IsActive.ToString()), "IsActive");
            formData.Add(new StringContent(categoryUpdateModel.IsDeleted.ToString()), "IsDeleted");
            if (categoryUpdateModel.Image != null)
            {
                var imageContent = new StreamContent(categoryUpdateModel.Image.OpenReadStream());
                formData.Add(imageContent, "Image", categoryUpdateModel.Image.FileName);
            }
             var response = await _HttpClientService.PutFormAsync<ResponseModel<NoContent>>("categories",formData);
             return response!;
        }

        public async Task<ResponseModel<bool>> UpdateIsActive(int id)
        {
            var  response = await _HttpClientService.PutAsync<object, ResponseModel<bool>>($"categories/update,sactive/{id}",null!);
            return response!;
        }
    }
}
