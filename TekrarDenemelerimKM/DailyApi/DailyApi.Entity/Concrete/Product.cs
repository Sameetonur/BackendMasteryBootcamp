using System;
using System.Reflection.Metadata;

namespace DailyApi.Entity.Concrete;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsActive { get; set; }


}
