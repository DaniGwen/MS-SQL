using Newtonsoft.Json;
using p01_Products_Shop_Database.Data;
using p01_Products_Shop_Database.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ImportProductsFromJson
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var context = new ProductsShopContext())
            {
                string jsonPath = @"C:\products.json";

                var result = ImportProducts(context, jsonPath);

                Console.WriteLine(result);
            }
           
        }

        public static string ImportProducts(ProductsShopContext context, string inputJson)
        {
            List<Product> products = new List<Product>();

            try
            {
                products = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(inputJson));
            }
            catch (Exception e)
            {

                throw e.InnerException;
            }

            try
            {
                context.Products.AddRange(products);
                context.SaveChanges();
            }
            catch (Exception e)
            {

                throw e.InnerException;
            }

            return $"Successfully imported {context.Products.Count()}";
        }
    }
}
