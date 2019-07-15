using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using p01_Products_Shop_Database.Data;
using p01_Products_Shop_Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace p2.ExportSuccessfullySoldProducts
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var context = new ProductsShopContext())
            {
                var result = GetSoldProducts(context);

                Console.WriteLine(result);
            }
        }

        public static string GetSoldProducts(ProductsShopContext context)
        {
            var products = context.Products
                .ToList();

            var users = context.Users
                .Where(u => u.ProductsSold.Count > 0)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ToList();

            var collectionToExport = new List<object>();

            var sb = new StringBuilder();

            foreach (var user in users)
            {
                var newUser = new
                {
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    soldProducts = new List<object>()
                };

                foreach (var item in user.ProductsSold)
                {
                    if (item.BuyingUser == null)
                    {
                        continue;
                    }

                    var newItem = new
                    {
                        name = item.Name,
                        price = item.Price,
                        buyerFirstName = item.BuyingUser.FirstName,
                        buyerLastName = item.BuyingUser.LastName
                    };

                    newUser.soldProducts.Add(newItem);
                }
                collectionToExport.Add(newUser);
            }

            foreach (var item in collectionToExport)
            {
                sb.AppendLine(JsonConvert.SerializeObject(item, Formatting.Indented));
            }
     
            return sb.ToString();
        }
    }
}
