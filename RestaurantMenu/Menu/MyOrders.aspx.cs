using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantMenu.Menu
{
    public partial class MyOrders : System.Web.UI.Page
    {
        public static string TotalFoods = "None";
        public static string TotalFoodsItems = "None";
        public static string RestaurantName = "Restaurant";
        public static string MenuURL;
        public static string TableName = "Table";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["res"]) && !string.IsNullOrEmpty(Request.QueryString["tbl"]))
                {
                    Session["MenuURL"] = "~/Menu/Order.aspx?res=" + Request.QueryString["res"] + "&tbl=" + Request.QueryString["tbl"] + "" as string;
                    Session["MyOrderURL"] = "~/Menu/MyOrders.aspx?res=" + Request.QueryString["res"] + "&tbl=" + Request.QueryString["tbl"] + "" as string;
                    Session["UrlPara"] = "?res=" + Request.QueryString["res"] + "&tbl=" + Request.QueryString["tbl"] + "" as string;

                    try
                    {
                        if (restaurantTblIdValid(Convert.ToInt32(Request.QueryString["res"]), Request.QueryString["tbl"]))
                        {
                            getOrderList();
                        }
                        else
                        {
                            //Response.Redirect("~/Errors/PageNotFound.aspx");
                            Response.Redirect("~/Menu/Home.aspx");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
                else
                {
                    //Response.Redirect("~/Errors/PageNotFound.aspx");
                    Response.Redirect("~/Menu/Home.aspx");
                }
                //DO WHATEVER ITS NOT A POSTBACK
                MenuURL = Session["MenuURL"].ToString();
            }
            else
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
                //DO WHATEVER AND ITS POSTBACK
                MenuURL = Session["MenuURL"].ToString();
                
            }
        }

        private void getOrderList()
        {
            Int32 tblID = new BLL.Restaurants().GetTableId(Convert.ToInt32(Request.QueryString["res"]), new Helpers.Global().RemoveSpecialCharacters(Request.QueryString["tbl"]));
            TotalFoods = new BLL.Restaurants().CountMyOrders(Convert.ToInt32(Request.QueryString["res"]), tblID).ToString();
            TotalFoodsItems = new BLL.Restaurants().CountMyOrdersItems(Convert.ToInt32(Request.QueryString["res"]), tblID).ToString();
            RestaurantName = new BLL.Restaurants().GetRestaurantName(new Helpers.Global().RemoveSpecialCharacters(Request.QueryString["res"]));
            TableName = new BLL.Restaurants().GetTableName(Convert.ToInt32(Request.QueryString["res"]), tblID);

            this.pnlDashboard.Controls.Add(new LiteralControl(getDashboardData(tblID)));
        }

        private bool restaurantTblIdValid(Int32 res, string tbl)
        {
            try
            {
                bool? _tmp = new BLL.Restaurants().RestaurantTblAndIdIsValid(res, tbl);
                if (_tmp != null && _tmp == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string getDashboardData(Int32 tblID)
        {
            string htmlStr = "";
            string currencyCode = "";
            double? totalAmount;
            try
            {
                DataTable dt = new BLL.Restaurants().GetMyOrders(Convert.ToInt32(Request.QueryString["res"]), tblID);

                if ((dt != null && dt.Rows.Count > 0) && tblID > 0)
                {
                    currencyCode = new BLL.Restaurants().GetCurrencyCode(Convert.ToInt32(Request.QueryString["res"]));
                    totalAmount = new BLL.Restaurants().GetMyOrdersTotalAmount(Convert.ToInt32(Request.QueryString["res"]), tblID);

                    htmlStr += "<div class='row'>";//begin 1div

                    foreach (DataRow row in dt.Rows)
                    {
                        htmlStr += "<div class='col-xl-3 col-md-4 col-sm-6' id='ord_"+row["Id"]+"'>";//begin product
                        htmlStr += "<div class='widget has-shadow'>";
                        htmlStr += "<figure class='img-hover-01'>";
                        htmlStr += string.Format(@"<img src='{0}' class='img-fluid' alt='...'/>"
                            , (string.IsNullOrEmpty(row["ImagePath"].ToString())) ? ResolveUrl("~/Template/assets/img/albums/01.jpg") : ResolveUrl("~/Reso/res/" + Convert.ToInt32(Request.QueryString["res"]) + "/p/tsm/" + row["ImagePath"].ToString() + ".jpg"));
                        htmlStr += "<div><a><span>Ordered: " + new Helpers.Global().UTCtoEDT(Convert.ToDateTime(row["OrderPlacedDTime"].ToString())).ToString("hh:mmtt dd-MMM") + "</span></a></div>";
                        htmlStr += "</figure>";
                        htmlStr += "<div class='widget-body text-center'>";
                        htmlStr += "<h3 class='mt-3 mb-1'>" + row["Name"].ToString() + "</h3>";
                        htmlStr += "<h6 class='mb-2'>" + row["Discription"].ToString() + "</h6>";
                        htmlStr += "<a>" + Convert.ToDouble(row["Price"].ToString()) + " " + currencyCode + "</a>";
                        htmlStr += "<hr class='hrline' />";
                        htmlStr += "<a>" + row["Qty"].ToString() + " items. Total: " + Convert.ToDouble(row["Price"].ToString()) * Convert.ToInt32(row["Qty"].ToString())+ " "+currencyCode+"</a>";

                        htmlStr += "<br><a><span>Order By: " + row["OrderByName"].ToString() + "</span></a>";
                        
                        htmlStr += "</div>";
                        htmlStr += "</div>";
                        htmlStr += "</div>";//end product
                    }

                    htmlStr += "</div>";//end 1div

                    if (totalAmount != null && totalAmount > 0)
                    {
                        htmlStr += string.Format(@"
                            <div class='row align-items-center mt-3 mb-4'>
                                <div class='col-10'>
                                    <h4 class='m-0'>Total Amount: {0} {1}</h4>
                                </div>
                            </div>", totalAmount, currencyCode);
                    }


                }
                else
                {
                    htmlStr += string.Format(@"<div class='widget-body text-center'><h3 class='mt3 mb-3'>No Order Available From your Table</h3></div>");
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return htmlStr;
        }
        
        protected void TimerDashboard_Tick(object sender, EventArgs e)
        {
            try
            {
                Int32 tblID = new BLL.Restaurants().GetTableId(Convert.ToInt32(Request.QueryString["res"]), new Helpers.Global().RemoveSpecialCharacters(Request.QueryString["tbl"]));
                this.pnlDashboard.Controls.Add(new LiteralControl(getDashboardData(tblID)));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}