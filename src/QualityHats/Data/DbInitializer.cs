using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QualityHats.Models;

namespace QualityHats.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.Hats.Any())
            {
                return;
            }

            var categories = new Category[]
            {
                new Category {CategoryName="Men’s Hats"},
                new Category {CategoryName="Women’s Hats"},
                new Category {CategoryName="Children’s Hats"},
            };
            foreach (Category category in categories)
            {
                context.Categories.Add(category);
            }
            context.SaveChanges();

            var suppliers = new Supplier[]
            {
                new Supplier {SupplierName="Serengetee", HomeNumber="092237689", Email="ninjas@serengeree.com" },
            };
            foreach (Supplier supplier in suppliers)
            {
                context.Suppliers.Add(supplier);
            }
            context.SaveChanges();

            var hats = new Hat[]
            {
            new Hat{CategoryID=1, SupplierID=1, HatName="GLOBETROTTER 5-PANEL", UnitPrice=15, Description="GLOBETROTTER 5-PANEL", ImagePath=""},
            new Hat{CategoryID=2, SupplierID=1, HatName="SERENGETEE BEANIE", UnitPrice=14, Description="SERENGETEE BEANIE", ImagePath=""},
            };

            foreach (Hat h in hats)
            {
                context.Hats.Add(h);
            }
            context.SaveChanges();
        }
    }
}
