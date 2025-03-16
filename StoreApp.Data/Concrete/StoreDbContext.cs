using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;

namespace StoreApp.Data.Concrete;

public class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions<StoreDbContext> options)
        : base(options)
    {

    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasData(
            new List<Product>() {
                new() { Id=1, Name="Samsung S24", Price=35000, Description="Güzel Telefon", Category="Telefon" },
                new() { Id=2, Name="Samsung S25", Price=45000, Description="Güzel Telefon", Category="Telefon" },
                new() { Id=3, Name="Samsung S26", Price=55000, Description="Güzel Telefon", Category="Telefon" },
                new() { Id=4, Name="Samsung S27", Price=65000, Description="Güzel Telefon", Category="Telefon" },
                new() { Id=5, Name="Samsung S28", Price=75000, Description="Güzel Telefon", Category="Telefon" },
                new() { Id=6, Name="Samsung S29", Price=85000, Description="Güzel Telefon", Category="Telefon" },
                new() { Id=7, Name="Samsung S30", Price=95000, Description="Güzel Telefon", Category="Telefon" }
            }
        );
    }
}
