using System;
using Microsoft.EntityFrameworkCore;
using SametApp.Entity.Contrete;

namespace SametApp.Data.Contrete.Context;

public class SametAppContext : DbContext
{
    public DbSet<Product>? Products { get; set; }

    public DbSet<Category>? Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = localhost, 1441; Database = SametDenemeDb; User = SA; Password = YourStrong@Passw0rd; TrustServerCertificate = true;");

        base.OnConfiguring(optionsBuilder);
    }

}
