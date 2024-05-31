using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantMenu.Objects
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Enable { get; set; }
        public DateTime LastLogin { get; set; }
        public string LostPassKey { get; set; }
        public int Type { get; set; }
        public string Details { get; set; }
        public bool EmailVarify { get; set; }
    }
}