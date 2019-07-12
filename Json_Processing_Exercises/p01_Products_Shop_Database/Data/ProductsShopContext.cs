using Microsoft.EntityFrameworkCore;
using p01_Products_Shop_Database.Data.EntityConfiguration;
using p01_Products_Shop_Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace p01_Products_Shop_Database.Data
{
    public class ProductsShopContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionConfig.MyConnection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryProductConfigure());

            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
