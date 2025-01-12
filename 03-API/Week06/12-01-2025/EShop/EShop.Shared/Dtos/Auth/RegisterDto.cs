using System;
using System.ComponentModel.DataAnnotations;
using EShop.Shared.ComplexTypes;

namespace EShop.Shared.Dtos.Auth;

public class RegisterDto
{
    [Required(ErrorMessage ="Burası Boş Bırakılamaz!")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    public string? Address { get; set; }

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    public string? City { get; set; }

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    public GenderType Gender { get; set; }

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    [EmailAddress(ErrorMessage ="Geçerli bir email adresi giriniz!")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Burası Boş Bırakılamaz!")]
    [Compare("Password",ErrorMessage ="Şifreler uyuşmuyor")]
    public string? ConfirmPassword { get; set; }
    
    public string? Role { get; set; } ="User";


}
