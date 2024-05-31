using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantMenu.Waiter
{
    public partial class Waiter : System.Web.UI.MasterPage
    {
        public string UName;
        public static string LastLogin;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                protectPage();
                if (!this.IsPostBack)
                {
                    //check if validity is not expired
                    if (new BLL.Restaurants().IsValidityDateExpWaiter(new BLL.Restaurants().GetWaiterRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]))))
                    {
                        Response.Redirect("~/Public/Pricing.aspx");
                    }
                }
                else
                {

                }

                UName = Session[new Helpers.SessionNames().SiteUserName].ToString();
                getLastlogin();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void protectPage()
        {
            //session check
            if (Session[new Helpers.SessionNames().SiteUserId] != null && !string.IsNullOrEmpty(Session[new Helpers.SessionNames().SiteUserId].ToString()))
            {
                if (Session[new Helpers.SessionNames().SiteUserType] != null)
                {
                    if (Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserType]) != 3)
                    {
                        redirectUserType(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserType]));
                    }
                    //the user is waiter and should'nt redirect other modules or login page
                }
                else
                {
                    Response.Redirect("~/Public/Signin.aspx");
                }
            }
            else
            {
                //cookie check
                HttpCookie reqCookies = Request.Cookies[new Helpers.CookieNames().SiteUser];
                if (reqCookies != null && reqCookies.HasKeys && !string.IsNullOrEmpty(reqCookies[new Helpers.CookieNames().UserEmail].ToString()) && !string.IsNullOrEmpty(reqCookies[new Helpers.CookieNames().UserPassword].ToString()))
                {
                    signin(Helpers.Global.Decrypt(reqCookies[new Helpers.CookieNames().UserEmail].ToString()), reqCookies[new Helpers.CookieNames().UserPassword].ToString());

                }
                else
                {
                    Response.Redirect("~/Public/Signin.aspx");
                }
            }
        }
        private void redirectUserType(Int32 _userType)
        {
            if (_userType == 1)
            {
                Response.Redirect("~/Admin/Home.aspx");
            }
            if (_userType == 2)
            {
                Response.Redirect("~/Restaurant/Home.aspx");
            }
        }
        private void signin(string _email, string _password)
        {
            try
            {
                DataTable dtSiteUser = new BLL.Users().AuthenticateActiveUserGetData(_email, _password);

                if (dtSiteUser != null && dtSiteUser.Rows.Count > 0)
                {
                    setSesstion(dtSiteUser);

                    redirectUserType(Convert.ToInt32(dtSiteUser.Rows[0]["Type"]));
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('error','Sorry Invalid Email or Password','center',3000);", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void setSesstion(DataTable dtSiteUser)
        {
            Session[new Helpers.SessionNames().SiteUserId] = dtSiteUser.Rows[0]["Id"].ToString();
            Session[new Helpers.SessionNames().SiteUserName] = dtSiteUser.Rows[0]["Name"].ToString();
            Session[new Helpers.SessionNames().SiteUserLastLogin] = dtSiteUser.Rows[0]["LastLogin"].ToString();
            Session[new Helpers.SessionNames().SiteUserEmail] = dtSiteUser.Rows[0]["Email"].ToString();
            Session[new Helpers.SessionNames().SiteUserType] = dtSiteUser.Rows[0]["Type"].ToString();
            Session[new Helpers.SessionNames().SiteUserPassword] = dtSiteUser.Rows[0]["Password"].ToString();
        }
        private void getLastlogin()
        {
            if (!string.IsNullOrEmpty(Session[new Helpers.SessionNames().SiteUserLastLogin].ToString()))
            {
                LastLogin = new Helpers.Global().UTCtoEDT(Convert.ToDateTime(Session[new Helpers.SessionNames().SiteUserLastLogin].ToString())).ToString("hh:mmtt dd-MMM");
            }
            else
            {
                LastLogin = "Welcome First Time Login";
            }
        }
    }
}