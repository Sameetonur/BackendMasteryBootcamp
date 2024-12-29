using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FakeStoreApıMVC.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using System.Text;

namespace FakeStoreApıMVC.Controllers;

public class HomeController : Controller
{
   
    private readonly HttpClient _httpClient;

    public HomeController(IHttpClientFactory   httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("FakeStoreApi");
    }

    public async  Task<IActionResult> Index()
    {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync("products");
            var contentResponse = await responseMessage.Content.ReadAsStringAsync();
            List<Product>? response = JsonConvert.DeserializeObject<List<Product>>(contentResponse);
        return View(response);
    }

    public async Task<IActionResult> Details(int id)
    {
        var response = await _httpClient.GetAsync($"products/{id}");
        var contentresponse = await response.Content.ReadAsStringAsync();
        var responseresult = JsonConvert.DeserializeObject<Product>(contentresponse);
        return View(responseresult);
    }

    public async Task<IActionResult> GetCategories()
    {
        var response = await _httpClient.GetAsync($"products/categories");
        var contentresponse = await response.Content.ReadAsStringAsync();
        var responseresult = JsonConvert.DeserializeObject<List<string>>(contentresponse);
        return View(responseresult);
    }

     public async Task<IActionResult> AddProduct()
     {
        var response = await _httpClient.GetAsync($"products/categories");
        var contentresponse = await response.Content.ReadAsStringAsync();
        var categories = JsonConvert.DeserializeObject<List<string>>(contentresponse);
        ViewBag.Categories = categories;

        return View();
     }

     [HttpPost]
     public async Task<IActionResult> AddProduct(Product product)
     {
        if(ModelState.IsValid)
        {
            // var resonse = await _httpClient.PostAsJsonAsync("products",product);
            var serializeProduct = JsonConvert.SerializeObject(product);
            HttpContent content =new StringContent(serializeProduct, Encoding.UTF8,"application/json");
            var response = await _httpClient.PostAsync("Products", content);
            var newProduct = response.Content.ReadAsStringAsync();
             var result = JsonConvert.DeserializeObject<Product>(newProduct.Result);
            return Json(result);
        }
        var responseMessage = await _httpClient.GetAsync($"products/categories");
        var contentresponse = await responseMessage.Content.ReadAsStringAsync();
        var categories = JsonConvert.DeserializeObject<List<string>>(contentresponse);
        ViewBag.Categories = categories;
        return View(product);
    }
    

   

    
}
