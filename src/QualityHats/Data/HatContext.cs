using Microsoft.EntityFrameworkCore;
using QualityHats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualityHats.Data
{
    public class HatContext : DbContext
    {
        public HatContext(DbContextOptions<HatContext> options) : base(options)
        {
        }

        public DbSet<Hat> Hats { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderHat> OrderHats { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hat>().ToTable("Hat");
            modelBuilder.Entity<Supplier>().ToTable("Supplier");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<OrderHat>().ToTable("OrderHat");
            modelBuilder.Entity<Order>().ToTable("Order");

            modelBuilder.Entity<OrderHat>().HasKey(o => new { o.HatID, o.OrderID });
        }
    }
}
