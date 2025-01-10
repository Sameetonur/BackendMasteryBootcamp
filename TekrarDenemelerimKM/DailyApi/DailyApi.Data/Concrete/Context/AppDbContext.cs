using System;
using DailyApi.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DailyApi.Data.Concrete.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {


    }

    public DbSet<Product> Products { get; set; }

}
