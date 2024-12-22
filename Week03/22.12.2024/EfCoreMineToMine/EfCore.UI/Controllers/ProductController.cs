using System.Reflection.Metadata.Ecma335;
using EfCore.Business.Abstract;
using EfCore.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EfCore.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // GET: ProductController
        public  async Task<ActionResult> Index()
        {
            var products = await _productService.GetProductsAsync();
         
            return View(products??new List<ProductDTO>());
        }
        [NonAction]
        private async Task<List<CategoryDTO>> GetCategoryListAsync()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            var categoryList = categories.Select(x=>new CategoryDTO
            {
               Id=x.Id,
               Name=x.Name

            }).ToList();
            return categoryList;
        }

        public async Task<ActionResult> Create()
        {
            ProductCreateDTO productCreateDto = new(){
                    CategoryList= await GetCategoryListAsync()
            };
            return View();
        }

        public async Task<ActionResult> Create(ProductCreateDTO productCreateDto)
        {
           return View();
        }
        
        


    }
}
