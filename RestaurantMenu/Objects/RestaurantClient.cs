using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantMenu.Objects
{
    public class RestaurantClient
    {
        //USER
        public Int32 UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Enable { get; set; }
        public DateTime LastLogin { get; set; }
        public string LostPassKey { get; set; }
        public Int32 Type { get; set; }
        public string Details { get; set; }
        public bool EmailVerify { get; set; }

        //CLIENT
        public Int32 ClientId { get; set; }
        public string CardNumber { get; set; }
        public Int32 CCExpMonth { get; set; }
        public Int32 CCExpYear { get; set; }
        public string CVV { get; set; }
    }
}