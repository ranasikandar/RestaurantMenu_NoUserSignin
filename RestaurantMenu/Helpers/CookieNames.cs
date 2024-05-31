using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantMenu.Helpers
{
    public class CookieNames
    {
        public string SiteUser = "RestMenu_USR_P";
        public string UserID = "USRID_P";
        public string UserEmail = "USREml_P";
        public string UserPassword = "USRPass_P";
        public string UserType = "USRTyp_P";
    }

    public class SessionNames
    {
        public string SiteUserId = "SUID_P";
        public string SiteUserName = "SUName_P";
        public string SiteUserEmail = "SUEmail_P";
        public string SiteUserType = "USRType_P";
        public string SiteUserPassword = "USRPass_P";
        public string SiteUserLastLogin = "SULastLog_P";
        public string DTableUser = "DTUser_P";
    }
}