using Newtonsoft.Json;
using p01_Products_Shop_Database.Data;
using p01_Products_Shop_Database.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ImportCategoriesFromJson
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var context = new ProductsShopContext())
            {
                string jsonFile = @"C:\categories.json";

                var result = ImportCategories(context, jsonFile);

                Console.WriteLine(result);
            }
        }

        public static string ImportCategories(ProductsShopContext context, string inputJson)
        {
            var categories = new List<Category>();

            try
            {
                categories = JsonConvert.DeserializeObject<List<Category>>(File.ReadAllText(inputJson));
            }
            catch (Exception e)
            {

                throw e.InnerException;
            }

            try
            {
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }
            catch (Exception e)
            {

                throw e.InnerException;
            }

            return $"Successfully imported {context.Categories.Count()}";
        }
    }
}
