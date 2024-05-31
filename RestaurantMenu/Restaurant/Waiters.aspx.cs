using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantMenu.Restaurant
{
    public partial class Waiters : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            protectPage();

            try
            {
                if (!this.IsPostBack)
                {
                    loadDgvSearch(" AND USR.[Enable]=1 AND USR.[Type]=3 AND RESW.RestaurantId=" + new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId])) + " ORDER BY USR.Name");
                    
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

        protected void dgvSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                this.dgvSearch.PageIndex = e.NewPageIndex;
                loadDgvSearch(" AND USR.[Enable]=1 AND USR.[Type]=3 AND RESW.RestaurantId=" + new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId])) + " ORDER BY USR.Name");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void dgvSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deletex")
            {
                try
                {
                    if (new BLL.Users().DeleteUserWaiter(Convert.ToInt32(this.dgvSearch.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0])
                        , Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId])))
                    {
                        loadDgvSearch(" AND USR.[Enable]=1 AND USR.[Type]=3 AND RESW.RestaurantId=" + new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId])) + " ORDER BY USR.Name");

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','The Waiter has been Deleted','center',2500);", true);
                        clearFieldsUpdatePanles();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('error','Sorry Unable to Delete. This Waiter is being used. You can Disable instead','center',5000);", true);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            if (e.CommandName == "editx")
            {
                try
                {
                    this.HiddenFieldGridEdit.Value = Convert.ToString(this.dgvSearch.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0]);
                    
                    DataTable dt = new DataTable();
                    dt = new BLL.Users().GetWaiterUsers(" AND USR.[Type]=3 AND RESW.RestaurantId=" + new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId])) +" AND USR.Id=" + this.HiddenFieldGridEdit.Value +"");

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        assignToCtrls(dt);
                        this.btnSave.Text = "Update Waiter";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "changeInputType", "changeInputType();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Nothing found of your selection','center',2500);", true);
                    }

                    this.updEdit.Update();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            if (e.CommandName == "Sendx")
            {
                DataTable dt = new DataTable();
                dt = new BLL.Users().GetWaiterUsers(" AND USR.Id=" + Convert.ToString(this.dgvSearch.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0]) + "");

                if (dt != null && dt.Rows.Count > 0)
                {
                    string emailMessage = "Welcome Waiter, Dear '" + dt.Rows[0]["Name"].ToString() + "', On " + ConfigurationManager.AppSettings["BusinessName"].ToString() + ". <br/> Your login details for " + ConfigurationManager.AppSettings["AppAddress"].ToString() + " <br/> Login '" + dt.Rows[0]["Email"].ToString() + "' <br/> Password '" + Helpers.Global.Decrypt(dt.Rows[0]["Password"].ToString()) + "' <br/><br/> Thank you.<br/><br/> Address " + ConfigurationManager.AppSettings["BusinessAddress"].ToString() + ". <br/> Email " + ConfigurationManager.AppSettings["BusinessEmail"].ToString() + ". ";
                    if (Helpers.Email.Send("", "", dt.Rows[0]["Email"].ToString(), dt.Rows[0]["Email"].ToString(), "Your Login Details for " + ConfigurationManager.AppSettings["BusinessName"].ToString() + "", emailMessage, true, null, null))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Login details has been sent to " + dt.Rows[0]["Name"].ToString() + "','center',5000);", true);
                    }else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('error','Login details was not sent to " + dt.Rows[0]["Name"].ToString() + "','center',5000);", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Nothing was found against this user','center',2500);", true);
                }
            }
        }
        private void assignToCtrls(DataTable dt)
        {
            try
            {
                this.txtId.Text = dt.Rows[0]["UserId"].ToString();
                this.txtName.Text = dt.Rows[0]["Name"].ToString();
                this.txtEmail.Text = dt.Rows[0]["Email"].ToString();
                this.txtPassword.Text = Helpers.Global.Decrypt(dt.Rows[0]["Password"].ToString());
                this.txtDiscription.Text = dt.Rows[0]["Details"].ToString();

                if (Convert.ToBoolean(dt.Rows[0]["Enable"]))
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
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.txtWaiterSearch.Text.Length > 0)
            {
                loadDgvSearch(" AND USR.[Type]=3 AND RESW.RestaurantId=" + new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId])) + " AND USR.[Name] LIKE '%" + txtWaiterSearch.Text + "%' ORDER BY USR.Name");
                this.updSearch.Update();
            }
            else
            {
                loadDgvSearch(" AND USR.[Type]=3 AND RESW.RestaurantId=" + new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId])) + " ORDER BY USR.Name");
                this.updSearch.Update();
            }
        }
        protected void clearFieldsUpdatePanles()
        {
            this.txtId.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.txtEmail.Text = string.Empty;
            this.txtPassword.Text = string.Empty;
            this.txtDiscription.Text = string.Empty;
            this.btnSave.Text = "Save";

            this.HiddenFieldGridEdit.Value = string.Empty;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "changeInputType", "changeInputType();", true);

            this.updEdit.Update();
            this.updSearch.Update();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clearFieldsUpdatePanles();
        }
        private Objects.Waiter assignToObj(string lossPassKey)
        {
            Objects.Waiter obj = new Objects.Waiter();

            if (txtId.Text.Length > 0)
            {
                obj.UserId = Convert.ToInt32(txtId.Text);
            }
            else
            {
                obj.UserId = 0;
            }

            try
            {
                obj.RestaurantId = new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]));
            }
            catch (Exception ex)
            {

                throw ex;
            }

            obj.Name = txtName.Text;
            try
            {
                obj.Email = new Helpers.Global().RemoveSpecialCharactersFromEmail(txtEmail.Text);
                obj.Password = Helpers.Global.Encrypt(txtPassword.Text);
                obj.Details = txtDiscription.Text;
                obj.Enable = (this.rdbEnable.Checked) ? true : false;
                obj.LostPassKey = lossPassKey;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return obj;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string pass = new Helpers.Global().GenerateStr(10);

                if (!string.IsNullOrEmpty(this.HiddenFieldGridEdit.Value) && !string.IsNullOrEmpty(this.txtId.Text))
                {
                    //update waiter
                    if (CtrlValidation())
                    {
                        if (new BLL.Users().UpdateWaiterUser(assignToObj(pass), Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]), waiterEmailUpdate))
                        {
                            if (waiterEmailUpdate)
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
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Waiter " + this.txtName.Text + " Account has been Updated. Waiter may Check his Email For New Email Verification','center',3000);", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Waiter " + this.txtName.Text + " Account has been Updated. But Could not send Email for New Email Verification','center',3000);", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Waiter " + this.txtName.Text + " Account has been updated','center',2500);", true);
                            }
                            
                            this.HiddenFieldGridEdit.Value = null;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('error','Sorry Waiter Account was not updated','center',2500);", true);
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else//insert new waiter
                {
                    if (CtrlValidation())
                    {
                        if (new BLL.Users().InsertWaiterUser(assignToObj(pass), Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId])))
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
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','New Waiter " + this.txtName.Text + " Account has been Created. Waiter may Check his Email For Email Verification','center',3000);", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','New Waiter " + this.txtName.Text + " Account has been Created. But Could not send Email for Email Verification','center',3000);", true);
                            }

                            this.HiddenFieldGridEdit.Value = null;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('error','Something went wrong. Sorry the Waiter was not saved.','center',2500);", true);
                            return;
                        }

                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            loadDgvSearch(" AND USR.[Enable]=1 AND USR.[Type]=3 AND RESW.RestaurantId=" + new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId])) + " ORDER BY USR.Name");
            
            clearFieldsUpdatePanles();
        }
        private void loadDgvSearch(string where)
        {
            try
            {
                this.dgvSearch.DataSource = new BLL.Users().GetWaiterUsers(where);
                dgvSearch.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool waiterEmailUpdate;
        private bool CtrlValidation()
        {
            if (this.txtName.Text.Length > 0)
            {
                if (this.txtEmail.Text.Length > 3)
                {
                    try
                    {
                        MailAddress m = new MailAddress(this.txtEmail.Text);
                    }
                    catch (Exception)
                    {
                        this.txtEmail.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Input an valid Email Format','center',2500);", true);
                        return false;
                    }

                    if (this.txtPassword.Text.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(this.HiddenFieldGridEdit.Value) && !string.IsNullOrEmpty(this.txtId.Text))
                        {
                            //updating //here we can check further if someone else email already or not
                            if (txtEmail.Text!=new BLL.Users().GetUserEmail(Convert.ToInt32(this.txtId.Text)))
                            {
                                if (!new BLL.Users().CheckEmailExist(new Helpers.Global().RemoveSpecialCharactersFromEmail(this.txtEmail.Text)))
                                {
                                    waiterEmailUpdate = true;
                                    return true;
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','An User Already Exist With This Email.','center',3000);", true);
                                    txtEmail.Focus();
                                    return false;
                                }
                            }
                            else
                            {
                                waiterEmailUpdate = false;
                                return true;
                            }
                            
                        }
                        else
                        {
                            if (!new BLL.Users().CheckEmailExist(new Helpers.Global().RemoveSpecialCharactersFromEmail(this.txtEmail.Text)))
                            {
                                return true;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','An User Already Exist With This Email.','center',3000);", true);
                                txtEmail.Focus();
                                return false;
                            }
                        }
                    }
                    else
                    {
                        this.txtPassword.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Input a Password','center',2500);", true);
                    }
                }
                else
                {
                    this.txtEmail.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Input an valid Email','center',2500);", true);
                }
            }
            else
            {
                this.txtName.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Input a Name','center',2500);", true);
            }
            return false;
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
    }
}