using System;

namespace DenemeApps.Entity.Abstract;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string?  School{ get; set; }
    public DateTime Age { get; set; }
}
