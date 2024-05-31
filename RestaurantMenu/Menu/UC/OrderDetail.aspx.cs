using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantMenu.Menu.UC
{
    public partial class OrderDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProtectPage();
            if (!this.IsPostBack)
            {
                if (Request.QueryString["order"] == "")
                {
                    Response.Redirect("~/Errors/InvalidRequest.aspx");
                }
                else
                {
                    theID.Value = Request.QueryString["order"].ToString();
                }
            }
        }
        private void ProtectPage()
        {
            //session check
            if (Session[new Helpers.SessionNames().SiteUserId] == null || string.IsNullOrEmpty(Session[new Helpers.SessionNames().SiteUserId].ToString()))
            {
                Response.Redirect("~/Errors/InvalidRequest.aspx");
            }
            else
            {
                if (Session[new Helpers.SessionNames().SiteUserType] == null)
                {
                    Response.Redirect("~/Errors/InvalidRequest.aspx");
                }
                else
                {
                    //if (Session[new Helpers.SessionNames().SiteUserType].ToString() != "1")
                    //{
                    //    redirectUserType(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserType]));
                    //}
                    ////user have session
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(theID.Value)&&!Regex.IsMatch(txtQty.Text, @"[a-zA-Z]"))
            {
                //orderPara=RestaurantId,TableId,ProductId
                //for aditional protection check if qr code match at this post webmethod
                //if (restaurantTblIdValid(Convert.ToInt32(Request.QueryString["res"]), Request.QueryString["tbl"]))

                try
                {
                    if (!string.IsNullOrEmpty(txtQty.Text))
                    {
                        string orderPara = theID.Value;

                        if (!string.IsNullOrEmpty(orderPara))
                        {
                            if (orderPara.Split('_').Length - 1 == 2)
                            {
                                string[] para = orderPara.Split('_');
                                if (new BLL.Restaurants().PlaceAnOrder(Convert.ToInt32(para[0]), Convert.ToInt32(para[1]), Convert.ToInt32(para[2]), Convert.ToInt32(txtQty.Text), Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]), txtNote.Text))
                                {
                                    Task.Run(() => sendEmail(para, Convert.ToInt32(txtQty.Text),txtNote.Text));
                                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('error','Your Order has been Placed Successfully','center',5000);", true);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "parent.clearEditIfram", "parent.clearEditIfram('Your Order has been Placed Successfully');", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('error','Sorry your order was not Placed. Please try again.','center',5000);", true);
                                }
                            }
                            else
                            {
                                Response.Redirect("~/Errors/InvalidRequest.aspx");
                            }
                        }
                        else
                        {
                            Response.Redirect("~/Errors/InvalidRequest.aspx");
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('error','Please Enter Valid Quantity','center',5000);", true);
                    }
                }
                catch (Exception ex)
                {
                    Helpers.WriteLogToFile.Write(ex);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('error','Sorry your order was not Placed. Please try again.','center',5000);", true);
                }
            }
            else
            {
                Response.Redirect("~/Errors/InvalidRequest.aspx");
            }
        }
        private static void sendEmail(string[] param,Int32 qty,string note)
        {
            //theId=RestaurantId,TableId,ProductId,qty

            try
            {
                DataTable dt = new BLL.Restaurants().GetRestaurants(Convert.ToInt32(param[0]));
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dt.Rows[0]["SendEmailNotification"]) && !string.IsNullOrEmpty(dt.Rows[0]["NotificationEmail"].ToString()))
                    {
                        string emailBody;
                        
                        if (string.IsNullOrEmpty(dt.Rows[0]["ValidityDate"].ToString())||Convert.ToDateTime(dt.Rows[0]["ValidityDate"].ToString())<DateTime.UtcNow)
                        {
                            emailBody = "Sorry your restaurant services are Temporary suspended Please pay your fee.";
                        }
                        else
                        {
                            DataTable dtProduct = new BLL.Restaurants().GetRestaurantProducts("AND PRO.RestaurantId=" + param[0] + " AND PRO.Id=" + param[2] + "");

                            emailBody = string.Format(@"New Order: Table: {0}. Food: {1}. Items: {2}. Note: {3}"
                            , new BLL.Restaurants().GetTableName(Convert.ToInt32(param[0]), Convert.ToInt32(param[1]))
                            , dtProduct.Rows[0]["Name"].ToString()
                            , qty
                            , note);
                        }

                        Helpers.Email.Send(ConfigurationManager.AppSettings["AppEmail"].ToString(), ConfigurationManager.AppSettings["BusinessName"].ToString(), dt.Rows[0]["NotificationEmail"].ToString(), dt.Rows[0]["Name"].ToString(), "New Order Alert For Restaurant " + dt.Rows[0]["Name"].ToString(), emailBody, false, null);
                    }
                }
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}