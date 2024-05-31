using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantMenu.Menu
{
    public partial class Menu : System.Web.UI.MasterPage
    {
        public static string MenuURL;
        public static string MyOrderURL;

        public string SmallLogoUrl = "~/Template/assets/img/smallLogo_tran.png";
        public string BigLogoUrl = "~/Template/assets/img/logo-2.png";

        public static string UrlPara;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Helpers.CSRF_Handler.Validate(this.Page, forgeryToken);
            protectQrCodeValidation();

            //load restaurant logo urls
            getBrandingLogos(Convert.ToInt32(Request.QueryString["res"].ToString()));
        }
        private void getBrandingLogos(int restId)
        {
            try
            {
                string[] logos = new BLL.Restaurants().GetBrandingLogos(restId);
                if (logos.Length>0)
                {
                    BigLogoUrl = "~" + logos[0];
                    SmallLogoUrl = "~" + logos[1];
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void protectQrCodeValidation()
        {
            if (Session["MenuURL"] == null || string.IsNullOrEmpty(Session["MenuURL"].ToString()))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["res"]) && !string.IsNullOrEmpty(Request.QueryString["tbl"]))
                {
                    Session["MenuURL"] = "~/Menu/Order.aspx?res=" + Request.QueryString["res"] + "&tbl=" + Request.QueryString["tbl"] + "" as string;
                    Session["MyOrderURL"] = "~/Menu/MyOrders.aspx?res=" + Request.QueryString["res"] + "&tbl=" + Request.QueryString["tbl"] + "" as string;
                    Session["UrlPara"] = "?res=" + Request.QueryString["res"] + "&tbl=" + Request.QueryString["tbl"] + "" as string;
                }
                else
                {
                    //Response.Redirect("~/Errors/PageNotFound.aspx");
                    Response.Redirect("~/Menu/Home.aspx");
                }
            }

            MenuURL = Session["MenuURL"].ToString();
            MyOrderURL = Session["MyOrderURL"].ToString();
            UrlPara = Session["UrlPara"].ToString();
        }
        
    }
}