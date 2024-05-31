using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantMenu.Restaurant.UC
{
    public partial class ProductCategories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            protectPage();

            try
            {
                if (!this.IsPostBack)
                {
                    loadDgvSearch(" AND RestaurantId=" + new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId])) 
                        + " ORDER BY CategoryName");
                    btnSubmit.Text = "Save";
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
        }
        private void loadDgvSearch(string where)
        {
            try
            {
                this.dgvSearch.DataSource = new BLL.Restaurants().GetProductCategories(where);
                dgvSearch.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.HiddenFieldGridEdit.Value) && !string.IsNullOrEmpty(this.txtId.Text))
                {
                    //update category
                    if (CtrlValidation())
                    {
                        if (new BLL.Restaurants().UpdateFoodProductCategory(assignToObj(), Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId])))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Category " + this.txtCategory.Text + " has been updated','center',2500);", true);
                            this.HiddenFieldGridEdit.Value = null;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('error','Sorry the Category was not updated','center',2500);", true);
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else//insert new category
                {
                    if (CtrlValidation())
                    {
                        if (new BLL.Restaurants().InsertFoodProductCategory(assignToObj(), Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId])))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','New Category " + this.txtCategory.Text + " has been saved','center',2500);", true);
                            this.HiddenFieldGridEdit.Value = null;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('error','Something went wrong. Sorry the Category was not saved.','center',2500);", true);
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

            loadDgvSearch(" AND RestaurantId=" + new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]))
                        + " ORDER BY CategoryName");

            clearFieldsUpdatePanles();
        }
        private Objects.ProductCategories assignToObj()
        {
            Objects.ProductCategories obj = new Objects.ProductCategories();

            if (txtId.Text.Length > 0)
            {
                obj.Id = Convert.ToInt32(txtId.Text);
            }
            else
            {
                obj.Id = 0;
            }

            try
            {
                obj.RestaurantId = new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]));
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            obj.CategoryName = txtCategory.Text;
            obj.Discription = txtDiscription.Text;

            if (txtDispayOrder.Text.Length>0)
            {
                obj.DisplayOrder = Convert.ToInt32(this.txtDispayOrder.Text);
            }
            else
            {
                obj.DisplayOrder = 0;
            }

            return obj;
        }
        private bool CtrlValidation()
        {
            if (this.txtCategory.Text.Length > 0)
            {
                if (txtDispayOrder.Text.Length>0)
                {
                    try
                    {
                        Convert.ToInt32(txtDispayOrder.Text);
                    }
                    catch (Exception)
                    {
                        this.txtDispayOrder.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Please Input Valid Display Order','center',2500);", true);
                    }

                    return true;//go on...
                }
                else
                {
                    return true;
                }
                
            }
            else
            {
                this.txtCategory.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Please Input Category Name','center',2500);", true);
            }
            return false;
        }
        protected void clearFieldsUpdatePanles()
        {
            this.txtId.Text = string.Empty;
            this.txtCategory.Text = string.Empty;
            this.txtDiscription.Text = string.Empty;
            this.txtDispayOrder.Text = "0";
            this.btnSubmit.Text = "Save";

            this.HiddenFieldGridEdit.Value = string.Empty;

            this.updAddEdit.Update();
            this.updSearch.Update();
        }
        protected void dgvSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deletex")
            {
                try
                {
                    if (new BLL.Restaurants().DeleteFoodProductCategory(Convert.ToInt32(this.dgvSearch.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0])
                        , Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId])))
                    {
                        loadDgvSearch(" AND RestaurantId=" + new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]))
                        + " ORDER BY CategoryName");

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','The Category has been Deleted','center',2500);", true);
                        //this.updSearch.Update();
                        clearFieldsUpdatePanles();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('error','Sorry Unable to Delete. Any of the Products could be using this category','center',5000);", true);
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

                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt = new BLL.Restaurants().GetProductCategories(" AND Id=" + this.HiddenFieldGridEdit.Value + " AND RestaurantId="
                        + new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId])) + "");

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        assignToCtrls(dt);
                        btnSubmit.Text = "Update Category";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Nothing found of your selection','center',2500);", true);
                    }

                    this.updAddEdit.Update();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        private void assignToCtrls(System.Data.DataTable dt)
        {
            try
            {
                this.txtId.Text = dt.Rows[0]["Id"].ToString();
                this.txtCategory.Text = dt.Rows[0]["CategoryName"].ToString();
                this.txtDiscription.Text = dt.Rows[0]["Discription"].ToString();
                this.txtDispayOrder.Text= dt.Rows[0]["DisplayOrder"].ToString();
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
                loadDgvSearch(" AND RestaurantId=" + new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]))
                        + " ORDER BY CategoryName");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}