using System;
using System.Data.SqlTypes;

namespace EShop.MVC.Areas.Admin.Models;

public class ProductCreateModel
{
    public string? Name { get; set; }
    public string Properties { get; set; } = null!;

    public decimal? Price { get; set; }

    public IFormFile? Image { get; set; }

    public ICollection<int> CategoryIds { get; set; } = [];

}
