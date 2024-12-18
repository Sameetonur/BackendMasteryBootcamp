using System;
using DenemeApps.Entity.Abstract;

namespace DenemeApps.Entity.Conrete;

public class Ogrenci : BaseEntity

{
    public int StudentNumber { get; set; }

    public int OgretmenId { get; set; }

    public Ogretmen? Ogretmen { get; set; }


}
