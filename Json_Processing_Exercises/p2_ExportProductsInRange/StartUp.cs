using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using p01_Products_Shop_Database.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace p2_ExportProductsInRange
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var context = new ProductsShopContext())
            {
                var result = GetProductsInRange(context);

                Console.WriteLine(result);
            }
        }

        public static string GetProductsInRange(ProductsShopContext context)
        {
            var productsInRange = context.Products
                .Where(p => p.Price > 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(x => new
                {
                    name = x.Name,
                    price = x.Price,
                    seller = x.SellingUser.FirstName + " " + x.SellingUser.LastName,
                })
                .ToList();

            var sb = new StringBuilder();

            var serializedObjects = new List<string>();

            foreach (var product in productsInRange)
            {
                //For console display
                sb.AppendLine(JsonConvert.SerializeObject(product, Formatting.Indented));

                //For export to json file
                serializedObjects.Add(JsonConvert.SerializeObject(product, Formatting.Indented));
            }

            string path = @"C:\products-in-range.json";

            File.WriteAllLines(path, serializedObjects);

            return sb.ToString();
        }
    }
}
