using System;
using System.ComponentModel.DataAnnotations;

namespace EShop.Shared.Dtos.Auth;

public class ResetPasswordDto
{
    public string? Token { get; set; }

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz!")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
    public string? ConfirmPassword { get; set; }

}
