using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EShop.Shared.Dtos;

public class CategoryUpdateDto
{

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    [StringLength(100, ErrorMessage = "Kategori adı en fazla 100 karakter olmalı!")]
    public string? Name { get; set; }

    [StringLength(500, ErrorMessage = "Kategori adı en fazla 300 karakter olmalı!")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    public IFormFile? Image { get; set; }

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    public bool IsActive { get; set; }

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    public bool IsDeleted { get; set; }

}
