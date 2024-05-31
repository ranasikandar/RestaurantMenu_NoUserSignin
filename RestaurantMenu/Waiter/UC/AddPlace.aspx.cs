using QRCoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantMenu.Waiter.UC
{
    public partial class AddPlace : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                protectPage();

                if (!this.IsPostBack)
                {

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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //save
                if (CtrlValidation())
                {
                    if (new BLL.Restaurants().InsertRestaurantTable(assignToOject(), Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId])))
                    {
                        clearCTRLs();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('information','Table " + this.txtName.Text + " has been saved','center',2500);", true);
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
                obj.Name = this.txtName.Text;
                obj.Discription = this.txtDisc.Text;
                obj.Enable = true;
                obj.QRCodeStr = validQRCode;

                Int32 restaurantId = new BLL.Restaurants().GetWaiterRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]));

                obj.QRImageLocation = "Reso/res/" + restaurantId + "/q/" + validQRCode + ".bmp";

                saveQRImage(validQRCode, restaurantId);

                obj.RestaurantId = restaurantId;


                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        string validQRCode;
        private bool CtrlValidation()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.txtName.Text))
                {
                    Int32 restaurantId = new BLL.Restaurants().GetWaiterRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]));
                    Int32? countQRCodes = 1;
                    string _qrCode = "";

                    while (countQRCodes > 0)
                    {
                        _qrCode = new Helpers.Global().GenerateStr(10);
                        countQRCodes = new BLL.Restaurants().CountQRTblId(restaurantId, _qrCode);
                    }
                    validQRCode = _qrCode;
                    return true;
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
        private void saveQRImage(string qrCodeStr, int restaurantId)
        {
            try
            {
                QRCoder.PayloadGenerator.Url generator = new QRCoder.PayloadGenerator.Url(ConfigurationManager.AppSettings["AppAddress"].ToString() + "Menu/Order.aspx?res=" + restaurantId + "&tbl=" + qrCodeStr);
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
                            qrCodeAsBitmap.Save(@filePath + @qrCodeStr + ".bmp");
                        }
                    }
                }

            }
            catch (Exception EX)
            {
                throw EX;
            }
        }
        private void clearCTRLs()
        {
            txtName.Text = "";
            txtDisc.Text = "";
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
                    if (Session[new Helpers.SessionNames().SiteUserType].ToString() != "3")
                    {
                        redirectUserType(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserType]));
                    }
                    //user is waiter and have session
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
            if (_userType == 2)
            {
                Response.Redirect("~/Restaurant/Home.aspx");
            }
            if (_userType == 4)
            {
                Response.Redirect("~/Menu/Home.aspx");
            }
        }
    }
}