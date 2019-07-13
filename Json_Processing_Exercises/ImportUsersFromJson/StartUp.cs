using Newtonsoft.Json;
using p01_Products_Shop_Database.Data;
using p01_Products_Shop_Database.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ImportUsersFromJson
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var fileJson = @"C:\users.json";

            using (var context = new ProductsShopContext())
            {
                string result = ImportUsers(context, fileJson);

                Console.WriteLine(result);
            }

        }

        public static string ImportUsers(ProductsShopContext context, string inputJson)
        {
            List<User> users = new List<User>();

            try
            {
                users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(inputJson)); 
            }
            catch (Exception)
            {
                throw new JsonSerializationException("Couldn't deserialize Json!");
            }

            try
            {
                context.Users.AddRange(users);
                context.SaveChanges();
            }
            catch (Exception e )
            {
              throw e.InnerException;
            }
           

            return $"Successfully imported {context.Users.Count()}";
        }
    }
}
