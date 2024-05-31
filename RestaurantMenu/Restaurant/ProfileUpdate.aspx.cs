using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace RestaurantMenu.Restaurant
{
    public partial class RestaurantProfileUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                protectPage();
                if (!this.IsPostBack)
                {
                    assignToCtrls(new BLL.Users().GetRestaurantUsers("AND USR.Id=" + Session[new Helpers.SessionNames().SiteUserId].ToString()));
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void assignToCtrls(DataTable dt)
        {
            try
            {
                if (string.IsNullOrEmpty(dt.Rows[0]["RestaurantName"].ToString()))
                {
                    IsUpdate.Value = "0";
                }
                else
                {
                    IsUpdate.Value = "1";
                }

                txtUserName.Text = dt.Rows[0]["UserName"].ToString();
                txtRestaurantName.Text = dt.Rows[0]["RestaurantName"].ToString();
                txtCountry.Text = dt.Rows[0]["Country"].ToString();
                txtCity.Text = dt.Rows[0]["City"].ToString();
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                txtPhone.Text = dt.Rows[0]["Phone"].ToString();
                txtCurrencyCode.Text = dt.Rows[0]["CurrencyCode"].ToString();
                txtOrderNotiEmail.Text = dt.Rows[0]["NotificationEmail"].ToString();

                if (!string.IsNullOrEmpty(dt.Rows[0]["SendEmailNotification"].ToString()))
                {
                    if (Convert.ToBoolean(dt.Rows[0]["SendEmailNotification"]))
                    {
                        this.rdbEnable.Checked = true;
                        this.rdbDisable.Checked = false;
                    }
                    else
                    {
                        this.rdbDisable.Checked = true;
                        this.rdbEnable.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
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
        private void protectPage()
        {
            //session check
            if (Session[new Helpers.SessionNames().SiteUserId] == null || string.IsNullOrEmpty(Session[new Helpers.SessionNames().SiteUserId].ToString()))
            {
                Response.Redirect("~/Public/Signin.aspx");
            }
            else
            {
                if (Session[new Helpers.SessionNames().SiteUserType] != null)
                {
                    if (Session[new Helpers.SessionNames().SiteUserType].ToString() != "2")
                    {
                        redirectUserType(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserType]));
                    }
                    //user is restaurant and have session
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
            if (_userType == 3)
            {
                Response.Redirect("~/Waiter/Home.aspx");
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (CtrlValidation())
                {
                    if (new BLL.Restaurants().UserRestaurantProfileUpdate(assingToObject()))
                    {
                        //Response.Redirect("~/Restaurant/Home.aspx");
                        creatHtmlNotification("Success", " Profile has been Updated", "la-warning");
                    }
                    else
                    {
                        creatHtmlNotification("Sorry", " Profile Was Not Updated Please try again", "la-warning");
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private Objects.RestaurantUser assingToObject()
        {
            Objects.RestaurantUser obj = new Objects.RestaurantUser();

            try
            {
                obj.UId = Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]);
                obj.UName = txtUserName.Text;
                obj.RName = txtRestaurantName.Text;
                obj.RCountry = txtCountry.Text;
                obj.RCity = txtCity.Text;
                obj.RAddress = txtAddress.Text;
                obj.RPhone = txtPhone.Text;
                obj.RCurrencyCode = txtCurrencyCode.Text;
                obj.RNotificationEmail = txtOrderNotiEmail.Text;
                obj.RSendEmailNotification = rdbEnable.Checked;

                if (IsUpdate.Value=="0")
                {
                    obj.RLogoPath = "/Template/assets/img/logo-2.png";
                    obj.RLogoSmallPath = "/Template/assets/img/smallLogo_tran.png";
                }
                else
                {
                    obj.RLogoPath = "";
                    obj.RLogoSmallPath = "";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return obj;
        }
        private bool CtrlValidation()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtUserName.Text))
                {
                    if (!string.IsNullOrEmpty(txtRestaurantName.Text))
                    {
                        if (!string.IsNullOrEmpty(txtCountry.Text))
                        {
                            if (!string.IsNullOrEmpty(txtCity.Text))
                            {
                                if (!string.IsNullOrEmpty(txtAddress.Text))
                                {
                                    if (!string.IsNullOrEmpty(txtPhone.Text))
                                    {
                                        if (!string.IsNullOrEmpty(txtCurrencyCode.Text))
                                        {
                                            if (!rdbEnable.Checked)
                                            {
                                                return true;
                                            }
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(txtOrderNotiEmail.Text))
                                                {
                                                    return true;
                                                }
                                                else
                                                {
                                                    creatHtmlNotification("Please", " Order Notification Email", "la-warning");
                                                    txtOrderNotiEmail.Focus();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            creatHtmlNotification("Please", " Enter Currency Code", "la-warning");
                                            txtCurrencyCode.Focus();
                                        }

                                    }
                                    else
                                    {
                                        creatHtmlNotification("Please", " Enter Phone Number", "la-warning");
                                        txtPhone.Focus();
                                    }

                                }
                                else
                                {
                                    creatHtmlNotification("Please", " Enter Complete Address", "la-warning");
                                    txtAddress.Focus();
                                }

                            }
                            else
                            {
                                creatHtmlNotification("Please", " Enter City Name", "la-warning");
                                txtCity.Focus();
                            }

                        }
                        else
                        {
                            creatHtmlNotification("Please", " Enter Country Name", "la-warning");
                            txtCountry.Focus();
                        }
                    }
                    else
                    {
                        creatHtmlNotification("Please", " Enter Restaurant Name.", "la-warning");
                        txtRestaurantName.Focus();
                    }
                }
                else
                {
                    creatHtmlNotification("Please", " Enter your full name.", "la-warning");
                    txtUserName.Focus();
                }
                return false;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}