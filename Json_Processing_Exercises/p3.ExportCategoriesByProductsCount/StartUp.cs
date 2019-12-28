using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using p01_Products_Shop_Database.Data;
using p01_Products_Shop_Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace p3.ExportCategoriesByProductsCount
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var context = new ProductsShopContext())
            {
                var result = GetCategoriesByProductsCount(context);

                Console.WriteLine(result);
            }
        }

        public static string GetCategoriesByProductsCount(ProductsShopContext context)
        {
            var products = context.Products
                .ToList();

            var categories = context.Categories
                .Include(c => c.CategoryProducts)
                .Where(c => c.Name != null)
                .Select(x => new objectDto
                {
                    Id = x.Id,
                    Category = x.Name,
                    ProductsCount = x.CategoryProducts.Count(),
                    TotalRevenue = x.CategoryProducts.Sum(c => c.Product.Price),
                    AveragePrice = x.CategoryProducts.Average(c => c.Product.Price)
                })
                .OrderByDescending(x => x.ProductsCount)
                .ToList();

            var resultJson = JsonConvert.SerializeObject(categories, Formatting.Indented);

            return resultJson;
        }
    }
}
