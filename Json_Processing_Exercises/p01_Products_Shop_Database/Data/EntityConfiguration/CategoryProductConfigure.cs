using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using p01_Products_Shop_Database.Models;

namespace p01_Products_Shop_Database.Data
{
    public class CategoryProductConfigure : IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryProduct> builder)
        {
            builder.HasKey(c => new { c.ProductId, c.CategoryId }); 
        }
    }
}
