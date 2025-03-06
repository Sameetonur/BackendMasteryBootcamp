using System;
using SametApp.Entity.Abstract;

namespace SametApp.Entity.Contrete;

public class Category : BaseEntity
{
    public string? Description { get; set; }

    public IEnumerable<Product> Products { get; set; } = [];

}
