using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantMenu.Public
{
    public partial class Signin : System.Web.UI.Page
    {
        DataTable dtSiteUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //logout
                if (Request.QueryString["LogOut"] == "1")
                {
                    Session.Abandon();
                    //Expire cookie
                    if (Request.Cookies[new Helpers.CookieNames().SiteUser] != null)
                    {
                        Response.Cookies[new Helpers.CookieNames().SiteUser].Expires = DateTime.UtcNow.AddDays(-1);
                    }
                    return;
                }
                
            }
            protectPage();
        }
        private void protectPage()
        {
            //session check
            try
            {
                string rurl = "";
                if (!string.IsNullOrEmpty(Request.QueryString["rUrl"] as string))
                {
                    rurl += "~"+Request.QueryString["rUrl"] as string + "&tbl=" + Request.QueryString["tbl"] as string;
                }

                if (Session[new Helpers.SessionNames().SiteUserId] != null && !string.IsNullOrEmpty(Session[new Helpers.SessionNames().SiteUserId].ToString()))
                {
                    if (Session[new Helpers.SessionNames().SiteUserType] != null)
                    {
                        redirectUserType(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserType]),rurl);
                    }
                }
                else
                {
                    //cookie check
                    HttpCookie reqCookies = Request.Cookies[new Helpers.CookieNames().SiteUser];
                    if (reqCookies != null && reqCookies.HasKeys && !string.IsNullOrEmpty(reqCookies[new Helpers.CookieNames().UserEmail].ToString()) && !string.IsNullOrEmpty(reqCookies[new Helpers.CookieNames().UserPassword].ToString()))
                    {
                        signin(Helpers.Global.Decrypt(reqCookies[new Helpers.CookieNames().UserEmail].ToString()), reqCookies[new Helpers.CookieNames().UserPassword].ToString(), false,rurl);

                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void redirectUserType(Int32 _userType, string returnURL)
        {
            if (!string.IsNullOrEmpty(returnURL)&&returnURL.Length>3)
            {
                Response.Redirect(returnURL);
            }
            if (_userType == 1)
            {
                Response.Redirect("~/Admin/Home.aspx");
            }
            if (_userType == 2)
            {
                Response.Redirect("~/Restaurant/Home.aspx");
            }
            if (_userType == 3)
            {
                Response.Redirect("~/Waiter/Home.aspx");
            }
        }
        private void signin(string _email, string _password, bool isSigninBtn, string returnURL)
        {
            try
            {
                dtSiteUser = new BLL.Users().AuthenticateActiveUserGetData(_email, _password);

                if (dtSiteUser != null && dtSiteUser.Rows.Count > 0)
                {
                    setSesstion(dtSiteUser);
                    if (remeber.Checked)
                    {
                        setCookies(dtSiteUser);
                    }

                    if (isSigninBtn)
                    {
                        new BLL.Users().InsertLastLogin(Convert.ToInt32(dtSiteUser.Rows[0]["Id"]));
                    }

                    redirectUserType(Convert.ToInt32(dtSiteUser.Rows[0]["Type"]), returnURL);
                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('error','Sorry Invalid Login Attempt','center',5000);", true);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void setCookies(DataTable dtSiteUser)
        {
            HttpCookie userInfo = new HttpCookie(new Helpers.CookieNames().SiteUser);

            userInfo[new Helpers.CookieNames().UserID] = Helpers.Global.Encrypt(dtSiteUser.Rows[0]["Id"].ToString());
            userInfo[new Helpers.CookieNames().UserEmail] = Helpers.Global.Encrypt(dtSiteUser.Rows[0]["Email"].ToString());
            userInfo[new Helpers.CookieNames().UserPassword] = dtSiteUser.Rows[0]["Password"].ToString();
            userInfo[new Helpers.CookieNames().UserType] = dtSiteUser.Rows[0]["Type"].ToString();

            userInfo.Expires = DateTime.UtcNow.AddDays(30.0);
            Response.Cookies.Add(userInfo);
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
        protected void btnSignin_Click(object sender, EventArgs e)
        {
            try
            {
                if (validateCTRL())
                {
                    string rurl = "";
                    if (!string.IsNullOrEmpty(Request.QueryString["rUrl"] as string))
                    {
                        rurl += "~"+Request.QueryString["rUrl"] as string + "&tbl=" + Request.QueryString["tbl"] as string;
                    }

                    signin(txtEmail.Text, Helpers.Global.Encrypt(txtPassword.Text), true,rurl);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private bool validateCTRL()
        {
            if (!string.IsNullOrEmpty(this.txtEmail.Text) &&
                !string.IsNullOrEmpty(this.txtPassword.Text) &&
                this.txtEmail.Text.Length <= 100 &&
                this.txtPassword.Text.Length <= 200)
            {
                try
                {
                    MailAddress m = new MailAddress(this.txtEmail.Text);

                    return true;
                }
                catch (FormatException)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Sorry Email format in invalid!','center',3000);", true);
                    return false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Sorry Email or Password is not valid!','center',3000);", true);//or length is grater than 100 500 characters
                return false;
            }
        }
    }
}