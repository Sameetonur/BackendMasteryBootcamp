using EfCore.Business.Abstract;
using EfCore.Business.Contrete;
using EfCore.Data.Concrate.Repositories;
using EfCore.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EfCore.UI.Controllers
{
    
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        // GET: CategoryController
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<ActionResult> Index()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            if (categories==null){
                categories = new List<CategoryDTO>();
            }
            
                
            
            return View(categories);
        }

        public ActionResult Create()
        {
            return  View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CategoryCreateDTO categoryCreateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.CreateAsync(categoryCreateDto);
                if (result == null)
                {
                    ViewBag.Message = "Bir hata oluştu";
                }
                else
                {
                    ViewBag.Message = "İşlem başarıyla tamamlandı";
                }
                return RedirectToAction("Index");
            }
            return View(categoryCreateDto);
        }
    }
}
