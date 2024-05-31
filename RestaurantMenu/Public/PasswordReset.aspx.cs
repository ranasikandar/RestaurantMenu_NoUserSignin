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
    public partial class PasswordReset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            protectPage();

            if (!this.IsPostBack)
            {
                var email = Request.QueryString["email"];
                var key = Request.QueryString["key"];

                if ((email != null && !string.IsNullOrEmpty(email.ToString())) && (key != null && !string.IsNullOrEmpty(key.ToString())))
                {
                    if (validateEmail(email))
                    {
                        try
                        {
                            email = new Helpers.Global().RemoveSpecialCharactersFromEmail(email);

                            string query = "AND Email='" + email.ToString() + "' AND LostPassKey='" + new Helpers.Global().RemoveSpecialCharacters(key.ToString()) + "'";
                            DataTable dtUser = new BLL.Users().GetUsers(query);

                            if (dtUser != null && dtUser.Rows.Count > 0)
                            {
                                creatHtmlNotification("Please", " enter your new password for " + email + "", "la-warning");
                                this.txtNewPassword.Focus();
                            }
                            else
                            {
                                //email already varified or not found
                                creatHtmlNotification("Sorry", " this Email '" + email + "' does not requested password reset request or not found in our Records", "la-warning");
                            }
                        }
                        catch (Exception ex)
                        {
                            //error
                            Helpers.WriteLogToFile.Write(ex.Message);
                            creatHtmlNotification("Sorry", " there was an Error while your password reset. Please Try again leter or contact to support", "la-warning");
                        }
                    }

                }
                else//invalid input 
                {
                    //creatHtmlNotification("Sorry", " Invalid Request!", "la-warning");
                    Response.Redirect("~/Errors/InvalidRequest.aspx");
                }
            }
        }
        private bool validateEmail(string _email)
        {
            if (!string.IsNullOrEmpty(_email) && _email.Length <= 50)
            {
                try
                {
                    MailAddress m = new MailAddress(_email);

                    return true;
                }
                catch (FormatException)
                {
                    creatHtmlNotification("Sorry", " Email format in invalid!", "la-warning");
                    return false;
                }

            }
            else
            {
                creatHtmlNotification("Sorry", " Email is Empty or invalid characters length ", "la-warning");
                return false;
            }
        }
        private void creatHtmlNotification(string title, string msg, string icon)
        {
            string yourHTMLstring = string.Format(@"<div class='alert alert-secondary-bordered alert-lg square fade show' role='alert'>
                                        <button type='button' class='close' data-dismiss='alert' aria-label='Close'></button>
                                        <i class='la {0} mr-2'></i>
                                        <strong>{1}</strong> {2}
                                    </div>", icon, title, msg);

            PnlMsg.Controls.Add(new LiteralControl(yourHTMLstring));
        }
        private void protectPage()
        {
            //session check
            if (Session[new Helpers.SessionNames().SiteUserId] != null && !string.IsNullOrEmpty(new Helpers.SessionNames().SiteUserId))
            {
                if (Session[new Helpers.SessionNames().SiteUserType] != null)
                {
                    redirectUserType(Convert.ToInt32(new Helpers.SessionNames().SiteUserType));
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
            if (_userType == 3)
            {
                Response.Redirect("~/Waiter/Home.aspx");
            }
        }
        protected void btnSetNewPass_Click(object sender, EventArgs e)
        {
            try
            {
                if (validateEmail(Request.QueryString["email"]) && !string.IsNullOrEmpty(Request.QueryString["key"]) && !string.IsNullOrEmpty(this.txtNewPassword.Text))
                {
                    if (new BLL.Users().PasswordReset(new Helpers.Global().RemoveSpecialCharactersFromEmail(Request.QueryString["email"]), new Helpers.Global().RemoveSpecialCharacters(Request.QueryString["key"]), Helpers.Global.Encrypt(this.txtNewPassword.Text)))
                    {
                        creatHtmlNotification("Success", " You have set a new password now you may Login !", "la-warning");
                        Session.Abandon();
                    }
                    else
                    {
                        creatHtmlNotification("Sorry", " Your New Password was not set!", "la-warning");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}