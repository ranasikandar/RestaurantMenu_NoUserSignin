using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QRCoder;
using System.Drawing;
using System.Configuration;
using System.IO;
using System.Web.Hosting;

namespace RestaurantMenu.Restaurant
{
    public partial class Tables : System.Web.UI.Page
    {
        public string ImageUrl = "~/Template/assets/img/albums/01.jpg";
        public string ImageUrlThumb = "~/Template/assets/img/albums/03.jpg";

        protected void Page_Load(object sender, EventArgs e)
        {
            protectPage();

            try
            {
                if (!this.IsPostBack)
                {
                    loadDgvSearch("AND USR.Id=" + Session[new Helpers.SessionNames().SiteUserId] + " ORDER BY TBL.Name");
                    this.txtQrCodeStr.Text = new Helpers.Global().GenerateStr(10);
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
            if (_userType == 3)
            {
                Response.Redirect("~/Waiter/Home.aspx");
            }
        }
        private void loadDgvSearch(string where)
        {
            try
            {
                this.dgvSearch.DataSource = new BLL.Restaurants().GetRestaurantTables(where);
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
                loadDgvSearch("AND USR.Id=" + Session[new Helpers.SessionNames().SiteUserId] + " ORDER BY TBL.Name");
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
                    dt = new BLL.Restaurants().GetRestaurantTables("AND USR.Id=" + Session[new Helpers.SessionNames().SiteUserId] + " AND TBL.Id=" + this.HiddenFieldGridEdit.Value + " ORDER BY TBL.Name");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        assignToCtrls(dt);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','No record found against your Search','center',2500);", true);
                    }

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
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtDiscription.Text = dt.Rows[0]["Discription"].ToString();

                txtQrCodeStr.Text = dt.Rows[0]["QrCodeStr"].ToString();

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

                if (!string.IsNullOrEmpty(dt.Rows[0]["QrCodeStr"].ToString()))
                {
                    Int32 restId = new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]));

                    ImageUrl = "~/Reso/res/" + restId + "/q/" + dt.Rows[0]["QrCodeStr"].ToString() + ".bmp";
                    ImageUrlThumb = ImageUrl;

                    Session["SImagePath"] = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, @"Reso\res\" + restId + "\\q\\" + dt.Rows[0]["QrCodeStr"].ToString() + ".bmp");
                    Session["SQrStr"] = dt.Rows[0]["QrCodeStr"].ToString();
                }
                else
                {
                    Session["SImagePath"] = string.Empty;
                    Session["SQrStr"] = string.Empty;
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
                if (this.txtTableSearch.Text.Length > 0)
                {
                    string qry = "";

                    qry += " AND TBL.Name LIKE '%" + this.txtTableSearch.Text + "%'";
                    qry += " ORDER BY TBL.Name";

                    loadDgvSearch("AND USR.Id=" + Session[new Helpers.SessionNames().SiteUserId] + qry);

                    this.updSearch.Update();
                }
                else
                {
                    loadDgvSearch("AND USR.Id=" + Session[new Helpers.SessionNames().SiteUserId] + " ORDER BY TBL.Name");

                    this.updSearch.Update();

                    this.txtTableSearch.Focus();
                }
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
                        if (new BLL.Restaurants().UpdateRestaurantTable(assignToOject(), Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId])))
                        {
                            //delete existing images
                                try
                                {
                                    if (!string.IsNullOrEmpty(Session["SImagePath"].ToString()) && Session["SQrStr"].ToString()!=txtQrCodeStr.Text)
                                    {
                                        if (File.Exists(@Session["SImagePath"].ToString()))
                                        {
                                            File.Delete(@Session["SImagePath"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        //user dont change the qr code so no delete needed
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Helpers.WriteLogToFile.Write(ex);
                                }

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Table " + this.txtName.Text + " has been updated','center',2500);", true);
                            this.HiddenFieldGridEdit.Value = null;
                            
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('error','Something went wrong. Sorry Table was not updated','center',2500);", true);
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
                        if (new BLL.Restaurants().InsertRestaurantTable(assignToOject(), Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId])))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Table " + this.txtName.Text + " has been saved','center',2500);", true);
                            this.HiddenFieldGridEdit.Value = null;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('error','Something went wrong. Sorry Table was not Saved','center',2500);", true);
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

            loadDgvSearch("AND USR.Id=" + Session[new Helpers.SessionNames().SiteUserId] + " ORDER BY TBL.Name");
            btnCancel_Click(sender, e);
            this.updSearch.Update();
        }
        private bool CtrlValidation()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.txtName.Text))
                {
                    if (!string.IsNullOrEmpty(this.txtQrCodeStr.Text))
                    {
                        Int32 restaurantId = new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]));
                        Int32? countQRCodes=new BLL.Restaurants().CountQRTblId(restaurantId, new Helpers.Global().RemoveSpecialCharacters(txtQrCodeStr.Text));
                        
                        if (!string.IsNullOrEmpty(this.HiddenFieldGridEdit.Value) && !string.IsNullOrEmpty(this.txtId.Text))
                        {
                            //updating existing table
                            if (txtQrCodeStr.Text==new BLL.Restaurants().GetTableQRCode(restaurantId,Convert.ToInt32(txtId.Text)))
                            {
                                return true;
                            }
                            else
                            {
                                if (countQRCodes != null && countQRCodes < 1)
                                {
                                    return true;
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','This QR Code Already Exist','center',3000);", true);
                                    this.txtQrCodeStr.Focus();
                                }
                            }
                            //ultimat false
                        }
                        else
                        {
                            //inserting new table
                            if (countQRCodes!=null&&countQRCodes<1)
                            {
                                return true;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','This QR Code Already Exist','center',3000);", true);
                                this.txtQrCodeStr.Focus();
                            }
                        }
                        //ultimat false
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Please Enter QR Code','center',3000);", true);
                        this.txtQrCodeStr.Focus();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Please Enter Table Name','center',3000);", true);
                    this.txtName.Focus();
                }

                return false;//ultimate
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private Objects.RestaurantTable assignToOject()
        {
            Objects.RestaurantTable obj = new Objects.RestaurantTable();

            try
            {
                if (!string.IsNullOrEmpty(this.txtId.Text))
                {
                    obj.Id = Convert.ToInt32(txtId.Text);
                }

                obj.Name = this.txtName.Text;
                obj.Discription = this.txtDiscription.Text;
                obj.Enable = this.rdbEnable.Checked;
                obj.QRCodeStr = this.txtQrCodeStr.Text;

                Int32 restaurantId = new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]));

                obj.QRImageLocation = "Reso/res/" + restaurantId + "/q/" + txtQrCodeStr.Text + ".bmp";

                saveQRImage(this.txtQrCodeStr.Text, restaurantId);

                obj.RestaurantId = restaurantId;


                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void saveQRImage(string qrCodeStr, int restaurantId)
        {
            try
            {
                QRCoder.PayloadGenerator.Url generator = new QRCoder.PayloadGenerator.Url(ConfigurationManager.AppSettings["AppAddress"].ToString() + "Menu/Order.aspx?res="+restaurantId+"&tbl="+qrCodeStr);
                string payload = generator.ToString();

                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                {
                    using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q))
                    {
                        using (QRCode qrCode = new QRCode(qrCodeData))
                        {
                            //var qrCodeAsBitmap = qrCode.GetGraphic(10, Color.Black, Color.White, (Bitmap)Bitmap.FromFile("D:\\logo.png"), logoSize);
                            var qrCodeAsBitmap = qrCode.GetGraphic(10, Color.Black, Color.White, true);
                            string filePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath
                                , @"Reso\res\" + restaurantId + "\\q\\");
                            Directory.CreateDirectory(@filePath);
                            qrCodeAsBitmap.Save(@filePath+@qrCodeStr+".bmp");
                        }
                    }
                }

            }
            catch (Exception EX)
            {
                throw EX;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.txtId.Text = "";
            this.txtName.Text = "";
            this.txtDiscription.Text = "";
            this.txtQrCodeStr.Text = new Helpers.Global().GenerateStr(10);
            rdbEnable.Checked = true;
            this.HiddenFieldGridEdit.Value = "";

            btnSave.Text = "Save";

            Session["SImagePath"] = string.Empty;
            Session["SQrStr"] = string.Empty;

            this.updEdit.Update();
        }
    }
}