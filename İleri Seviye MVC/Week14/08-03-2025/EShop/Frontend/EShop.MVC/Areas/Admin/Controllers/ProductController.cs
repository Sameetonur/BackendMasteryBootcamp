using EShop.MVC.Areas.Admin.Models;
using EShop.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;

namespace EShop.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IToastNotification _toastNotification;

        public ProductController(IProductService productService, ICategoryService categoryService, IToastNotification toastNotification)
        {
            _productService = productService;
            _categoryService = categoryService;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _productService.GetAllAsync();
            return View(response.Data);//Bilerek hata kontrolü yapmadık, aslında doğru olan yapmak.
        }

        [HttpPost]
        public async Task<IActionResult> UpdateIsActive(int id)
        {
            var response = await _productService.UpdateIsActiveAsync(id);
            Console.WriteLine(response.Data);
            Console.WriteLine(response.Error);
            Console.WriteLine(response.IsSuccessful);
            return Json(new { isSuccessful = response.IsSuccessful, error = response.Error });
        }

        [HttpDelete]
        public async Task<IActionResult> HardDelete(int id)
        {
            var response = await _productService.HardDeleteAsync(id);
            return Json(new { isSuccessful = response.IsSuccessful, error = response.Error });
        }

        [HttpDelete]
        public async Task<IActionResult> SoftDelete(int id)
        {
            Console.WriteLine("Silinecek Ürün Id: " + id);
            var response = await _productService.SoftDeleteAsync(id);
            return Json(new { isSuccessful = response.IsSuccessful, error = response.Error });
        }

        [NonAction]
        private async Task<List<SelectListItem>> GenetareCategoryList(List<int>? categoryIds =null!)  //Bu metodu kullanarak kategori listesini oluşturabiliriz.
        {
            var categories = (await _categoryService.GetAllActivesAsync()).Data;
            List<SelectListItem> categoryList = [];
            SelectListItem selectListItem;
            foreach (var category in categories)
            {
                selectListItem = new SelectListItem();
                if (categoryIds == null )
                {
                    selectListItem.Selected=false;
                }else
                {
                    foreach(var categoryId in categoryIds)
                    {
                        if (categoryId == category.Id)
                        {
                            selectListItem.Selected=true;
                        }
                    }
                }
                selectListItem.Text = category.Name;
                selectListItem.Value=category.Id.ToString();
                categoryList.Add(selectListItem);
                
            }

            return categoryList;

        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
           
            ViewBag.Categories = await GenetareCategoryList();
           
            return View();
        }

      

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateModel productCreateModel)
        {
           
            if (!ModelState.IsValid || productCreateModel.CategoryIds.Count ==0)
            {
                if (productCreateModel.CategoryIds.Count == 0)
                {
                    ModelState.AddModelError("CategoryIds", "En az bir kategori seilmelidir!");
                }

                ViewBag.Categories= await GenetareCategoryList((List<int>)productCreateModel.CategoryIds);
                return View(productCreateModel);
                
            }
            return View();
        }

    }
}
