using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantMenu.Objects
{
    public class RestaurantTable
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public int RestaurantId { get; set; }
        public string Discription { get; set; }
        public bool Enable { get; set; }
        public string QRImageLocation { get; set; }
        public string QRCodeStr { get; set; }
    }
}