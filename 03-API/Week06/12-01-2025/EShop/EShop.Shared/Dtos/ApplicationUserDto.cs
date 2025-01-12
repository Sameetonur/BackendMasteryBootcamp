using System;
using EShop.Shared.ComplexTypes;

namespace EShop.Shared.Dtos;

public class ApplicationUserDto
{
    public string? Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateofBirth { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public GenderType Gender { get; set; }
    public bool EmailConfirmed { get; set; }
    public string? PhoneNumber { get; set; }
}
