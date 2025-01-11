using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EShop.Shared.Dtos;

public class CategoryCreateDto
{
   
    public int Id { get; set; }

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    [StringLength(100, ErrorMessage ="Kategori adı en fazla 100 karakter olmalıdır!")]
    public string? Name { get; set; }

    [StringLength(500, ErrorMessage = "Kategori adı en fazla 300 karakter olmalıdır!")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    public IFormFile? Image { get; set; }
}
