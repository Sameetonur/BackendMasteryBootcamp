using System;
using DenemeApps.Entity.Abstract;

namespace DenemeApps.Entity.Conrete;

public class Ogretmen : BaseEntity
{
    public string? Bolum { get; set; }

    public IEnumerable<Ogrenci> Ogrencis { get; set; }=[];


}
