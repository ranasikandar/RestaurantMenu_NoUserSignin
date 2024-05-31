using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantMenu.Restaurant
{
    public partial class Orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            protectPage();
            if (!this.IsPostBack)
            {
                try
                {
                    //load ddls
                    DataTable dtPlaces = new BLL.Restaurants().GetRestaurantTables(" AND USR.Id=" + Session[new Helpers.SessionNames().SiteUserId].ToString()
                        + " ORDER BY TBL.Name");
                    DataRow row = dtPlaces.NewRow();
                    dtPlaces.Rows.InsertAt(row, 0);
                    dtPlaces.Rows[0]["Id"] = 0;
                    dtPlaces.Rows[0]["Name"] = "Select";
                    ddlPlaces.DataSource = dtPlaces;
                    ddlPlaces.DataBind();

                    DataTable dtProducts = new BLL.Restaurants().GetRestaurantProductsWithCatPri(" AND PRO.[Enable]=1 AND USR.Id=" + Session[new Helpers.SessionNames().SiteUserId].ToString()
                        + " ORDER BY PRO.Name");

                    foreach (DataRow dtrow in dtProducts.Rows)
                    {
                        dtrow["Name"] = dtrow["Name"] + " | " + dtrow["CategoryName"] + " | " + Convert.ToDouble(dtrow["Price"]).ToString();
                    }

                    ddlFoods.DataSource = dtProducts;
                    ddlFoods.DataBind();

                    //load dashboard
                    this.pnlDashboard.Controls.Add(new LiteralControl(getDashboardData()));
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                if (Request["__EVENTARGUMENT"] == "updateOrdList")
                {
                    this.pnlDashboard.Controls.Add(new LiteralControl(getDashboardData()));
                }
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
        protected void TimerDashboard_Tick(object sender, EventArgs e)
        {
            try
            {
                this.pnlDashboard.Controls.Add(new LiteralControl(getDashboardData()));
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private string getDashboardData()
        {
            string output="";
            string currencyCode="";

            try
            {
                Int32 restaurantId = new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]));
                DataTable dtTables = new BLL.Restaurants().GetTablesWithOrder(restaurantId);

                if (dtTables!=null&&dtTables.Rows.Count>0)
                {
                    output += "<div class='row widget mb-0'>";//Begin Widget*
                    //LEFT PAN START
                    output += "<div class='col-xl-4 col-md-4 no-padding' id='sidebar'>";//Begin Messages List
                    output += "<div class='sidebar-content w-100 h-100'>";
                    output += "<div id='list-group'>";//Begin List Group
                    output += "<ul class='messages-list list-group w-100 list-scroll auto-scroll'>";

                    foreach (DataRow row in dtTables.Rows)
                    {
                        output += "<li class='list-group-item'>";
                        output += "<a data-toggle='tab' href='#retbl-"+row["Id"].ToString()+"'>";
                        output += "<div class='media'>";
                        output += "<div class='media-left align-self-center'><img src='../Template/assets/img/avatar/table-chairs.png' class='user-img rounded-circle' alt='...' /></div>";
                        output += "<div class='media-body align-self-center'>";
                        output += "<div class='username'>";
                        output += "<h4>Table: "+row["Name"].ToString()+"</h4>";
                        output += "</div>";
                        output += "<div class='msg'><p>" + row["Discription"].ToString() + "</p></div>";
                        output += "</div>";
                        output += "<div class='media-right'><span class='date-send'>(" + row["Orders"].ToString() + ")</span></div>";//media-right align-self-center
                        output += "</div>";
                        output += "</a>";
                        output += "</li>";
                    }

                    output += "</ul>";
                    output += "</div>";//End List Group
                    output += "</div>";
                    output += "</div>";//End Messages List
                    //LEFT PAN END

                    //RIGHT PAN START
                    output += "<div class='col-xl-8 col-md-8 bg-mail d-flex no-padding'>";//Begin Messages
                    output += "<div class='card w-100 has-shadow'>";//Begin Card
                    output += "<div class='tab-content'>";//Begin Tab
                    
                    bool isFirstOrder = true;

                    foreach (DataRow row in dtTables.Rows)
                    {
                        if (isFirstOrder)
                        {
                            output += "<div class='tab-pane fade show active mail-scroll auto-scroll' id='retbl-"+row["Id"].ToString()+ "' > ";//Start    
                        }
                        else
                        {
                            output += "<div class='tab-pane fade mail-scroll auto-scroll' id='retbl-" + row["Id"].ToString() + "' >";//Start
                        }

                        output += "<div class='d-flex align-items-center mt-2 ml-2'><h3 class='page-header-title'>Table: " + row["Name"].ToString() + " Orders</h3></div>";//table name of orders
                        
                        isFirstOrder = false;
                        output += "<div class='card-header'>";//begin card header
                        ///
                        DataTable dtOrders = new BLL.Restaurants().GetOrdersOfTable(restaurantId, Convert.ToInt32(row["Id"].ToString()));
                        if (dtOrders != null && dtOrders.Rows.Count > 0)
                        {
                            foreach (DataRow row1 in dtOrders.Rows)
                            {
                                currencyCode = new BLL.Restaurants().GetCurrencyCode(restaurantId);

                                output += string.Format(@"<div id={0} class='row'>", "OrdDiv" + row1["OrderID"].ToString());//row start
                                output += "<div class='col-xl-10 col-sm-12 no-padding'>";//pan top start
                                output += string.Format(@"<div class='message-avatar'><a><img src='{0}' class='my-rounded-circle' alt='...' /></a></div>"
                                    , (!string.IsNullOrEmpty(row1["ImagePath"].ToString())) ? ResolveUrl("~/Reso/res/" + restaurantId + "/p/tsm/" + row1["ImagePath"].ToString() + ".jpg") : ResolveUrl("~/Template/assets/img/albums/01.jpg"));
                                output += "<div class='message-infos'>";
                                output += "<div class='user-title'>" + row1["Food"].ToString() + "</div>";
                                output += "<div class='show-details'>";
                                output += (!string.IsNullOrEmpty(row1["Discription"].ToString())) ? "<a>" + row1["Discription"].ToString() + "</a><br/>" : "";
                                output += "<a>Category: " + row1["CategoryName"].ToString() + "</a><br/>";
                                output += "<a>" + Convert.ToDouble(row1["Price"].ToString()) + " " + currencyCode + "</a><br/>";
                                output += "<a>Items: " + row1["Qty"].ToString() + "</a><br/>";
                                output += "<a>Note: " + row1["Note"].ToString() + "</a><br/>";
                                output += "<a data-toggle='collapse' href='#details-" + row1["OrderID"].ToString() + "' aria-expanded='false' aria-controls='details-" + row1["OrderID"].ToString() + "'>Details</a>";
                                
                                output += string.Format(@"<div class='collapse' id='details-{0}'><ul><li>Order Placed: {1}</li><li>By: {2}. Name: {3}. Email: {4}</li><li>Total Amount: {5}</li><li>Status: {6}</li></ul></div>"
                                    , row1["OrderID"].ToString()
                                    , new Helpers.Global().UTCtoEDT(Convert.ToDateTime(row1["OrderPlacedDTime"].ToString())).ToString("hh:mm tt dd-MMM")
                                    , row1["UserType"].ToString()
                                    , row1["Name"].ToString()
                                    , row1["Email"].ToString()
                                    , Convert.ToInt32(row1["Qty"].ToString()) * Convert.ToDouble(row1["Price"].ToString()) + " " + currencyCode
                                    , row1["Status"].ToString()
                                    );

                                output += "</div>";
                                output += "</div>";

                                output += "</div>";//pan top end

                                output += string.Format(@"<div class='col-xl-2 col-sm-12 no-padding d-flex justify-content-end'>
							<ul class='list-inline pt-3'>
								<li class='list-inline-item mr-3'>
									<a>
										<i id={2} class='{6}' data-toggle='tooltip' data-placement='bottom' title='' data-original-title='{7}' onclick={3}></i>
									</a>
								</li>
								<li class='list-inline-item mr-3'>
									<a>
										<i id={4} class='la la-money la-2x pointer' data-toggle='tooltip' data-placement='bottom' title='' data-original-title='Order Paid. it will not appear in orders list any more' onclick={5}></i>
									</a>
								</li>
								<li class='list-inline-item mr-3'>
									<a>
										<i id={0} class='la la-trash la-2x pointer' data-toggle='tooltip' data-placement='bottom' title='' data-original-title='Cancel Order. it will not appear in orders list any more' onclick={1}></i>
									</a>
								</li>
							</ul>
						</div>"
                                    , "CAOrd_" + row1["OrderID"].ToString()
                                    , "setCancelAccept('" + row1["OrderID"].ToString() + "')"
                               , "PreOrd_" + row1["OrderID"].ToString()
                               , "setPrepare('" + row1["OrderID"].ToString() + "')"
                               , "PaidOrd_" + row1["OrderID"].ToString()
                               , "setPaid('" + row1["OrderID"].ToString() + "')"
                               , (Convert.ToInt32(row1["StatusNameId"].ToString()) == 3) ? "la la-cutlery la-2x pointer" : "ion ion-bonfire la-2x pointer"
                               , (Convert.ToInt32(row1["StatusNameId"].ToString()) == 3) ? "Order UnPrepare and set as Placed" : "Order Prepared");//pan botom
                                
                                output += "<hr class='hrline' />";//line for next row
                                output += "</div>";//row End                                
                            }
                        }
                        else
                        {
                            output += string.Format(@"<div class='widget-body text-center'><h3 class='mt3 mb-3'>No Food was Ordered</h3></div>");
                        }
                        ///
                        output += "</div>";//End card header

                        output += string.Format(@"<div class='publisher publisher-multi bg-white'><div class='publisher-bottom d-flex justify-content-end'><h3>Total Foods: {0}. Items: {3}. Total Amount: {1} {2}.</h3></div></div>"
                            ,new BLL.Restaurants().CountMyOrders(restaurantId,Convert.ToInt32(row["Id"].ToString()))
                            , new BLL.Restaurants().GetMyOrdersTotalAmount(restaurantId, Convert.ToInt32(row["Id"].ToString()))
                            ,currencyCode
                            , new BLL.Restaurants().CountMyOrdersItems(restaurantId, Convert.ToInt32(row["Id"].ToString())));//FOOTER TOTAL

                        output += string.Format(@"<div class='publisher publisher-multi bg-white'><div class='publisher-bottom d-flex justify-content-end'>
                        <div class='btn btn-lg btn-gradient-02 mr-2' onclick={0}><span class='la la-money pointer mr-2'></span>Paid these orders</div>
	                    <!--<div class='btn btn-lg btn-gradient-02' onclick={1}><span class='la la-trash pointer mr-2'></span>Cancel these orders</div>-->
                        </div></div>", "paidOrders('" + row["Id"].ToString() + "')", "cancelOrders('" + row["Id"].ToString() + "')");//FOOTER BUTTONS

                        output += "</div>";//End
                    }

                    output += "</div>";//End Tab
                    output += "</div>";//End Card
                    output += "</div>";//End Messages
                    //RIGHT PAN END
                    output += "</div>";//End Widget*
                }
                else
                {
                    output += string.Format(@"<div class='widget-body text-center'><h3 class='mt3 mb-3'>None of the Tables Ordered anything</h3></div>");
                }

                return output;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        [System.Web.Services.WebMethod]
        public static string MarkCancel(string theId)
        {
            try
            {
                if (!string.IsNullOrEmpty(theId))
                {
                    if ((HttpContext.Current.Session[new Helpers.SessionNames().SiteUserType] != null) && (!string.IsNullOrEmpty(HttpContext.Current.Session[new Helpers.SessionNames().SiteUserId].ToString()) || HttpContext.Current.Session[new Helpers.SessionNames().SiteUserType].ToString() != "2"))
                    {
                        if (new BLL.Restaurants().MarkOrderStatus(Convert.ToInt32(theId),2,Convert.ToInt32(HttpContext.Current.Session[new Helpers.SessionNames().SiteUserId])))
                        {
                            return "Order has been Marked as Cancel. it will not appear in orders list any more";
                        }
                        else
                        {
                            return "Sorry this order was not Marked as Cancel. Please try again";
                        }
                    }
                    else
                    {
                        return "You dont Have Permissions";
                    }
                }
                else
                {
                    return "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                Helpers.WriteLogToFile.Write(ex);
                throw ex;
            }
        }
        
        [System.Web.Services.WebMethod]
        public static string MarkPrepare(string theId)
        {
            try
            {
                if (!string.IsNullOrEmpty(theId))
                {
                    if ((HttpContext.Current.Session[new Helpers.SessionNames().SiteUserType] != null) && (!string.IsNullOrEmpty(HttpContext.Current.Session[new Helpers.SessionNames().SiteUserId].ToString()) || HttpContext.Current.Session[new Helpers.SessionNames().SiteUserType].ToString() != "2"))
                    {
                        if (new BLL.Restaurants().MarkOrderStatus(Convert.ToInt32(theId), 3, Convert.ToInt32(HttpContext.Current.Session[new Helpers.SessionNames().SiteUserId])))
                        {
                            return "Order has been Marked as Prepare";
                        }
                        else
                        {
                            return "Sorry this order was not Marked as Prepare. Please try again";
                        }
                    }
                    else
                    {
                        return "You dont Have Permissions";
                    }
                }
                else
                {
                    return "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                Helpers.WriteLogToFile.Write(ex);
                throw ex;
            }
        }

        [System.Web.Services.WebMethod]
        public static string MarkUnPrepare(string theId)
        {
            try
            {
                if (!string.IsNullOrEmpty(theId))
                {
                    if ((HttpContext.Current.Session[new Helpers.SessionNames().SiteUserType] != null) && (!string.IsNullOrEmpty(HttpContext.Current.Session[new Helpers.SessionNames().SiteUserId].ToString()) || HttpContext.Current.Session[new Helpers.SessionNames().SiteUserType].ToString() != "2"))
                    {
                        if (new BLL.Restaurants().MarkOrderStatus(Convert.ToInt32(theId), 1, Convert.ToInt32(HttpContext.Current.Session[new Helpers.SessionNames().SiteUserId])))
                        {
                            return "Order has been Marked as UnPrepare and as Placed";
                        }
                        else
                        {
                            return "Sorry this order was not Marked as UnPrepare. Please try again";
                        }
                    }
                    else
                    {
                        return "You dont Have Permissions";
                    }
                }
                else
                {
                    return "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                Helpers.WriteLogToFile.Write(ex);
                throw ex;
            }
        }

        [System.Web.Services.WebMethod]
        public static string MarkPaid(string theId)
        {
            try
            {
                if (!string.IsNullOrEmpty(theId))
                {
                    if ((HttpContext.Current.Session[new Helpers.SessionNames().SiteUserType] != null) && (!string.IsNullOrEmpty(HttpContext.Current.Session[new Helpers.SessionNames().SiteUserId].ToString()) || HttpContext.Current.Session[new Helpers.SessionNames().SiteUserType].ToString() != "2"))
                    {
                        if (new BLL.Restaurants().MarkOrderPaidUnPaid(Convert.ToInt32(theId), true, Convert.ToInt32(HttpContext.Current.Session[new Helpers.SessionNames().SiteUserId])))
                        {
                            return "Order has been Marked as Paid. it will not appear in orders list any more";
                        }
                        else
                        {
                            return "Sorry this order was not Marked as Paid. Please try again";
                        }
                    }
                    else
                    {
                        return "You dont Have Permissions";
                    }
                }
                else
                {
                    return "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                Helpers.WriteLogToFile.Write(ex);
                throw ex;
            }
        }

        [System.Web.Services.WebMethod]
        public static string PaidOrders(string theId)
        {
            try
            {
                if (!string.IsNullOrEmpty(theId))
                {
                    if ((HttpContext.Current.Session[new Helpers.SessionNames().SiteUserType] != null) && (!string.IsNullOrEmpty(HttpContext.Current.Session[new Helpers.SessionNames().SiteUserId].ToString()) || HttpContext.Current.Session[new Helpers.SessionNames().SiteUserType].ToString() != "2"))
                    {
                        if (new BLL.Restaurants().MarkOrdersPaid(Convert.ToInt32(theId), new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(HttpContext.Current.Session[new Helpers.SessionNames().SiteUserId])), Convert.ToInt32(HttpContext.Current.Session[new Helpers.SessionNames().SiteUserId])))
                        {
                            return "Orders have been Marked as Paid. these will not appear in orders list any more";
                        }
                        else
                        {
                            return "Sorry these order were not Marked as Paid. Please try again";
                        }
                    }
                    else
                    {
                        return "You dont Have Permissions";
                    }
                }
                else
                {
                    return "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                Helpers.WriteLogToFile.Write(ex);
                throw ex;
            }
        }
        [System.Web.Services.WebMethod]
        public static string PlaceOrder(string ordDetail)
        {
            try
            {
                if (!string.IsNullOrEmpty(ordDetail))
                {
                    if ((HttpContext.Current.Session[new Helpers.SessionNames().SiteUserType] != null) && (!string.IsNullOrEmpty(HttpContext.Current.Session[new Helpers.SessionNames().SiteUserId].ToString()) || HttpContext.Current.Session[new Helpers.SessionNames().SiteUserType].ToString() != "2"))
                    {
                        string[] _ordDetails = ordDetail.Split('_');//tbl,food,qty,note
                        if (_ordDetails.Length==4)
                        {
                            Int32 restId = new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(HttpContext.Current.Session[new Helpers.SessionNames().SiteUserId]));
                            if (new BLL.Restaurants().PlaceAnOrder(restId,Convert.ToInt32(_ordDetails[0]), Convert.ToInt32(_ordDetails[1]), Convert.ToInt32(_ordDetails[2]), Convert.ToInt32(HttpContext.Current.Session[new Helpers.SessionNames().SiteUserId]), _ordDetails[3]))
                            {
                                Task.Run(() => sendEmail(_ordDetails, restId));
                                return "Order has been Placed Successfully";
                            }
                            else
                            {
                                return "Sorry this order was not Placed. Please try again";
                            }
                        }
                        else
                        {
                            return "Invalid Request";
                        }
                        
                    }
                    else
                    {
                        return "You dont Have Permissions";
                    }
                }
                else
                {
                    return "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                Helpers.WriteLogToFile.Write(ex);
                throw ex;
            }
        }

        [System.Web.Services.WebMethod]
        public static string CancelOrders(string theId)
        {
            try
            {
                if (!string.IsNullOrEmpty(theId))
                {
                    if ((HttpContext.Current.Session[new Helpers.SessionNames().SiteUserType] != null) && (!string.IsNullOrEmpty(HttpContext.Current.Session[new Helpers.SessionNames().SiteUserId].ToString()) || HttpContext.Current.Session[new Helpers.SessionNames().SiteUserType].ToString() != "2"))
                    {
                        if (new BLL.Restaurants().MarkOrdersCancel(Convert.ToInt32(theId), new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(HttpContext.Current.Session[new Helpers.SessionNames().SiteUserId])), Convert.ToInt32(HttpContext.Current.Session[new Helpers.SessionNames().SiteUserId])))
                        {
                            return "Orders have been Marked as Cancel. these will not appear in orders list any more";
                        }
                        else
                        {
                            return "Sorry these orders were not Marked as Cancel. Please try again";
                        }
                    }
                    else
                    {
                        return "You dont Have Permissions";
                    }
                }
                else
                {
                    return "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                Helpers.WriteLogToFile.Write(ex);
                throw ex;
            }
        }

        [System.Web.Services.WebMethod]
        public static string MarkUnPaid(string theId)
        {
            return "This feature is Currently unavailable";
            //try
            //{
            //    if (!string.IsNullOrEmpty(theId))
            //    {
            //        if ((HttpContext.Current.Session[new Helpers.SessionNames().SiteUserType] != null) && (!string.IsNullOrEmpty(HttpContext.Current.Session[new Helpers.SessionNames().SiteUserId].ToString()) || HttpContext.Current.Session[new Helpers.SessionNames().SiteUserType].ToString() != "2"))
            //        {
            //            if (new BLL.Restaurants().MarkOrderPaidUnPaid(Convert.ToInt32(theId), false, Convert.ToInt32(HttpContext.Current.Session[new Helpers.SessionNames().SiteUserId])))
            //            {
            //                return "Order has been Marked as UnPaid. it will not appear in orders list any more";
            //            }
            //            else
            //            {
            //                return "Sorry this order was not Marked as UnPaid. Please try again";
            //            }
            //        }
            //        else
            //        {
            //            return "You dont Have Permissions";
            //        }
            //    }
            //    else
            //    {
            //        return "Invalid Request";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Helpers.WriteLogToFile.Write(ex);
            //    throw ex;
            //}
        }

        [System.Web.Services.WebMethod]
        public static string MarkUnCancel(string theId)
        {
            return "This feature is Currently unavailable";
            //try
            //{
            //    if (!string.IsNullOrEmpty(theId))
            //    {
            //        if ((HttpContext.Current.Session[new Helpers.SessionNames().SiteUserType] != null) && (!string.IsNullOrEmpty(HttpContext.Current.Session[new Helpers.SessionNames().SiteUserId].ToString()) || HttpContext.Current.Session[new Helpers.SessionNames().SiteUserType].ToString() != "2"))
            //        {
            //            if (new BLL.Restaurants().MarkOrderStatus(Convert.ToInt32(theId), 1, Convert.ToInt32(HttpContext.Current.Session[new Helpers.SessionNames().SiteUserId])))
            //            {
            //                return "Order has been Marked as UnCancel and as Placed";
            //            }
            //            else
            //            {
            //                return "Sorry this order was not Marked as UnCancel. Please try again";
            //            }
            //        }
            //        else
            //        {
            //            return "You dont Have Permissions";
            //        }
            //    }
            //    else
            //    {
            //        return "Invalid Request";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Helpers.WriteLogToFile.Write(ex);
            //    throw ex;
            //}
        }

        private static void sendEmail(string[] param, Int32 restId)
        {
            //theId=tbl,food,qty,note

            try
            {
                DataTable dt = new BLL.Restaurants().GetRestaurants(restId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dt.Rows[0]["SendEmailNotification"]) && !string.IsNullOrEmpty(dt.Rows[0]["NotificationEmail"].ToString()))
                    {
                        DataTable dtProduct = new BLL.Restaurants().GetRestaurantProducts("AND PRO.RestaurantId=" + restId + " AND PRO.Id=" + param[1] + "");

                        string emailBody = string.Format(@"New Order: Table: {0}. Food: {1}. Items: {2}. Note: {3}"
                        , new BLL.Restaurants().GetTableName(restId, Convert.ToInt32(param[0]))
                        , dtProduct.Rows[0]["Name"].ToString()
                        , Convert.ToInt32(param[2])
                        , param[3]);

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