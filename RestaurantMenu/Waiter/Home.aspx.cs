using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace RestaurantMenu.Waiter
{
    public partial class Home : System.Web.UI.Page
    {
        public string RestaurantName;
        protected void Page_Load(object sender, EventArgs e)
        {
            protectPage();
            try
            {
                if (!this.IsPostBack)
                {
                    DataTable dtPlaces = new BLL.Restaurants().GetRestaurantWaiterTables(" AND USR.Id=" + Session[new Helpers.SessionNames().SiteUserId].ToString()
                        + " ORDER BY TBL.Name");
                    DataRow row = dtPlaces.NewRow();
                    dtPlaces.Rows.InsertAt(row, 0);
                    dtPlaces.Rows[0]["Id"] = 0;
                    dtPlaces.Rows[0]["Name"] = "Select";
                    ddlPlaces.DataSource = dtPlaces;
                    ddlPlaces.DataBind();

                    Int32 waiterRestaurantId= new BLL.Restaurants().GetWaiterRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]));

                    DataTable dtProducts = new BLL.Restaurants().GetRestaurantWaiterProducts(" AND PRO.[Enable]=1 AND PRO.RestaurantId=" + waiterRestaurantId
                        + " ORDER BY PRO.Name");

                    foreach (DataRow dtrow in dtProducts.Rows)
                    {
                        dtrow["Name"] = dtrow["Name"] + " | " + dtrow["CategoryName"] + " | " + Convert.ToDouble(dtrow["Price"]).ToString();
                    }

                    ddlFoods.DataSource = dtProducts;
                    ddlFoods.DataBind();

                    hidResId.Value = waiterRestaurantId.ToString();
                }
                else
                {
                    //DO WHATEVER AND ITS POSTBACK
                    if (Request["__EVENTARGUMENT"] == "updateOrdList")
                    {
                        getOrderList();
                    }
                }
                RestaurantName = getRestaurantName();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void getOrderList()
        {
            this.pnlDashboard.Controls.Add(new LiteralControl(getDashboardData(Convert.ToInt32(ddlPlaces.SelectedValue))));
        }
        protected void ddlPlaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pnlDashboard.Controls.Add(new LiteralControl(getDashboardData(Convert.ToInt32(ddlPlaces.SelectedValue))));
        }
        private string getDashboardData(Int32 tblID)
        {
            string htmlStr = "";
            string currencyCode = "";
            double? totalAmount;
            try
            {
                Int32 restId = new BLL.Restaurants().GetWaiterRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId].ToString()));

                DataTable dt = new BLL.Restaurants().GetMyOrders(restId, tblID);

                if ((dt != null && dt.Rows.Count > 0) && tblID > 0)
                {
                    currencyCode = new BLL.Restaurants().GetCurrencyCode(restId);
                    totalAmount = new BLL.Restaurants().GetMyOrdersTotalAmount(restId, tblID);

                    htmlStr += "<div class='row'>";//begin 1div

                    foreach (DataRow row in dt.Rows)
                    {
                        htmlStr += "<div class='col-xl-3 col-md-4 col-sm-6' id='ord_" + row["Id"] + "'>";//begin product
                        htmlStr += "<div class='widget has-shadow'>";
                        htmlStr += "<figure class='img-hover-01'>";
                        htmlStr += string.Format(@"<img src='{0}' class='img-fluid' alt='...'/>"
                            , (string.IsNullOrEmpty(row["ImagePath"].ToString())) ? ResolveUrl("~/Template/assets/img/albums/01.jpg") : ResolveUrl("~/Reso/res/" + restId + "/p/tsm/" + row["ImagePath"].ToString() + ".jpg"));
                        htmlStr += "<div><a><span>Ordered: " + new Helpers.Global().UTCtoEDT(Convert.ToDateTime(row["OrderPlacedDTime"].ToString())).ToString("hh:mmtt dd-MMM") + "</span></a></div>";
                        htmlStr += "</figure>";
                        if (Convert.ToInt32(row["StatusNameId"].ToString())==3)
                        {
                            htmlStr += "<div class='widget-body text-center' style='background-color:#bdffbd;'>";//color for prepared foods
                        }
                        else
                        {
                            htmlStr += "<div class='widget-body text-center'>";//color for prepared foods
                        }
                        htmlStr += "<h3 class='mt-3 mb-1'>" + row["Name"].ToString() + "</h3>";
                        htmlStr += "<h6 class='mb-2'>" + row["Discription"].ToString() + "</h6>";
                        htmlStr += "<a>" + Convert.ToDouble(row["Price"].ToString()) + " " + currencyCode + "</a>";
                        htmlStr += "<hr class='hrline' />";
                        if (row["Note"]!=null&&!string.IsNullOrEmpty(row["Note"].ToString()))
                        {
                            htmlStr += "<a>" + "Note: " + row["Note"].ToString() + "</a>";
                            htmlStr += "<hr class='hrline' />";
                        }
                        htmlStr += "<a>" + row["Qty"].ToString() + " items. Total: " + Convert.ToDouble(row["Price"].ToString()) * Convert.ToInt32(row["Qty"].ToString()) + " " + currencyCode + "</a>";
                        htmlStr += "<br><a><span>Order By: " + row["OrderByName"].ToString() + "</span></a>";
                        if (row["OrderById"].ToString() == Session[new Helpers.SessionNames().SiteUserId].ToString() && row["StatusNameId"].ToString() == "1")
                        {
                            htmlStr += string.Format(@"<br><a class='btn btn-danger btclr mt-2' onclick={0}>Cancel</a>", "CancelMyOrder('" + row["Id"].ToString() + "')");
                        }

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
                    htmlStr += string.Format(@"<div class='widget-body text-center'><h3 class='mt3 mb-3'>No Order From this Place or Table</h3></div>");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return htmlStr;
        }

        [System.Web.Services.WebMethod]
        public static string MarkCancel(string theId)
        {
            if (!string.IsNullOrEmpty(theId))
            {
                try
                {
                    DataTable dtOrder = new BLL.Restaurants().GetOrder(Convert.ToInt32(theId));
                    if (dtOrder != null && dtOrder.Rows.Count > 0)
                    {
                        //CHECK IF USER IS SAME WHO ORDER THIS FOOD // CHECK IF ITS STATUS IS VALID FOR CANCELLATION
                        if ((!string.IsNullOrEmpty(HttpContext.Current.Session[new Helpers.SessionNames().SiteUserId].ToString())) && (HttpContext.Current.Session[new Helpers.SessionNames().SiteUserType] != null))//|| HttpContext.Current.Session[new Helpers.SessionNames().SiteUserType].ToString() != "1"
                        {
                            if (dtOrder.Rows[0]["OrderBy"].ToString() == HttpContext.Current.Session[new Helpers.SessionNames().SiteUserId].ToString())
                            {
                                if (dtOrder.Rows[0]["StatusNameId"].ToString() == "1")
                                {
                                    if (new BLL.Restaurants().MarkOrderStatus(Convert.ToInt32(theId), 6, Convert.ToInt32(HttpContext.Current.Session[new Helpers.SessionNames().SiteUserId])))
                                    {
                                        return "This Order has been Cancelled by Waiter";
                                    }
                                    else
                                    {
                                        return "This Order Status was not changed Please try again";
                                    }
                                }
                                else
                                {
                                    switch (Convert.ToInt32(dtOrder.Rows[0]["StatusNameId"]))
                                    {
                                        case 2:
                                            return "Order already has been cancelled by Restaurant";
                                        case 3:
                                            return "Order has been Processed can not be cancelled";
                                        case 4:
                                            return "Order has been Paid can not be cancelled";
                                        case 5:
                                            return "Order already has been Cancelled by Client";
                                        case 6:
                                            return "Order already has been Cancelled by Waiter";
                                        default:
                                            return "Invalid Order Status";
                                    }
                                }
                            }
                            else
                            {
                                return "You dont Have Permission";
                            }
                        }
                        else
                        {
                            return "You dont Have Permission";
                        }
                    }
                    else
                    {
                        return "Nothing Found";
                    }
                }
                catch (Exception ex)
                {
                    Helpers.WriteLogToFile.Write(ex);
                    return "This Order Status was not changed Please try again";
                }
            }
            else
            {
                return "Invalid Request";
            }
        }
        private string getRestaurantName()
        {
            try
            {
                string _restName = new BLL.Restaurants().GetRestaurantNameWaiter(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]));
                return (string.IsNullOrEmpty(_restName)) ? "Restaurant" : _restName;
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
        }
    }
}