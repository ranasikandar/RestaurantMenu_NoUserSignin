using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantMenu.Objects
{
    public class Restaurant
    {
        public Int32 Id { get; set; }
        public string Name { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string NotificationEmail { get; set; }
        public DateTime ValidityDate { get; set; }
        public int AllowedTables { get; set; }
        public bool SendEmailNotification { get; set; }
        public string CurrencyCode { get; set; }
        public string LogoPath { get; set; }
        public string LogoSmallPath { get; set; }
    }
}