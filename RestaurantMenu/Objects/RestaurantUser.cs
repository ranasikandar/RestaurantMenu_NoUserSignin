using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantMenu.Objects
{
    public class RestaurantUser
    {
        //user
        public int UId { get; set; }
        public string UUserName { get; set; }
        public string UName { get; set; }
        public string UEmail { get; set; }
        public string UPassword { get; set; }
        public bool UEnable { get; set; }
        public DateTime ULastLogin { get; set; }
        public string ULostPassKey { get; set; }
        public int UType { get; set; }
        public string UDetails { get; set; }
        public bool UEmailVarify { get; set; }
        //restaurant 
        public Int32 RId { get; set; }
        public string RName { get; set; }
        public DateTime RRegisterDate { get; set; }
        public string RCountry { get; set; }
        public string RCity { get; set; }
        public string RAddress { get; set; }
        public string RPhone { get; set; }
        public string RNotificationEmail { get; set; }
        public DateTime RValidityDate { get; set; }
        public int RAllowedTables { get; set; }
        public bool RSendEmailNotification { get; set; }
        public string RCurrencyCode { get; set; }
        public string RLogoPath { get; set; }
        public string RLogoSmallPath { get; set; }
    }
}