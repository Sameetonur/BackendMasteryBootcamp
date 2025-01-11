using System;
using System.ComponentModel.DataAnnotations;

namespace EShop.Shared.Dtos;

public class CartItemCreateDto
{
    [Required(ErrorMessage ="Burası Boş Bırakılamaz!")]
    public int CartId { get; set; }

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    public int Quantity { get; set; }
}
