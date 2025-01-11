using System;
using System.ComponentModel.DataAnnotations;

namespace EShop.Shared.Dtos;

public class CartUpdateDto
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    public bool IsActive { get; set; }

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    public bool IsDeleted { get; set; }

}
