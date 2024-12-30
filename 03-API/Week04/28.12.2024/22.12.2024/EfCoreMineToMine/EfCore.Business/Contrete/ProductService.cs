using System;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using EfCore.Business.Abstract;
using EfCore.Data.Abstract;
using EfCore.Entity.Concrete;
using EfCore.Shared.Dtos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCore.Business.Contrete;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDTO> CreateAsync(ProductCreateDTO productCreateDTO)
    {
        var product = new Product
        {
            Name = productCreateDTO.Name,
            Price = productCreateDTO.Price,
            Properties = productCreateDTO.Properties,

        };
        await _productRepository.AddAsync(product);

        product.ProductCategories = productCreateDTO.CategoryIds.Select(x => new ProductCategory
        {
            CategoryId = x,
            ProductId = product.Id
        }).ToList();

        await _productRepository.UpdateAsync(product);

        var productDto = new ProductDTO{
            Id= product.Id,
            Name = product.Name,
            Price = product.Price,
            Properties = product.Properties,
        };
        return productDto;
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        await _productRepository.DeleteAsync(product);  
    }

    public async Task<ProductDTO> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetProductWithCategoriesAsync(id);

        var productDto = new ProductDTO
        {
            Id=product.Id,
            Name = product.Name,
            Price = product.Price,
            Properties = product.Properties,

            Categories = product.ProductCategories
            .Select(x=> new CategoryDTO
            {
                Id=x.CategoryId,
                Name=x.Category.Description,
                Description=x.Category.Description
            }).ToList()
        };
        return productDto;
    }

    public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
    {
         var products = await _productRepository.GetProductsWithCategoriesAsync();
           var productDtos= products
            .Select(x=> new ProductDTO
            {
                Id=x.Id,
                Name=x.Name,
                Price=x.Price,
                Properties=x.Properties,
                IsDeleted=x.IsDeleted,
                Categories = x.ProductCategories
            .Select(x => new CategoryDTO
            {
                Id = x.CategoryId,
                Name = x.Category.Description,
                Description = x.Category.Description
            }).ToList()
    }).ToList();
            return productDtos;
           
    }

    public async Task<IEnumerable<ProductDTO>> GetProductsAsync(bool isDeleted)
    {
        var products = await _productRepository.GetAllDeleteProductsAsync(isDeleted);
        var productDtos = products
         .Select(x => new ProductDTO
         {
             Id = x.Id,
             Name = x.Name,
             Price = x.Price,
             Properties = x.Properties,
             IsDeleted = x.IsDeleted,
             Categories = x.ProductCategories
            .Select(x => new CategoryDTO
            {
                Id = x.CategoryId,
                Name = x.Category.Description,
                Description = x.Category.Description
            }).ToList()
            }).
    ToList();
        return productDtos;
    }

    public async Task<IEnumerable<ProductDTO>> GetProductsByCategoryIdAsync(int categoryId)
    {
        var products = await _productRepository.GetProductsByCategoryAsync(categoryId);
        var productDtos = products
         .Select(x => new ProductDTO
         {
             Id = x.Id,
             Name = x.Name,
             Price = x.Price,
             Properties = x.Properties,
             IsDeleted = x.IsDeleted,

         }).ToList();
        return productDtos;
    }

    public async Task<ProductDTO> UpdateAsync(ProductUpdateDTO productUpdateDTO)
    {
        var product = await _productRepository.GetProductWithCategoriesAsync(productUpdateDTO.Id);
        product.ProductCategories.Clear();
         await _productRepository.UpdateAsync(product);
        product.Name = productUpdateDTO.Name;
        product.Properties = productUpdateDTO.Properties;
        product.Price = productUpdateDTO.Price;
        product.ProductCategories = productUpdateDTO.CategoryIds.Select(x=> new ProductCategory{
                ProductId=product.Id,
                CategoryId=x
         }).ToList();
        await _productRepository.UpdateAsync(product);
        var productDto = new ProductDTO
        {
            Id=product.Id,
            Name=product.Name,
            Properties=product.Properties,
            Price=product.Price,
            IsDeleted=product.IsDeleted,

            Categories=product.ProductCategories.Select(x=> new CategoryDTO{
                Id=x.CategoryId,
                Name=x.Category.Description,
                Description=x.Category.Description

            }).ToList() 
        };
        return productDto;
    }
    
}
