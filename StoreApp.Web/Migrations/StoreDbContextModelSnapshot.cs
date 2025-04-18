﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoreApp.Data.Concrete;

#nullable disable

namespace StoreApp.Web.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    partial class StoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("StoreApp.Data.Concrete.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Url")
                        .IsUnique();

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Telefon",
                            Url = "telefon"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Elektronik",
                            Url = "elektronik"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Beyaz Eşya",
                            Url = "beyaz-esya"
                        });
                });

            modelBuilder.Entity("StoreApp.Data.Concrete.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Güzel Telefon",
                            Name = "Samsung S24",
                            Price = 35000m
                        },
                        new
                        {
                            Id = 2,
                            Description = "Güzel Telefon",
                            Name = "Samsung S25",
                            Price = 45000m
                        },
                        new
                        {
                            Id = 3,
                            Description = "Güzel Dolap",
                            Name = "Buzdolabı",
                            Price = 55000m
                        },
                        new
                        {
                            Id = 4,
                            Description = "Güzel Telefon",
                            Name = "Samsung S27",
                            Price = 65000m
                        },
                        new
                        {
                            Id = 5,
                            Description = "Güzel Telefon",
                            Name = "Samsung S28",
                            Price = 75000m
                        },
                        new
                        {
                            Id = 6,
                            Description = "Güzel Telefon",
                            Name = "Samsung S29",
                            Price = 85000m
                        },
                        new
                        {
                            Id = 7,
                            Description = "Güzel Makine",
                            Name = "Çamaşır Makinesi",
                            Price = 95000m
                        });
                });

            modelBuilder.Entity("StoreApp.Data.Concrete.ProductCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CategoryId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductCategory");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            ProductId = 1
                        },
                        new
                        {
                            CategoryId = 2,
                            ProductId = 1
                        },
                        new
                        {
                            CategoryId = 1,
                            ProductId = 2
                        },
                        new
                        {
                            CategoryId = 3,
                            ProductId = 3
                        },
                        new
                        {
                            CategoryId = 1,
                            ProductId = 4
                        },
                        new
                        {
                            CategoryId = 2,
                            ProductId = 5
                        },
                        new
                        {
                            CategoryId = 2,
                            ProductId = 6
                        },
                        new
                        {
                            CategoryId = 3,
                            ProductId = 7
                        });
                });

            modelBuilder.Entity("StoreApp.Data.Concrete.ProductCategory", b =>
                {
                    b.HasOne("StoreApp.Data.Concrete.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoreApp.Data.Concrete.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
