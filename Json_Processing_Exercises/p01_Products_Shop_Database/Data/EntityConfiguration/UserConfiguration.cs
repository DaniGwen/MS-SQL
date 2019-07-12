using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using p01_Products_Shop_Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace p01_Products_Shop_Database.Data.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(u => u.ProductsBought)
                .WithOne(p => p.BuyingUser)
                .HasForeignKey(p => p.BuyerId);

            builder.HasMany(u => u.ProductsSold)
                .WithOne(p => p.SellingUser)
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
