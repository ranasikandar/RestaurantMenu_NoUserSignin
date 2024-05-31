using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantMenu.Objects
{
    public class Waiter
    {
        //user
        public Int32 UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Enable { get; set; }
        public string LostPassKey { get; set; }
        public string Details { get; set; }

        //waiter user
        public Int32 WaiterId { get; set; }
        public Int32 RestaurantId { get; set; }
    }
}