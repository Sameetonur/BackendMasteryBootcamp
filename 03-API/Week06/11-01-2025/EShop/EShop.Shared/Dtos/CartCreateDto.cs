using System;
using System.ComponentModel.DataAnnotations;

namespace EShop.Shared.Dtos;

public class CartCreateDto
{
    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    public string? ApplicationUserId { get; set; }
}
