using System;

namespace EfCore.Shared.Dtos;

public class CategoryCreateDTO
{
    public string? Name { get; set; }

    public bool IsDeleted { get; set; }

    public string? Description { get; set; }

}
