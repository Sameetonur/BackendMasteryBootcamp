using System;
using System.ComponentModel.DataAnnotations;

namespace EShop.MVC.Areas.Admin.Models
{
    public class CategoryUpdateModel
    {

        public int Id { get; set; }

        [Display(Name = "Kategoryi")]
        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Kategori adı en fazla 100 karakter olmalıdır.")]
        public string? Name { get; set; }

        [Display(Name = "Kategoryi Açıklaması")]
        [StringLength(300, ErrorMessage = "Kategori açıklaması en fazla 300 karakter olmalıdır.")]
        public string? Description { get; set; }

        [Display(Name = "Kategoryi Görseli")]
        public IFormFile? Image { get; set; }

        [Display(Name = "Aktif mi?")]
        public bool IsActive { get; set; }

        [Display(Name = "Silinsin mi?")]
        public bool IsDeleted { get; set; }

    }
}
