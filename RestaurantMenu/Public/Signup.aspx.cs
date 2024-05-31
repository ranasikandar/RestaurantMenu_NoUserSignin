using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantMenu.Public
{
    public partial class Signup : System.Web.UI.Page
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
        private void creatHtmlNotification(string title, string msg, string icon)//icon=la-warning
        {
            string yourHTMLstring = string.Format(@"<div class='alert alert-secondary-bordered alert-lg square fade show' role='alert'>
                                        <button type='button' class='close' data-dismiss='alert' aria-label='Close'></button>
                                        <i class='la {0} mr-2'></i>
                                        <strong>{1}</strong> {2}
                                    </div>", icon, title, msg);

            PnlMsg.Controls.Add(new LiteralControl(yourHTMLstring));
        }
        protected void btnSignup_Click(object sender, EventArgs e)
        {
            try
            {
                if (validateCTRL())
                {
                    string pass = new Helpers.Global().GenerateStr(10);

                    if (new BLL.Users().UserRestaurantSignup(assignUserOBJ(pass)))
                    {

                        System.Collections.Specialized.NameValueCollection param_values = new System.Collections.Specialized.NameValueCollection();
                        param_values.Add("EMAIL", new Helpers.Global().RemoveSpecialCharactersFromEmail(txtEmail.Text));
                        param_values.Add("KEY", pass);
                        param_values.Add("BusinessName", ConfigurationManager.AppSettings["BusinessName"].ToString());
                        param_values.Add("AppAddress", ConfigurationManager.AppSettings["AppAddress"].ToString());
                        param_values.Add("AppName", ConfigurationManager.AppSettings["AppName"].ToString());
                        param_values.Add("BusinessEmail", ConfigurationManager.AppSettings["BusinessEmail"].ToString());
                        param_values.Add("BusinessAddress", ConfigurationManager.AppSettings["BusinessAddress"].ToString());
                        param_values.Add("WebsiteAddress", ConfigurationManager.AppSettings["WebsiteAddress"].ToString());
                        param_values.Add("FooterShort", ConfigurationManager.AppSettings["FooterShort"].ToString());

                        if (Helpers.Email.SendMail(txtEmail.Text, "Email_Verification.htm", param_values, null))
                        {
                            creatHtmlNotification("Success", " Your Account has been Created. Please Check Your Email For Account Activation.", "la-warning");
                            //Response.Redirect("~/Client/Home");
                        }
                        else
                        {
                            creatHtmlNotification("Sorry", " we have Created your Account but Sorry we could not send you email please contact to support", "la-warning");
                        }
                        disableCTRLs();
                    }
                    else
                    {
                        creatHtmlNotification("Sorry", " we are facing issue in creating your account Please try again later", "la-warning");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void disableCTRLs()
        {
            btnSignup.Visible = false;
        }
        private bool validateCTRL()
        {
            if (agree.Checked)
            {
                if (!string.IsNullOrEmpty(txtName.Text) && txtName.Text.Length <= 50)
                {
                    if (!string.IsNullOrEmpty(this.txtEmail.Text) && this.txtEmail.Text.Length <= 100)
                    {
                        if (!string.IsNullOrEmpty(txtPassword.Text) && txtPassword.Text.Length < 50)
                        {
                            try
                            {
                                MailAddress m = new MailAddress(this.txtEmail.Text);
                            }
                            catch (FormatException)
                            {
                                creatHtmlNotification("Sorry", " Email format is invalid!", "la-warning");
                                txtEmail.Focus();
                                return false;
                            }
                            if (!new BLL.Users().CheckEmailExist(new Helpers.Global().RemoveSpecialCharactersFromEmail(this.txtEmail.Text)))
                            {
                                return true;
                            }
                            else
                            {
                                creatHtmlNotification("Sorry", " This Email Already Exist. try with an other Email", "la-warning");
                                txtEmail.Focus();
                            }
                        }
                        else
                        {
                            creatHtmlNotification("Please", " Enter a Password for Signin", "la-warning");
                            txtPassword.Focus();
                        }

                    }
                    else
                    {
                        creatHtmlNotification("Sorry", " Email is Empty or Length is more then 100 characters", "la-warning");
                        txtEmail.Focus();
                    }
                }
                else
                {
                    creatHtmlNotification("Please", " Enter Your Full Name", "la-warning");
                    txtName.Focus();
                }
            }
            else
            {
                creatHtmlNotification("Please", "  Accept Terms and Conditions And Privacy Policy.", "la-warning");
            }
            return false;
        }
        private Objects.User assignUserOBJ(string key)
        {
            Objects.User obj = new Objects.User();

            try
            {
                obj.Name = new Helpers.Global().RemoveSpecialCharacters(txtName.Text);
                obj.Email = new Helpers.Global().RemoveSpecialCharactersFromEmail(txtEmail.Text);
                obj.Password = Helpers.Global.Encrypt(txtPassword.Text);

                obj.LostPassKey = key;
                obj.Details = "New Restaurant Signup";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return obj;
        }
    }
}