using System;
using System.Collections.Generic;
using System.Text;

namespace p01_Products_Shop_Database.Models
{
   public class CategoryProduct
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
