using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantMenu.Public
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                protectPage();
            }
        }
        private void protectPage()
        {
            if (Session[new Helpers.SessionNames().SiteUserId] == null || string.IsNullOrEmpty(Session[new Helpers.SessionNames().SiteUserId].ToString()))
            {
                Response.Redirect("~/Public/Signin.aspx");
            }
        }
        private void creatHtmlNotification(string title, string msg, string icon)//icon=la-warning
        {
            string yourHTMLstring = string.Format(@"<div class='alert alert-secondary-bordered alert-lg square fade show' role='alert'>
                                        <button type='button' class='close' data-dismiss='alert' aria-label='Close'></button>
                                        <i class='la {0} mr-2'></i>
                                        <strong>{1}</strong> {2}
                                    </div>", icon, title, msg);

            PnlMsg.Controls.Add(new LiteralControl(yourHTMLstring));
        }
        protected void btnChangePass_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCurrentPassword.Text.Length > 0 && this.txtNewPassword.Text.Length > 0)
                {
                    if (Helpers.Global.Encrypt(this.txtCurrentPassword.Text) == Convert.ToString(Session[new Helpers.SessionNames().SiteUserPassword]))
                    {
                        if (new BLL.Users().ChangePassword(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]), Helpers.Global.Encrypt(this.txtNewPassword.Text)))
                        {
                            creatHtmlNotification("Success", " Your Password has been changed Please login again with your new password.", "la-warning");

                            Session.Abandon();
                            //Expire cookie
                            if (Request.Cookies[new Helpers.CookieNames().SiteUser] != null)
                            {
                                Response.Cookies[new Helpers.CookieNames().SiteUser].Expires = DateTime.UtcNow.AddDays(-1);
                            }
                        }
                        else
                        {
                            creatHtmlNotification("Sorry", " Your Password was not changed please try again leter.", "la-warning");
                        }
                    }
                    else
                    {
                        creatHtmlNotification("Sorry", " Your Current Password does not match.", "la-warning");
                    }
                }
                else
                {
                    creatHtmlNotification("Sorry", " Your current password and new password should not be empty", "la-warning");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}