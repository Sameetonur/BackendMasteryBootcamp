using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EfCore.Shared.Dtos;

public class ProductCreateDTO
{
    public string? Name { get; set; }
    public string? Properties { get; set; }
    public decimal Price { get; set; }
    public int[] CategoryIds { get; set; }
    public List<CategoryDTO> CategoryList { get; set; }

}
