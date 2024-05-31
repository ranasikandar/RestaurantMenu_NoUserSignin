using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantMenu.Admin
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            protectPage();

            if (!this.IsPostBack)
            {
                this.pnlDashboard.Controls.Add(new LiteralControl(getDashboardData()));
            }
        }
        protected void TimerDashboard_Tick(object sender, EventArgs e)
        {
            this.pnlDashboard.Controls.Add(new LiteralControl(getDashboardData()));
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
                    if (Session[new Helpers.SessionNames().SiteUserType].ToString() != "1")
                    {
                        redirectUserType(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserType]));
                    }
                    //user is admin and have session
                }
                else
                {
                    Response.Redirect("~/Public/Signin.aspx");
                }
            }
        }
        private void redirectUserType(Int32 _userType)
        {
            if (_userType == 2)
            {
                Response.Redirect("~/Restaurant/Home.aspx");
            }
            if (_userType == 3)
            {
                Response.Redirect("~/Waiter/Home.aspx");
            }
        }
        private string getDashboardData()
        {
            string dashboardData;

            string lastRegRestaurant = new BLL.Restaurants().GetLastRegisterRestaurantName();
            Int32 validRestaurants = new BLL.Restaurants().CountValidRestaurants();
            Int32 validityExpRestaurants = new BLL.Restaurants().CountValidityExpRestaurants();
            Int32 disabledRestaurants = new BLL.Restaurants().CountDisabledRestaurants();
            Int32 withoutEmailVerifyRestaurans = new BLL.Restaurants().CountWithoutEmailVerityRestaurants();
            Int32 incompleteProfiles = new BLL.Restaurants().CountIncompleteProfiles();
            Int32 validityExpiresToday = new BLL.Restaurants().CountValidityExpToday();
                        
            Int32 totalRestaurants = new BLL.Restaurants().CountTotalRestaurants("");
            Int32 totalTables = new BLL.Restaurants().CountTotalTables("AND [Enable]=1");
            Int32 totalFoods = new BLL.Restaurants().CountTotalFoods("AND [Enable]=1");

            try
            {
                dashboardData = string.Format(@"

<div class='row flex-row'>
    <div class='col-xl-12 col-md-12 os-animation' data-os-animation='fadeInUp'>
        <div class='widget widget-05 has-shadow'>
            <div class='widget-body no-padding hidden'>
                
				<div class='author-avatar'>
                    <img src='{6}' alt='...' class='img-fluid rounded-circle' />
                </div>
				
                <div class='author-name'>{7}
                <span>{8}</span>
                </div>
				
                <div class='social-stats'>
                    <div class='row d-flex justify-content-between'>
                        <div class='col-4 text-center'>
                            <i class='la la-hourglass-start followers'></i>
                            <div class='counter'>{9}</div>
                            <div class='heading'>{10}</div>
                        </div>
                        <div class='col-4 text-center'>
                            <i class='la la-hourglass-end followers'></i>
                            <div class='counter'>{11}</div>
                            <div class='heading'>{12}</div>
                        </div>
                        <div class='col-4 text-center'>
                            <i class='la la-hourglass-half followers'></i>
                            <div class='counter'>{19}</div>
                            <div class='heading'>{20}</div>
                        </div>
                    </div>
                </div>

                <div class='author-name'>
                    <span>Profile Status</span>
                </div>

                <div class='social-stats'>
                    <div class='row d-flex justify-content-between'>
                        <div class='col-4 text-center'>
                            <i class='la la-unlink followers'></i>
                            <div class='counter'>{15}</div>
                            <div class='heading'>{16}</div>
                        </div>
                        <div class='col-4 text-center'>
                            <i class='la la-edit followers'></i>
                            <div class='counter'>{17}</div>
                            <div class='heading'>{18}</div>
                        </div>
                        <div class='col-4 text-center'>
                            <i class='la la-user-times followers'></i>
                            <div class='counter'>{13}</div>
                            <div class='heading'>{14}</div>
                        </div>
                    </div>
                </div>
				
            </div>
        </div>
    </div>

</div>


<div class='row flex-row'>

    <div class='col-xl-4 col-md-4 col-sm-12 os-animation' data-os-animation='fadeInUp'>
        <div class='widget widget-21 has-shadow' style='height: auto;'>
            <div class='widget-body h-100 d-flex align-items-center' style='background-image: url({0}); background-repeat: no-repeat; background-size: 90px 80px; background-position: right;'>
                <div class='section-title'>
                    <h3>Total Restaurants</h3>
                </div>
                <div class='value-progressRana text-gradient-02'>
                    {1}
                </div>
            </div>
        </div>
    </div>

    <div class='col-xl-4 col-md-4 col-sm-12 os-animation' data-os-animation='fadeInUp'>
        <div class='widget widget-21 has-shadow' style='height: auto;'>
            <div class='widget-body h-100 d-flex align-items-center' style='background-image: url({2}); background-repeat: no-repeat; background-size: 90px 80px; background-position: right;'>
                <div class='section-title'>
                    <h3>Total Tables</h3>
                </div>
                <div class='value-progressRana text-gradient-02'>
                    {3}
                </div>
            </div>
        </div>
    </div>

    <div class='col-xl-4 col-md-4 col-sm-12 os-animation' data-os-animation='fadeInUp'>
        <div class='widget widget-21 has-shadow' style='height: auto;'>
            <div class='widget-body h-100 d-flex align-items-center' style='background-image: url({4}); background-repeat: no-repeat; background-size: 90px 80px; background-position: right;'>
                <div class='section-title'>
                    <h3>Total Foods</h3>
                </div>
                <div class='value-progressRana text-gradient-02'>
                    {5}
                </div>
            </div>
        </div>
    </div>

</div>

{21}

", ResolveUrl("~/Template/assets/img/balls/restaurants.png"), (totalRestaurants > 0) ? totalRestaurants.ToString() : "None"
 , ResolveUrl("~/Template/assets/img/balls/tables.png"), (totalTables>0)?totalTables.ToString():"None"
 , ResolveUrl("~/Template/assets/img/balls/foods.png"), (totalFoods>0)?totalFoods.ToString():"None"
, ResolveUrl("~/Template/assets/img/avatar/avatar-03.jpg"), (string.IsNullOrEmpty(lastRegRestaurant) ? "Name Not Provided Yet" : lastRegRestaurant), "Last Register Restaurant", (validRestaurants>0)?validRestaurants.ToString():"None", "Validate Restaurants"
, (validityExpRestaurants>0)?validityExpRestaurants.ToString():"None", "Validity Expired Restaurants", (disabledRestaurants>0)?disabledRestaurants.ToString():"None", "Disabled Restaurants", (withoutEmailVerifyRestaurans>0)?withoutEmailVerifyRestaurans.ToString():"None", "Without Email Verification", (incompleteProfiles>0)?incompleteProfiles.ToString():"None", "Incomplete Profiles", (validityExpiresToday>0)?validityExpiresToday.ToString():"None", "Validity Expires Today", getLog());
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
                string query = "ORDER BY USL.LogDateTime DESC";

                DataTable dtLog = new BLL.Users().GetLog("TOP(500)", query);
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
                        HTMLstring += "<li class='list-group-item'>";
                        HTMLstring += "<div class='media'>";
                        HTMLstring += "<div class='media-body align-self-center'>";
                        HTMLstring += "<div class='username'>";
                        HTMLstring += "<h4>" + row["Name"].ToString() + "</h4>";

                        HTMLstring += "</div>";
                        HTMLstring += "<div class='msg'>";
                        HTMLstring += "<p>" + row["LogDetail"].ToString() + "</p>";
                        HTMLstring += "</div>";
                        HTMLstring += "<div class='meta'>";
                        HTMLstring += "<span class='mr-3'>" + new Helpers.Global().UTCtoEDT(Convert.ToDateTime(row["LogDateTime"])).ToString("dd-MM-yy hh:mm tt") + "</span>";
                        HTMLstring += "</div></div></div></li>";

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
    }
}