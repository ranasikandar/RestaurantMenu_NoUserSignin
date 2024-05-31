using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantMenu.Objects
{
    public class Product
    {
        public Int32 Id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public string ImagePath { get; set; }
        public double Price { get; set; }
        public Int32 RestaurantId { get; set; }
        public bool Enable { get; set; }

        //new
        public int CategoryId { get; set; }
    }
}