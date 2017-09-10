using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using QualityHats.Data;

namespace QualityHats.Migrations
{
    [DbContext(typeof(HatContext))]
    partial class HatContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QualityHats.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName");

                    b.HasKey("CategoryID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("QualityHats.Models.Hat", b =>
                {
                    b.Property<int>("HatID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryID");

                    b.Property<string>("Description");

                    b.Property<string>("HatName");

                    b.Property<string>("ImagePath");

                    b.Property<int>("SupplierID");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("money");

                    b.HasKey("HatID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("SupplierID");

                    b.ToTable("Hat");
                });

            modelBuilder.Entity("QualityHats.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerID");

                    b.Property<decimal>("GST")
                        .HasColumnType("money");

                    b.Property<decimal>("GrandTotal")
                        .HasColumnType("money");

                    b.Property<string>("Status");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("money");

                    b.HasKey("OrderID");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("QualityHats.Models.OrderHat", b =>
                {
                    b.Property<int>("HatID");

                    b.Property<int>("OrderID");

                    b.Property<int>("Quantity");

                    b.HasKey("HatID", "OrderID");

                    b.HasIndex("HatID");

                    b.HasIndex("OrderID");

                    b.ToTable("OrderHat");
                });

            modelBuilder.Entity("QualityHats.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("HomeNumber");

                    b.Property<string>("MobileNumber");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("SupplierName");

                    b.HasKey("SupplierID");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("QualityHats.Models.Hat", b =>
                {
                    b.HasOne("QualityHats.Models.Category", "Category")
                        .WithMany("Hats")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QualityHats.Models.Supplier", "Supplier")
                        .WithMany("Hats")
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("QualityHats.Models.OrderHat", b =>
                {
                    b.HasOne("QualityHats.Models.Hat", "Hat")
                        .WithMany()
                        .HasForeignKey("HatID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("QualityHats.Models.Order", "Order")
                        .WithMany("OrderHats")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
