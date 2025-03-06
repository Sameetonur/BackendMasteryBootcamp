using System;
using SametApp.Entity.Abstract;

namespace SametApp.Entity.Contrete;

public class Product: BaseEntity
{
    public string? Properties { get; set; }

    public decimal Price { get; set; }

    public bool IsThere { get; set; }

    public int CategoryId { get; set; }

    public Category? Category { get; set; }


}
