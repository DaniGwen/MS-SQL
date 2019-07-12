using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace p01_Products_Shop_Database.Models
{
    public class User
    {
        //Users have an id, first name(optional) and last name(at least 3 characters) and age(optional).
        public int Id { get; set; }
        
        public string FirstName { get; set; }

        [MinLength(3)]
        public string LastName { get; set; }

        public int Age { get; set; }

        public ICollection<Product> ProductsSold { get; set; }

        public ICollection<Product> ProductsBought { get; set; }
    }
}
