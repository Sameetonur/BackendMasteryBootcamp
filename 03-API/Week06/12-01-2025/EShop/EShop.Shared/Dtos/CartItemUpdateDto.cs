using System;
using System.ComponentModel.DataAnnotations;

namespace EShop.Shared.Dtos;

public class CartItemUpdateDto
{
    [Required(ErrorMessage ="Burası Boş Bırakılamaz!")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    public int Quantity { get; set; }

}
