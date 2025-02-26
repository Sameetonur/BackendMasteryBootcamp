using System;
using EShop.MVC.Areas.Admin.Models;
using EShop.MVC.Models;
using EShop.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace EShop.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IToastNotification _toastr;

        public CategoryController(ICategoryService categoryService, IToastNotification toastr)
        {
            _categoryService = categoryService;
            _toastr = toastr;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _categoryService.GetAllAsync();
            if (!response.IsSuccessful && response.Data == null)
            {
                
                return View(new List<CategoryModel>());
            }
            return View(response.Data);
        }

        public async Task<IActionResult> UpdateIsActive(int id)
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateModel categoryCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryCreateModel);
                
            }
            var response = await _categoryService.CreateAsync(categoryCreateModel);
            if (!response.IsSuccessful)
            {
                _toastr.AddWarningToastMessage(response.Error ?? "Server'dan kaynaklı bir sorun oluştu!");
                return View(categoryCreateModel);
            }
            _toastr.AddSuccessToastMessage("Kategori başarıyla oluşturuldu");
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _categoryService.GetAsync(id);
            if(!response.IsSuccessful)
            {
                _toastr.AddErrorToastMessage(response.Error ?? "Kategori Bulunamadı!");
                return RedirectToAction(nameof(Index));
            }
            var categoryUpdateModel = new CategoryUpdateModel
            {
                Id =response.Data!.Id,
                Name = response.Data.Name,
                Description = response.Data.Description,
                IsActive = response.Data.IsActive,  
                IsDeleted = response.Data.IsDeleted
            };
             ViewBag.CurrentImageUrl = response.Data.ImageUrl;
             return View(categoryUpdateModel);
        }
    }
}
