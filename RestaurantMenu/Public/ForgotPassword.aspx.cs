using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantMenu.Public
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            protectPage();
        }
        private void protectPage()
        {
            //session check
            if (Session[new Helpers.SessionNames().SiteUserId] != null && !string.IsNullOrEmpty(Session[new Helpers.SessionNames().SiteUserId].ToString()))
            {
                if (Session[new Helpers.SessionNames().SiteUserType] != null)
                {
                    redirectUserType(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserType]));
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
        protected void btnSignup_Click(object sender, EventArgs e)
        {
            if (validateCTRL())
            {
                DataTable dtSiteUser = new BLL.Users().GetUsers("AND Email='" + new Helpers.Global().RemoveSpecialCharactersFromEmail(this.txtEmail.Text) + "'");

                if (dtSiteUser != null && dtSiteUser.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtSiteUser.Rows[0]["Enable"]))
                    {
                        try
                        {
                            string pass = new Helpers.Global().GenerateStr(10);

                            new BLL.Users().ForgotUserPassword(new Helpers.Global().RemoveSpecialCharactersFromEmail(this.txtEmail.Text), pass);

                            System.Collections.Specialized.NameValueCollection param_values = new System.Collections.Specialized.NameValueCollection();
                            param_values.Add("EMAIL", txtEmail.Text);
                            param_values.Add("KEY", pass);
                            param_values.Add("BusinessName", ConfigurationManager.AppSettings["BusinessName"].ToString());
                            param_values.Add("AppAddress", ConfigurationManager.AppSettings["AppAddress"].ToString());
                            param_values.Add("AppName", ConfigurationManager.AppSettings["AppName"].ToString());
                            param_values.Add("BusinessEmail", ConfigurationManager.AppSettings["BusinessEmail"].ToString());
                            param_values.Add("BusinessAddress", ConfigurationManager.AppSettings["BusinessAddress"].ToString());
                            param_values.Add("WebsiteAddress", ConfigurationManager.AppSettings["WebsiteAddress"].ToString());
                            param_values.Add("FooterShort", ConfigurationManager.AppSettings["FooterShort"].ToString());

                            if (Helpers.Email.SendMail(new Helpers.Global().RemoveSpecialCharactersFromEmail(this.txtEmail.Text), "PasswordReset.htm", param_values, null))
                            {
                                creatHtmlNotification("Please Check Your Email", " to Reset your Password. an Email has been sent to '" + new Helpers.Global().RemoveSpecialCharactersFromEmail(this.txtEmail.Text) + "'", "la-warning");
                            }
                            else
                            {
                                creatHtmlNotification("Sorry", " we are facing issue in sending you Reset Password Email Please try again later", "la-warning");
                            }

                        }
                        catch (Exception ex)
                        {
                            Helpers.WriteLogToFile.Write(ex);
                            creatHtmlNotification("Sorry", " we could not Reset Your Password. Please try again later", "la-warning");
                        }
                    }
                    else
                    {
                        creatHtmlNotification("Sorry", " Your Account is not Enable. Please Contact to Support", "la-warning");
                    }
                }
                else
                {
                    creatHtmlNotification("Sorry", " We could not find this Email in our records.", "la-warning");
                }
            }
        }
        private bool validateCTRL()
        {
            if (!string.IsNullOrEmpty(this.txtEmail.Text) && this.txtEmail.Text.Length <= 50)
            {
                try
                {
                    MailAddress m = new MailAddress(this.txtEmail.Text);

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
                creatHtmlNotification("Sorry", " Email is Empty or invalid character Length", "la-warning");
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
    }
}