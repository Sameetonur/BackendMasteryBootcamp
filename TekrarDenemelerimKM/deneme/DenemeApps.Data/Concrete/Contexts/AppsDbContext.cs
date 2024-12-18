using System;
using DenemeApps.Entity.Conrete;
using Microsoft.EntityFrameworkCore;

namespace DenemeApps.Data.Concrete.Context;

public class AppsDbContexts : DbContext
{
    public DbSet<Ogrenci> Ogrencis { get; set; }
    public DbSet<Ogretmen> Ogretmens { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer
        ("Server = localhost, 1441; Database = EfCoreAppsDb; User = SA; Password = YourStrong@Passw0rd; TrustServerCertificate = true;");
        base.OnConfiguring(optionsBuilder);
    }

}
