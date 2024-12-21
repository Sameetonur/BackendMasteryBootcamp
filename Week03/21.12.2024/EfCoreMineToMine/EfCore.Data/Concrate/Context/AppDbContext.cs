using System;
using EfCore.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace EfCore.Data.Concrate.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options ) : base(options)
    {
        
    }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Product>? Products { get; set; }
    public DbSet<ProductCategory>? ProductCategories { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlServer("Server = localhost, 1441; Database = EfCoreDbMany; User = SA; Password = YourStrong@Passw0rd; TrustServerCertificate = true;");
    //     base.OnConfiguring(optionsBuilder);
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductCategory>()
            .HasKey(x=> new {x.ProductId,x.CategoryId});

        base.OnModelCreating(modelBuilder);
    }

}
