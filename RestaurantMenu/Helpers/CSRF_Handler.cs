using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantMenu.Helpers
{
    public class CSRF_Handler
    {
        public static void Validate(Page page, HiddenField forgeryToken)
        {
            if (!page.IsPostBack)
            {
                Guid antiforgeryToken = Guid.NewGuid();
                page.Session["AntiforgeryToken"] = antiforgeryToken;
                forgeryToken.Value = antiforgeryToken.ToString();
            }
            else
            {
                Guid stored = (Guid)page.Session["AntiforgeryToken"];
                Guid sent = new Guid(forgeryToken.Value);
                if (sent != stored)
                {
                    //WriteLogToFile.Write("CSRF ATTACK ALERT");
                    ////Logout
                    //page.Session.Abandon();
                    ////Expire cookie
                    //page.Response.Cookies[new Helpers.CookieNames().SiteUser].Expires = DateTime.UtcNow.AddDays(-1);
                    //page.Response.Redirect("~/");

                }
            }
        }
    }
}