﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CornerStore.Migrations
{
    [DbContext(typeof(CornerStoreDbContext))]
    partial class CornerStoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CornerStore.Models.Cashier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Cashiers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Bill",
                            LastName = "Gates"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Will",
                            LastName = "Andrews"
                        });
                });

            modelBuilder.Entity("CornerStore.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryName = "Beverages"
                        },
                        new
                        {
                            Id = 2,
                            CategoryName = "Candies"
                        },
                        new
                        {
                            Id = 3,
                            CategoryName = "Snacks"
                        });
                });

            modelBuilder.Entity("CornerStore.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CashierId")
                        .HasColumnType("integer");

                    b.Property<DateOnly?>("PaidOnDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("CashierId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CashierId = 1,
                            PaidOnDate = new DateOnly(2023, 11, 11)
                        },
                        new
                        {
                            Id = 2,
                            CashierId = 2,
                            PaidOnDate = new DateOnly(2023, 11, 11)
                        },
                        new
                        {
                            Id = 3,
                            CashierId = 1,
                            PaidOnDate = new DateOnly(2023, 11, 11)
                        },
                        new
                        {
                            Id = 4,
                            CashierId = 1,
                            PaidOnDate = new DateOnly(2023, 11, 12)
                        },
                        new
                        {
                            Id = 5,
                            CashierId = 1,
                            PaidOnDate = new DateOnly(2023, 11, 12)
                        },
                        new
                        {
                            Id = 6,
                            CashierId = 2,
                            PaidOnDate = new DateOnly(2023, 11, 13)
                        },
                        new
                        {
                            Id = 7,
                            CashierId = 2,
                            PaidOnDate = new DateOnly(2023, 11, 13)
                        },
                        new
                        {
                            Id = 8,
                            CashierId = 2,
                            PaidOnDate = new DateOnly(2023, 11, 14)
                        },
                        new
                        {
                            Id = 9,
                            CashierId = 1,
                            PaidOnDate = new DateOnly(2023, 11, 15)
                        },
                        new
                        {
                            Id = 10,
                            CashierId = 2,
                            PaidOnDate = new DateOnly(2023, 11, 16)
                        });
                });

            modelBuilder.Entity("CornerStore.Models.OrderProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProducts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            OrderId = 1,
                            ProductId = 1,
                            Quantity = 2
                        },
                        new
                        {
                            Id = 2,
                            OrderId = 1,
                            ProductId = 2,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 3,
                            OrderId = 1,
                            ProductId = 3,
                            Quantity = 2
                        },
                        new
                        {
                            Id = 4,
                            OrderId = 2,
                            ProductId = 6,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 5,
                            OrderId = 2,
                            ProductId = 4,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 6,
                            OrderId = 2,
                            ProductId = 5,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 7,
                            OrderId = 3,
                            ProductId = 4,
                            Quantity = 2
                        },
                        new
                        {
                            Id = 8,
                            OrderId = 4,
                            ProductId = 2,
                            Quantity = 3
                        },
                        new
                        {
                            Id = 9,
                            OrderId = 5,
                            ProductId = 5,
                            Quantity = 2
                        },
                        new
                        {
                            Id = 10,
                            OrderId = 5,
                            ProductId = 2,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 11,
                            OrderId = 6,
                            ProductId = 6,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 12,
                            OrderId = 7,
                            ProductId = 1,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 13,
                            OrderId = 7,
                            ProductId = 3,
                            Quantity = 3
                        },
                        new
                        {
                            Id = 14,
                            OrderId = 7,
                            ProductId = 4,
                            Quantity = 2
                        },
                        new
                        {
                            Id = 15,
                            OrderId = 8,
                            ProductId = 5,
                            Quantity = 2
                        },
                        new
                        {
                            Id = 16,
                            OrderId = 9,
                            ProductId = 3,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 17,
                            OrderId = 9,
                            ProductId = 4,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 18,
                            OrderId = 10,
                            ProductId = 6,
                            Quantity = 2
                        },
                        new
                        {
                            Id = 19,
                            OrderId = 10,
                            ProductId = 3,
                            Quantity = 3
                        });
                });

            modelBuilder.Entity("CornerStore.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "Sprite",
                            CategoryId = 1,
                            Price = 2.99m,
                            ProductName = "12oz Can"
                        },
                        new
                        {
                            Id = 2,
                            Brand = "Coca-Cola",
                            CategoryId = 1,
                            Price = 2.50m,
                            ProductName = "20oz Bottle"
                        },
                        new
                        {
                            Id = 3,
                            Brand = "M&Ms",
                            CategoryId = 2,
                            Price = 2.75m,
                            ProductName = "Small Candy"
                        },
                        new
                        {
                            Id = 4,
                            Brand = "Twix",
                            CategoryId = 2,
                            Price = 3.50m,
                            ProductName = "Large Candy"
                        },
                        new
                        {
                            Id = 5,
                            Brand = "Lays",
                            CategoryId = 3,
                            Price = 3.25m,
                            ProductName = "Small Chips"
                        },
                        new
                        {
                            Id = 6,
                            Brand = "Doritos",
                            CategoryId = 3,
                            Price = 5.99m,
                            ProductName = "Large Chips"
                        });
                });

            modelBuilder.Entity("CornerStore.Models.Order", b =>
                {
                    b.HasOne("CornerStore.Models.Cashier", "Cashier")
                        .WithMany("Orders")
                        .HasForeignKey("CashierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cashier");
                });

            modelBuilder.Entity("CornerStore.Models.OrderProduct", b =>
                {
                    b.HasOne("CornerStore.Models.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CornerStore.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CornerStore.Models.Product", b =>
                {
                    b.HasOne("CornerStore.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CornerStore.Models.Cashier", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("CornerStore.Models.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
