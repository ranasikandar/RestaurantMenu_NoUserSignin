using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantMenu.Objects
{
    public class ProductCategories
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string CategoryName { get; set; }
        public string Discription { get; set; }
        public int DisplayOrder { get; set; }
    }
}