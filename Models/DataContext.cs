using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    // Class that provides data model components access to the database
    public class DataContext : DbContext
    {
        // Set up DataContext options and then pass it to the DbContext base class constructor
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        // (DbSet<T>) Represents collection of all (Product, Category, and Supplier) objects in the context
        // Used to query the database
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}
