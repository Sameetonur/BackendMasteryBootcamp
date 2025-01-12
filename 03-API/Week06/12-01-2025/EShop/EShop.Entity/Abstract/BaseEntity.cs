using System;

namespace EShop.Entity.Abstract;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.UtcNow; // Hep aynı olacak şekilde zaman dilimi belirleyici.

    public DateTime UpdatedDate { get; set; } 

    public bool IsActive { get; set; } = true; // aktif mi değil mi için hazırlnmış olan bir prop ve default olarak aktif olarak tutmak isrediğimden default olarak true olarak verdim.

    public bool IsDeleted { get; set; }  // silinmiş mi silinememişmiyi tutan prop default olarak silinmemiş olarakk gösteriliyor.
}
