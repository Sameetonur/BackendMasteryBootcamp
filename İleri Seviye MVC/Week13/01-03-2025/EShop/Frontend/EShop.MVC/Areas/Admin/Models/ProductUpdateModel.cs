using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EShop.MVC.Areas.Admin.Models;

public class ProductUpdateModel
{
    public int Id { get; set; }

    [DisplayName("Ürün")]
    [Required(ErrorMessage = "Ürün adı zorunludur!")]
    [StringLength(100, ErrorMessage = "Ürün adı en fazla 100 karakter olabilir!")]
    public string? Name { get; set; }

    [DisplayName("Ürün Açıklaması")]
    [Required(ErrorMessage = "Ürün Açıklaması zorunludur!")]

    public string Properties { get; set; } = null!;

    [DisplayName("Fiyat")]
    [Required(ErrorMessage = "Fiyat zorunludur!")]
    [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır!")]
    [RegularExpression(@"^\d+(\.\d\d{1,2})?")]

    public decimal? Price { get; set; }

    [DisplayName("Fotoğraf")]
    public IFormFile? Image { get; set; }

    [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
    public ICollection<int> CategoryIds { get; set; } = [];

    [Display(Name = "Aktif mi?")]
    public bool IsActive { get; set; }

    [Display(Name = "Silinsin mi?")]
    public bool IsDeleted { get; set; }

}
