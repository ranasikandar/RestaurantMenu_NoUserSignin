using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantMenu.Restaurant
{
    public partial class Home : System.Web.UI.Page
    {
        public string importantMsg = "";//email verification, validity expire reminders
        protected void Page_Load(object sender, EventArgs e)
        {
            protectPage();
            try
            {
                if (!this.IsPostBack)
                {
                    if (!new BLL.Users().RestaurantEmailVerified("AND Id=" + Session[new Helpers.SessionNames().SiteUserId] + ""))
                    {
                        importantMsg += "<script>alertMeBottom('notification', 'Please Verifiy your Email', 'bottom');</script>";
                    }

                    //check if validity is near to expire show important msg
                    string validityPeriod = new BLL.Restaurants().GetValidityDate(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]));
                    if (!string.IsNullOrEmpty(validityPeriod))
                    {
                        Int32 expIn = Convert.ToDateTime(validityPeriod).Subtract(DateTime.UtcNow).Days;

                        if (Convert.ToInt32(ConfigurationManager.AppSettings["WarnOwnerValidityExpBeforeDay"].ToString()) >= (expIn + 1))
                        {
                            importantMsg += "<script>alertMeBottom('notification', 'Your Subscription Period is going to Expire soon. its valid for " + (expIn + 1) + " Days', 'bottom');</script>";
                        }
                    }
                    //load dashboard
                    this.pnlDashboard.Controls.Add(new LiteralControl(getDashboardData()));
                }
                else
                {
                    //its not page load
                }
            }
            catch (Exception ex)
            {

                throw ex;
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
            string dashboardData;
            Int32 restaurantId;
            string restaurantName;
            Int32 orderToPrepare;
            Int32 orderPrepared;
            Int32 unPaidOrders;
            Int32 todayCompletedOrders;
            Int32 todayCancelledOrders;
            double? todaySale;

            try
            {
                restaurantId = new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]));
                restaurantName = new BLL.Restaurants().GetRestaurantName(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]));
                orderToPrepare = new BLL.Restaurants().CountOrdersWithStatus(1, restaurantId);
                orderPrepared = new BLL.Restaurants().CountOrdersWithStatus(3, restaurantId);
                unPaidOrders = new BLL.Restaurants().CountUnPaidOrders(restaurantId);
                todayCompletedOrders = new BLL.Restaurants().CountTodayCompletedOrders(restaurantId);
                todayCancelledOrders = new BLL.Restaurants().CountTodayCancelledOrders(restaurantId);
                todaySale = new BLL.Restaurants().TodayTotalSale(restaurantId);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            try
            {
                dashboardData = string.Format(@"

<div class='row flex-row'>
    <div class='col-xl-12 col-md-12 os-animation' data-os-animation='fadeInUp'>
        <div class='widget widget-05 has-shadow'>
            <div class='widget-body no-padding hidden'>
                
				<div class='author-avatar'>
                    <img src='{0}' alt='...' class='img-fluid rounded-circle' />
                </div>
				
                <div class='author-name'>{1}
                <span>{2}</span>
                </div>
				
                <div class='social-stats'>
                    <div class='row d-flex justify-content-between'>
                        <div class='col-4 text-center'>
                            <i class='ion ion-bonfire followers'></i>
                            <div class='counter'>{3}</div>
                            <div class='heading'>{4}</div>
                        </div>
                        <div class='col-4 text-center'>
                            <i class='la la-cutlery followers'></i>
                            <div class='counter'>{5}</div>
                            <div class='heading'>{6}</div>
                        </div>
                        <div class='col-4 text-center'>
                            <i class='la la-money followers'></i>
                            <div class='counter'>{7}</div>
                            <div class='heading'>{8}</div>
                        </div>
                    </div>
                </div>

                <div class='author-name'>
                    <span>Order Achievements</span>
                </div>

                <div class='social-stats'>
                    <div class='row d-flex justify-content-between'>
                        <div class='col-4 text-center'>
                            <i class='la la-trophy followers'></i>
                            <div class='counter'>{9}</div>
                            <div class='heading'>{10}</div>
                        </div>
                        <div class='col-4 text-center'>
                            <i class='la la-trash followers'></i>
                            <div class='counter'>{11}</div>
                            <div class='heading'>{12}</div>
                        </div>
                        <div class='col-4 text-center'>
                            <i class='la la-euro followers'></i>
                            <div class='counter'>{13}</div>
                            <div class='heading'>{14}</div>
                        </div>
                    </div>
                </div>
				
            </div>
        </div>
    </div>

</div>

{15}

", ResolveUrl("~/Template/assets/img/avatar/avatar-03.jpg")
 , (string.IsNullOrEmpty(restaurantName) ? "Name Not Provided Yet" : restaurantName)//restaurant name
 , "Welcome"
 , (orderToPrepare>0)?orderToPrepare.ToString():"None"
 , "Orders to Prepare"
 , (orderPrepared>0)?orderPrepared.ToString():"None"
 , "Orders Prepared"
 , (unPaidOrders>0)?unPaidOrders.ToString():"None"
 , "UnPaid Orders"
 , (todayCompletedOrders > 0) ? todayCompletedOrders.ToString(): "None"
 , "Today Order Completed"
 , (todayCancelledOrders>0)?todayCancelledOrders.ToString():"None"
 , "Today Order Cancelled"
 , (todaySale!=null)?todaySale.ToString():"None"
 , "Today Sale"
 , getLog());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dashboardData;
        }
        private string getLog()
        {
            try
            {
                string query = string.Format(@" AND USR.Id={0} ORDER BY USL.LogDateTime DESC",Session[new Helpers.SessionNames().SiteUserId].ToString());

                DataTable dtLog = new BLL.Users().GetLog("TOP(200)", query);
                if (dtLog != null && dtLog.Rows.Count > 0)
                {
                    string HTMLstring = "<div class='row flex-row'>";//<!-- start Row -->
                    HTMLstring += "<div class='col-xl-12 os-animation' data-os-animation='fadeInUp'>";
                    HTMLstring += "<div class='widget widget-06 has-shadow'>";
                    HTMLstring += "<div class='widget-header bordered d-flex align-items-center'><h3>Recent Logs</h3></div>";

                    HTMLstring += string.Format(@"<div id='list-group' class='widget-scroll' style='max-height:490px;'>");
                    HTMLstring += string.Format(@"<ul class='reviews list-group w-100'>");

                    foreach (DataRow row in dtLog.Rows)
                    {
                        ///////////////////////////////////////////////
                        HTMLstring += "<li class='list-group-item'>";
                        HTMLstring += "<div class='media'>";
                        HTMLstring += "<div class='media-body align-self-center'>";

                        HTMLstring += "<div class='username'>";
                        HTMLstring += "<h4>" + row["LogDetail"].ToString() + "</h4>";
                        HTMLstring += "</div>";

                        HTMLstring += "<div class='meta'>";
                        HTMLstring += "<span class='mr-3'>" + new Helpers.Global().UTCtoEDT(Convert.ToDateTime(row["LogDateTime"])).ToString("hh:mmtt dd-MMM-yy") + "</span>";
                        HTMLstring += "</div></div></div></li>";
                        ///////////////////////////////////////////////
                    }

                    HTMLstring += "</ul>";
                    HTMLstring += "</div>";

                    HTMLstring += "</div></div></div>";//<!-- end Row -->

                    return HTMLstring;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('error','Sorry Nothing found from log','center',5000);", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
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
    }
}