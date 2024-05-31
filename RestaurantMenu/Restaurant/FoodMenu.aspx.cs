using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace RestaurantMenu.Restaurant
{
    public partial class FoodMenu : System.Web.UI.Page
    {
        public string ImageUrl = "~/Template/assets/img/albums/01.jpg";
        public string ImageUrlThumb = "~/Template/assets/img/albums/01.jpg";
        protected void Page_Load(object sender, EventArgs e)
        {
            protectPage();

            try
            {
                DataTable dtCategory;

                if (!this.IsPostBack)
                {
                    loadDgvSearch("AND USR.Id=" + Session[new Helpers.SessionNames().SiteUserId] + " ORDER BY PRO.Name");

                    dtCategory = new BLL.Restaurants().GetProductCategories(" AND RestaurantId=" + new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]))
                        + " ORDER BY CategoryName");
                    ddlCategory.DataSource = dtCategory;
                    ddlCategory.DataBind();

                    DataRow row = dtCategory.NewRow();
                    dtCategory.Rows.InsertAt(row, 0);
                    dtCategory.Rows[0]["Id"] = 0;
                    dtCategory.Rows[0]["CategoryName"] = "Any";

                    ddlCategorySearch.DataSource = dtCategory;
                    ddlCategorySearch.DataBind();
                }
                else
                {
                    if (Request["__EVENTARGUMENT"]=="updateddlcategory")
                    {
                        dtCategory = new BLL.Restaurants().GetProductCategories(" AND RestaurantId=" + new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]))
                                + " ORDER BY CategoryName");
                        ddlCategory.DataSource = dtCategory;
                        ddlCategory.DataBind();

                        DataRow row = dtCategory.NewRow();
                        dtCategory.Rows.InsertAt(row, 0);
                        dtCategory.Rows[0]["Id"] = 0;
                        dtCategory.Rows[0]["CategoryName"] = "Any";

                        ddlCategorySearch.DataSource = dtCategory;
                        ddlCategorySearch.DataBind();
                    }
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
            if (_userType == 3)
            {
                Response.Redirect("~/Waiter/Home.aspx");
            }
        }
        private void loadDgvSearch(string where)
        {
            try
            {
                this.dgvSearch.DataSource = new BLL.Restaurants().GetRestaurantProducts(where);
                dgvSearch.DataBind();
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
                loadDgvSearch("AND USR.Id=" + Session[new Helpers.SessionNames().SiteUserId] + " ORDER BY PRO.Name");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void dgvSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editx")
            {
                try
                {
                    this.HiddenFieldGridEdit.Value = Convert.ToString(this.dgvSearch.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0]);

                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt = new BLL.Restaurants().GetRestaurantProducts("AND USR.Id=" + Session[new Helpers.SessionNames().SiteUserId] + " AND PRO.Id=" + this.HiddenFieldGridEdit.Value + " ORDER BY PRO.Name");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        assignToCtrls(dt);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','No record found against your Search','center',2500);", true);
                    }

                    //btnSave.Enabled = true;
                    btnSave.Text = "Update";
                    this.updEdit.Update();
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
                txtId.Text = dt.Rows[0]["Id"].ToString();
                ddlCategory.SelectedValue = dt.Rows[0]["CategoryId"].ToString();
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtDiscription.Text = dt.Rows[0]["Discription"].ToString();

                txtPrice.Text = Convert.ToDouble(dt.Rows[0]["Price"].ToString()).ToString();

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

                if (!string.IsNullOrEmpty(dt.Rows[0]["ImagePath"].ToString()))
                {
                    Int32 restId = new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]));

                    ImageUrl = "~/Reso/res/" + restId + "/p/fsm/" + dt.Rows[0]["ImagePath"].ToString() + ".jpg";
                    ImageUrlThumb = "~/Reso/res/" + restId + "/p/tsm/" + dt.Rows[0]["ImagePath"].ToString() + ".jpg";

                    string filePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, @"Reso\res\" + restId + "\\p\\fsm\\" + dt.Rows[0]["ImagePath"].ToString() + ".jpg");

                    Session["SImageUrl"] = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, @"Reso\res\" + restId + "\\p\\fsm\\" + dt.Rows[0]["ImagePath"].ToString() + ".jpg");
                    Session["SImageUrlThumb"] = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, @"Reso\res\" + restId + "\\p\\tsm\\" + dt.Rows[0]["ImagePath"].ToString() + ".jpg");
                }
                else
                {
                    Session["SImageUrl"] = string.Empty;
                    Session["SImageUrlThumb"] = string.Empty;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = " AND USR.Id=" + Session[new Helpers.SessionNames().SiteUserId];

                if (this.txtFoodSearch.Text.Length > 0)
                {
                    if (!string.IsNullOrEmpty(this.txtFoodSearch.Text))
                    {
                        qry += " AND PRO.Name LIKE '%" + new Helpers.Global().RemoveSpecialCharacters(this.txtFoodSearch.Text) + "%'";
                    }
                }

                if (ddlCategorySearch.SelectedValue!="0")
                {
                    qry += " AND PRO.CategoryId="+Convert.ToInt32(ddlCategorySearch.SelectedValue);
                }

                qry += " ORDER BY PRO.Name";

                loadDgvSearch(qry);

                this.updSearch.Update();
                this.txtFoodSearch.Focus();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.HiddenFieldGridEdit.Value) && !string.IsNullOrEmpty(this.txtId.Text))
                {
                    //update food
                    if (CtrlValidation())
                    {
                        if (new BLL.Restaurants().UpdateFoodProduct(assignToOject(), Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId])))
                        {
                            //delete existing images
                            if (this.fileUploadID.HasFile)
                            {
                                try
                                {
                                    if (!string.IsNullOrEmpty(Session["SImageUrlThumb"].ToString()) && !string.IsNullOrEmpty(Session["SImageUrl"].ToString()))
                                    {
                                        if (File.Exists(@Session["SImageUrl"].ToString()))
                                        {
                                            File.Delete(@Session["SImageUrl"].ToString());
                                        }

                                        if (File.Exists(@Session["SImageUrlThumb"].ToString()))
                                        {
                                            File.Delete(@Session["SImageUrlThumb"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        //user dont change the image so no delete needed
                                    }
                                }
                                catch (Exception ex)
                                {

                                }
                            }

                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Food " + this.txtName.Text + " has been updated','center',2500);", true);
                            Helpers.Alert.Show("Food " + this.txtName.Text + " has been updated");
                            this.HiddenFieldGridEdit.Value = null;
                        }
                        else
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('error','Something went wrong. Sorry Restaurant was not updated Correctly','center',2500);", true);
                            Helpers.Alert.Show("Something went wrong. Sorry Food was not updated");
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    //save food
                    if (CtrlValidation())
                    {
                        if (new BLL.Restaurants().InsertFoodProduct(assignToOject(), Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId])))
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Food " + this.txtName.Text + " has been saved','center',2500);", true);
                            Helpers.Alert.Show("Food " + this.txtName.Text + " has been saved");
                            this.HiddenFieldGridEdit.Value = null;
                        }
                        else
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('error','Something went wrong. Sorry Food was not Saved Correctly','center',2500);", true);
                            Helpers.Alert.Show("Something went wrong. Sorry Food was not saved");
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

            try
            {
                loadDgvSearch("AND USR.Id=" + Session[new Helpers.SessionNames().SiteUserId] + " ORDER BY PRO.Name");
                btnCancel_Click(sender, e);
                this.updSearch.Update();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private Objects.Product assignToOject()
        {
            Objects.Product obj = new Objects.Product();

            try
            {
                if (!string.IsNullOrEmpty(this.txtId.Text))
                {
                    obj.Id = Convert.ToInt32(txtId.Text);
                }

                obj.CategoryId = Convert.ToInt32(this.ddlCategory.SelectedValue);
                obj.Name = this.txtName.Text;
                obj.Discription = this.txtDiscription.Text;
                obj.Price = Convert.ToDouble(txtPrice.Text);

                Int32 restaurantId = new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]));

                if (this.fileUploadID.HasFile)
                {
                    string fileName = Guid.NewGuid().ToString().Replace("-","");
                    obj.ImagePath = fileName;
                    saveImage(fileName,restaurantId);
                }

                obj.RestaurantId = restaurantId;
                obj.Enable = this.rdbEnable.Checked;

                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void saveImage(string fileName, Int32 restaurantId)
        {
            if (this.fileUploadID.HasFile)
            {
                try
                {
                    Stream strmStream = this.fileUploadID.PostedFile.InputStream;

                    System.Drawing.Image im = System.Drawing.Image.FromStream(strmStream);
                    
                    string filePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath
                        , @"Reso\res\" + restaurantId + "\\p\\fsm\\");
                    Directory.CreateDirectory(filePath);
                    im.Save(filePath + fileName + ".jpg", ImageFormat.Jpeg);

                    System.Drawing.Image.GetThumbnailImageAbort myCallback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                    System.Drawing.Image myThumbnail1 = im.GetThumbnailImage(685, 385, myCallback, IntPtr.Zero);
                    
                    filePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath
                        , @"Reso\res\" + restaurantId + "\\p\\tsm\\");
                    Directory.CreateDirectory(filePath);
                    myThumbnail1.Save(filePath + fileName + ".jpg", ImageFormat.Jpeg);

                }
                catch (Exception EX)
                {
                    throw EX;
                }
            }
        }
        public bool ThumbnailCallback()
        {
            return false;
        }
        private bool ImageValid()
        {
            try
            {
                if (this.fileUploadID.HasFile)
                {
                    string[] validFileTypes = { "jpg", "jpeg", "bmp", "png" };
                    string ext = System.IO.Path.GetExtension(fileUploadID.PostedFile.FileName);
                    bool isValidFile = false;

                    for (int i = 0; i < validFileTypes.Length; i++)
                    {
                        if (ext == "." + validFileTypes[i])
                        {
                            isValidFile = true;
                            break;
                        }
                    }

                    if (!isValidFile)
                    {
                        Helpers.Alert.Show("Invalid Image Format Please use .png .jpg .jpeg .bmp");
                        return false;
                    }
                    else
                    {
                        if (fileUploadID.PostedFile.ContentLength <= 2000000)
                        {
                            return true;
                        }
                        else
                        {
                            Helpers.Alert.Show("Please Select Image size less then 2MB");
                            return false;
                        }
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private bool CtrlValidation()
        {
            try
            {
                if (ddlCategory.SelectedIndex>=0)
                {
                    if (!string.IsNullOrEmpty(this.txtName.Text))
                    {
                        if (!string.IsNullOrEmpty(this.txtPrice.Text))
                        {
                            try
                            {
                                Convert.ToDouble(txtPrice.Text);
                                //return true;
                            }
                            catch (Exception)
                            {
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Please Enter Valid Food Price','center',3000);", true);
                                Helpers.Alert.Show("Please Enter Valid Food Price");
                                txtPrice.Focus();
                                return false;
                            }
                            if (ImageValid())
                            {
                                return true;
                            }
                        }
                        else
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Please Enter Food Price','center',3000);", true);
                            Helpers.Alert.Show("Please Enter Food Price");
                            txtPrice.Focus();
                        }
                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Please Enter Food Name','center',3000);", true);
                        Helpers.Alert.Show("Please Enter Food Name");
                        this.txtName.Focus();
                    }
                }
                else
                {
                    Helpers.Alert.Show("Please Select Category of this Food Item");
                    ddlCategory.Focus();
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.txtId.Text = "";
            this.txtName.Text = "";
            this.txtDiscription.Text = "";
            txtPrice.Text = "";
            rdbEnable.Checked = true;
            this.HiddenFieldGridEdit.Value = "";

            btnSave.Text = "Save";

            Session["SImageUrl"] = string.Empty;
            Session["SImageUrlThumb"] = string.Empty;

            ddlCategory.SelectedIndex = 0;
            this.updEdit.Update();
        }
    }
}