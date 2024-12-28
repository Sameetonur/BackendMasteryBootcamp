using System;

namespace EfCore.Shared.Dtos;

public class ProductDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsDeleted { get; set; }
    public string? Properties { get; set; }
    public decimal Price { get; set; }
    public List<CategoryDTO>? Categories { get; set; }

}
