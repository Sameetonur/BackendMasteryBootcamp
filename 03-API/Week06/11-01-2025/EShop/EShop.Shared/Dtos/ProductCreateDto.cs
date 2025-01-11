using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EShop.Shared.Dtos;

public class ProductCreateDto
{
    [Required(ErrorMessage ="Burası Boş Bırakılamaz!")]
    [StringLength(100, ErrorMessage = "Ürün adı adı en fazla 100 karakter olmalıdır!")]
    public string? Name { get; set; }


    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    [StringLength(10000, ErrorMessage = "Ürün adı adı en fazla 10000 karakter olmalıdır!")]
    public string? Properties { get; set; }


    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
     [Range(0.01, double.MaxValue, ErrorMessage ="Ürün fiyatı 0'dan büyük olmalıdır!")]
    public decimal? Price { get; set; }

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    public IFormFile? Image { get; set; }


    [Required(ErrorMessage = "En az bir seçim yapmak zorundasınız!")]
    public ICollection<int> CategoryIds { get; set; } = new List<int>();

}
