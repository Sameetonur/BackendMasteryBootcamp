using System;
using System.ComponentModel.DataAnnotations;

namespace EShop.Shared.Dtos.Auth;

public class ForgotPasswordDto
{
    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz!")]
    public string? Email { get; set; }

}
