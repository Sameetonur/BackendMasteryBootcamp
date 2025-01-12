using System;
using System.ComponentModel.DataAnnotations;

namespace EShop.Shared.Dtos.Auth;

public class ChangePasswordDto
{
    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    public string? UserName { get; set; }
    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    public string? CurrentPassword { get; set; }

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    public string? NewPassword { get; set; }

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    [Compare("NewPassword", ErrorMessage = "Şifreler uyuşmuyor")]
    public string? ConfirmPassword { get; set; }

}
