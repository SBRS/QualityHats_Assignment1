using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QualityHats.Data;

namespace QualityHats.Models
{
    public static class DbInitializer
    {
        public static void Initialize(HatContext context)
        {
            context.Database.EnsureCreated();
            //if (context.Hats.Any())
            //{
            //    return;
            //}
            //var hats = new Hat[]
            //{
            //new Hat{HatName="Test",UnitPrice=15.10,Description="Test Description"},
            //new Hat{HatName="Test2",UnitPrice=14.10,Description="Test2 Description"},
            //};

            //foreach (Hat h in hats)
            //{
            //    context.Hats.Add(h);
            //}
            //context.SaveChanges();
        }
    }
}
