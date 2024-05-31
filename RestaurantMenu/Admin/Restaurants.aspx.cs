using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantMenu.Admin
{
    public partial class Restaurants : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            protectPage();
            if (!this.IsPostBack)
            {
                loadDgvSearch("AND USR.EmailVerify=1 ORDER BY REST.Name");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pageloadRender", "pageloadRender();", true);
            }
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
                    if (Session[new Helpers.SessionNames().SiteUserType].ToString() != "1")
                    {
                        redirectUserType(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserType]));
                    }
                    //user is admin and have session
                }
                else
                {
                    Response.Redirect("~/Public/Signin.aspx");
                }
            }
        }
        private void redirectUserType(Int32 _userType)
        {
            if (_userType == 2)
            {
                Response.Redirect("~/Restaurant/Home.aspx");
            }
            if (_userType == 3)
            {
                Response.Redirect("~/Waiter/Home.aspx");
            }
        }
        private void loadDgvSearch(string where)
        {
            try
            {
                this.dgvSearch.DataSource = new BLL.Users().GetRestaurantUsers(where);
                dgvSearch.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void dgvSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgvSearch.PageIndex = e.NewPageIndex;
            loadDgvSearch("AND USR.EmailVerify=1 ORDER BY REST.Name");
        }
        protected void dgvSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editx")
            {
                this.HiddenFieldGridEdit.Value = Convert.ToString(this.dgvSearch.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0]);

                System.Data.DataTable dt = new System.Data.DataTable();
                dt = new BLL.Users().GetRestaurantUsers("AND REST.Id=" + this.HiddenFieldGridEdit.Value + "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    assignToCtrls(dt);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','No record found against your Search','center',2500);", true);
                }

                btnSave.Enabled = true;

                this.updEdit.Update();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.txtEmailSearch.Text.Length > 3 || this.txtResNameSearch.Text.Length>0)
            {
                string qry="";

                if (!string.IsNullOrEmpty(txtEmailSearch.Text))
                {
                    qry += "AND USR.Email='" + this.txtEmailSearch.Text+"'";
                }
                if (!string.IsNullOrEmpty(txtResNameSearch.Text))
                {
                    qry += " AND REST.Name LIKE '%" + this.txtResNameSearch.Text+"%'";
                }

                qry += " ORDER BY REST.Name";

                this.dgvSearch.DataSource = new BLL.Users().GetRestaurantUsers(qry);
                dgvSearch.DataBind();

                this.updSearch.Update();
            }
            else
            {
                this.dgvSearch.DataSource = new BLL.Users().GetRestaurantUsers("AND USR.EmailVerify=1 ORDER BY REST.Name");
                dgvSearch.DataBind();

                this.updSearch.Update();

                this.txtEmailSearch.Focus();
            }
        }
        private void assignToCtrls(DataTable dt)
        {
            this.txtId.Text = dt.Rows[0]["RestaurantID"].ToString();
            this.txtUserName.Text = dt.Rows[0]["UserName"].ToString();
            this.txtRestaurantName.Text = dt.Rows[0]["RestaurantName"].ToString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["RegisterDate"].ToString()))
            {
                this.txtRegDate.Text = Convert.ToDateTime(dt.Rows[0]["RegisterDate"].ToString()).ToString("dd-MM-yyyy");    
            }
            
            this.txtCompleteAddress.Text = dt.Rows[0]["Address"].ToString() + "," + dt.Rows[0]["City"].ToString() + "," + dt.Rows[0]["Country"].ToString();
            this.txtContact.Text = dt.Rows[0]["Phone"].ToString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["ValidityDate"].ToString()))
            {
                this.txtValidityTill.Text = Convert.ToDateTime(dt.Rows[0]["ValidityDate"].ToString()).ToString("dd-MM-yyyy");    
            }
            

            this.txtDetails.Text = dt.Rows[0]["Details"].ToString();

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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.HiddenFieldGridEdit.Value) && !string.IsNullOrEmpty(this.txtId.Text))
                {
                    //update user
                    if (CtrlValidation())
                    {
                        if (new BLL.Users().UpdateRestaurantsByAdmin(DateTime.ParseExact(this.txtValidityTill.Text, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture)
                            ,rdbEnable.Checked,Convert.ToInt32(txtId.Text),txtDetails.Text))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Restaurant " + this.txtRestaurantName.Text + " has been updated','center',2500);", true);
                            this.HiddenFieldGridEdit.Value = null;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('error','Something went wrong. Sorry Restaurant was not updated Correctly','center',2500);", true);
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

            loadDgvSearch("AND USR.EmailVerify=1 ORDER BY REST.Name");

            btnCancel_Click(sender, e);

            this.updSearch.Update();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.txtId.Text = "";
            this.txtUserName.Text = "";
            this.txtRestaurantName.Text = "";
            this.txtRegDate.Text = "";
            txtCompleteAddress.Text = "";
            txtContact.Text = "";
            txtValidityTill.Text = "";
            rdbEnable.Checked = true;
            this.txtDetails.Text = "";
            this.HiddenFieldGridEdit.Value = "";

            btnSave.Enabled = false;

            this.updEdit.Update();
        }
        private bool CtrlValidation()
        {
            if (!string.IsNullOrEmpty(txtValidityTill.Text)&&txtValidityTill.Text.Length==10)
            {
                try
                {
                    DateTime datetmp = DateTime.ParseExact(this.txtValidityTill.Text, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    return true;
                }
                catch (Exception)
                {
                    this.txtValidityTill.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('error','Please Enter valid date Format Example 31-12-2020','center',2500);", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Please set Validity Date for this Restaurant Format Example 31-12-2020','center',3000);", true);
                txtValidityTill.Focus();
            }
            return false;
        }
    }
}