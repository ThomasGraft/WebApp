using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    public class SeedData
    {
        public static void SeedDatabase(DataContext context)
        {
            // Apply all pending migrations for the context to the database
            context.Database.Migrate();
            
            // If database is empty
            if (context.Products.Count() == 0 && context.Suppliers.Count() == 0 && context.Categories.Count() == 0)
            {
                Supplier s1 = new Supplier { Name = "Samsung", City = "San Jose" };
                Supplier s2 = new Supplier { Name = "Wilson", City = "Chicago" };
                Supplier s3 = new Supplier { Name = "Levi", City = "New York" };

                Category c1 = new Category { Name = "Electronics" };
                Category c2 = new Category { Name = "Sports" };
                Category c3 = new Category { Name = "Clothing" };

                // Seed database with entities
                context.Products.AddRange(
                    new Product { Name = "Television", Price = 699, Supplier = s1, Category = c1 },
                    new Product { Name = "Stereo", Price = 148.95m, Supplier = s1, Category = c1 },
                    new Product { Name = "Football Helmet", Price = 69.99m, Supplier = s2, Category = c2 },
                    new Product { Name = "Basketball", Price = 24.50m, Supplier = s2, Category = c2 },
                    new Product { Name = "Golf Clubs", Price = 468.95m, Supplier = s2, Category = c2 },
                    new Product { Name = "Winter Coat", Price = 299.99m, Supplier = s3, Category = c3 },
                    new Product { Name = "Faded Demin Jacket", Price = 175, Supplier = s3, Category = c3 },
                    new Product { Name = "Demin Blue Jeans", Price = 54.79m, Supplier = s3, Category = c3 },
                    new Product { Name = "Leather Belt", Price = 20, Supplier = s3, Category = c3 });

                context.SaveChanges();
            }
        }
    }
}
