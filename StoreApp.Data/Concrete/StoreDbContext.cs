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
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Many to Many relationship
        // Product ile Category arasında çoktan çoğa ilişki var.
        modelBuilder.Entity<Product>()
            .HasMany(p => p.Categories)
            .WithMany(c => c.Products)
            .UsingEntity<ProductCategory>();

        modelBuilder.Entity<Category>()
            .HasIndex(c => c.Url)
            .IsUnique(); // Url alanı benzersiz olmalı


        modelBuilder.Entity<Product>().HasData(
            new List<Product>() {
                new() { Id=1, Name="Samsung S24", Price=35000, Description="Güzel Telefon" },
                new() { Id=2, Name="Samsung S25", Price=45000, Description="Güzel Telefon" },
                new() { Id=3, Name="Buzdolabı", Price=55000, Description="Güzel Dolap" },
                new() { Id=4, Name="Samsung S27", Price=65000, Description="Güzel Telefon" },
                new() { Id=5, Name="Samsung S28", Price=75000, Description="Güzel Telefon" },
                new() { Id=6, Name="Samsung S29", Price=85000, Description="Güzel Telefon" },
                new() { Id=7, Name="Çamaşır Makinesi", Price=95000, Description="Güzel Makine" }
            }
        );

        modelBuilder.Entity<Category>().HasData(
            new List<Category>() {
                new() { Id=1, Name="Telefon", Url="telefon" },
                new() { Id=2, Name="Elektronik", Url="elektronik" },
                new() { Id=3, Name="Beyaz Eşya", Url="beyaz-esya" },
                // extension method ile eklenebilir --> slug dotnet
            }
        );

        modelBuilder.Entity<ProductCategory>().HasData(
            new List<ProductCategory>() {
                new() { ProductId=1, CategoryId=1 },
                new() { ProductId=1, CategoryId=2 },
                new() { ProductId=2, CategoryId=1 },
                new() { ProductId=3, CategoryId=3 },
                new() { ProductId=4, CategoryId=1 },
                new() { ProductId=5, CategoryId=2 },
                new() { ProductId=6, CategoryId=2 },
                new() { ProductId=7, CategoryId=3 }
            }
        );


    }
}
