using Newtonsoft.Json;
using p01_Products_Shop_Database.Data;
using p01_Products_Shop_Database.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ImportCategoriesProductsFromJson
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var context = new ProductsShopContext())
            {
                var jsonFile = @"C:\categories-products.json";

                var result = ImportCategoryProducts(context, jsonFile);

                Console.WriteLine(result);
            }

        }

        public static string ImportCategoryProducts(ProductsShopContext context, string inputJson)
        {
            var categoryProducts = new List<CategoryProduct>();

            try
            {
                categoryProducts = JsonConvert.DeserializeObject<List<CategoryProduct>>(File.ReadAllText(inputJson));
            }
            catch (Exception e)
            {

                throw e.InnerException;
            }

            try
            {
                context.CategoryProducts.AddRange(categoryProducts);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

            return $"Successfully imported {context.CategoryProducts.Count()}";
        }
    }
}
