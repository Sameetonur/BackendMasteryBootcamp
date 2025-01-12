using System;
using System.ComponentModel.DataAnnotations;

namespace EShop.Shared.Dtos.Auth;

public class LoginDto
{
    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    public string? UserName { get; set; }
    
    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    public string? Password { get; set; }
}
