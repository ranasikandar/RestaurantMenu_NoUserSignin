using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace RestaurantMenu
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }
        public void Application_Error(Object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Server.ClearError();

            if (exception.GetType() == typeof(HttpException))//any error
            {
                if (((HttpException)exception).GetHttpCode() == 404 || ((HttpException)exception).GetHttpCode()==403)//error 403.14 
                {
                    //cachePageNotFoundError(exception);
                    Response.Redirect("~/Errors/PageNotFound.aspx");
                }
                else
                {
                    //cacheAnyError(exception);
                    Response.Redirect("~/Errors/InternalServerError.aspx");
                }

            }
            else//error 500
            {
                Response.Redirect("~/Errors/InternalServerError.aspx");
            }
        }
    }
}