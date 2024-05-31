using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantMenu.Restaurant
{
    public partial class Restaurant : System.Web.UI.MasterPage
    {
        //danger is DTableUser session

        public string UName;
        public string RestaurantName;
        public static string LastLogin;
        public string ValidityPeriod;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Helpers.CSRF_Handler.Validate(this.Page, forgeryToken);
    
            protectPage();

            try
            {
                if (!this.IsPostBack)
                {
                    //chk id profile is empty
                    if (new BLL.Restaurants().IncompleteProfile(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId])))
                    {
                        Response.Redirect("~/Restaurant/ProfileUpdate.aspx");
                    }

                    PnlNoti.Controls.Add(new LiteralControl(getNotifications()));

                }
                else
                {
                    //its not page load

                }

                //check if validity is not expired
                if (new BLL.Restaurants().IsValidityDateExp(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId])))
                {
                    Response.Redirect("~/Public/Pricing.aspx");
                }

                ValidityPeriod = new BLL.Restaurants().GetValidityDate(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]));
                ValidityPeriod = (string.IsNullOrEmpty(ValidityPeriod)) ? "Not Set" : Convert.ToDateTime(ValidityPeriod).ToString("dd-MMM-yyyy") + " UTC";
                UName = Session[new Helpers.SessionNames().SiteUserName].ToString();
                RestaurantName = getRestaurantName();
                getLastlogin();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string getRestaurantName()
        {
            try
            {
                string _restName = new BLL.Restaurants().GetRestaurantName(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]));
                return (string.IsNullOrEmpty(_restName)) ? "Restaurant" : _restName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void getLastlogin()
        {
            if (!string.IsNullOrEmpty(Session[new Helpers.SessionNames().SiteUserLastLogin].ToString()))
            {
                LastLogin = new Helpers.Global().UTCtoEDT(Convert.ToDateTime(Session[new Helpers.SessionNames().SiteUserLastLogin].ToString())).ToString("hh:mmtt dd-MMM");
            }
            else
            {
                LastLogin = "Welcome First Time Login";
            }
        }
        private void protectPage()
        {
            //session check
            if (Session[new Helpers.SessionNames().SiteUserId] != null && !string.IsNullOrEmpty(Session[new Helpers.SessionNames().SiteUserId].ToString()))
            {
                if (Session[new Helpers.SessionNames().SiteUserType] != null)
                {
                    if (Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserType]) != 2)
                    {
                        redirectUserType(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserType]));
                    }
                    //the user is restaurant and should'nt redirect other modules or login page
                }
                else
                {
                    Response.Redirect("~/Public/Signin.aspx");
                }
            }
            else
            {
                //cookie check
                HttpCookie reqCookies = Request.Cookies[new Helpers.CookieNames().SiteUser];
                if (reqCookies != null && reqCookies.HasKeys && !string.IsNullOrEmpty(reqCookies[new Helpers.CookieNames().UserEmail].ToString()) && !string.IsNullOrEmpty(reqCookies[new Helpers.CookieNames().UserPassword].ToString()))
                {
                    signin(Helpers.Global.Decrypt(reqCookies[new Helpers.CookieNames().UserEmail].ToString()), reqCookies[new Helpers.CookieNames().UserPassword].ToString());

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
        private void signin(string _email, string _password)
        {
            try
            {
                DataTable dtSiteUser = new BLL.Users().AuthenticateActiveUserGetData(_email, _password);

                if (dtSiteUser != null && dtSiteUser.Rows.Count > 0)
                {
                    setSesstion(dtSiteUser);

                    redirectUserType(Convert.ToInt32(dtSiteUser.Rows[0]["Type"]));
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMe", "alertMe('error','Sorry Invalid Email or Password','center',3000);", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void setSesstion(DataTable dtSiteUser)
        {
            Session[new Helpers.SessionNames().SiteUserId] = dtSiteUser.Rows[0]["Id"].ToString();
            Session[new Helpers.SessionNames().SiteUserName] = dtSiteUser.Rows[0]["Name"].ToString();
            Session[new Helpers.SessionNames().SiteUserLastLogin] = dtSiteUser.Rows[0]["LastLogin"].ToString();
            Session[new Helpers.SessionNames().SiteUserEmail] = dtSiteUser.Rows[0]["Email"].ToString();
            Session[new Helpers.SessionNames().SiteUserType] = dtSiteUser.Rows[0]["Type"].ToString();
            Session[new Helpers.SessionNames().SiteUserPassword] = dtSiteUser.Rows[0]["Password"].ToString();
        }
        protected void TimerNoti_Tick(object sender, EventArgs e)
        {
            PnlNoti.Controls.Add(new LiteralControl(getNotifications()));
        }
        private Int32 countNotificaions;
        private string getNotifications()
        {
            countNotificaions = 0;

            string notifications = "";
            try
            {
                notifications += getCustomNotificaions();

                string bellclass;
                if (countNotificaions > 0)
                {
                    bellclass = "la la-bell animated infinite swing";
                }
                else
                {
                    bellclass = "la la-bell";
                }

                string redDotClass = "";
                if (countNotificaions > 0)
                {
                    redDotClass = "badge-pulse";
                }
                else
                {
                    redDotClass = "";
                }

                string notiCount;
                if (countNotificaions > 7)
                {
                    notiCount = "7+";
                }
                else
                {
                    notiCount = countNotificaions.ToString();
                }

                string noty = string.Format(@"<li class='nav-item dropdown'><a id='notifications' rel='nofollow' data-target='#' href='#' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false' class='nav-link'><i class='{0}'></i><span class='{1}'></span></a>
                                <ul aria-labelledby='notifications' class='dropdown-menu notification'>
                                    <li>
                                        <div class='notifications-header'>
                                            <div class='title'>Orders {2}</div>
                                            <div class='notifications-overlay'></div>
                                            <img src='{3}' alt='...' class='img-fluid'/>
                                        </div>
                                    </li>
                                        {4}
                                    <li>
                                        <a rel='nofollow' href='{5}' class='dropdown-item all-notifications text-center'>View All Orders</a>
                                    </li>

                                </ul>
                            </li>", bellclass, redDotClass, notiCount, ResolveUrl("~/Template/assets/img/notifications/01.jpg"), notifications, ResolveUrl("~/Restaurant/Orders.aspx"));

                return noty;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        private string getCustomNotificaions()
        {
            try
            {
                Int32 restaurantId = new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]));
                string notifications = "";

                DataTable dtNoti = new BLL.Restaurants().GetOrderNotifications("TOP(7)", restaurantId);
                if (dtNoti != null && dtNoti.Rows.Count > 0)
                {
                    foreach (DataRow row in dtNoti.Rows)
                    {
                        DateTime dtime = new Helpers.Global().UTCtoEDT(Convert.ToDateTime(row["OrderPlacedDTime"]));

                        countNotificaions++;
                        notifications += string.Format(@"<li>
                                                        <a href='{0}'>
                                                            <div class='message-icon'>
                                                                <i class='la la-bullhorn'></i>
                                                            </div>
                                                            <div class='message-body'>
                                                                <div class='message-body-heading'>
                                                                    {1}
                                                                </div>
                                                                <span class='date'>{2}</span>
                                                            </div>
                                                        </a>
                                                    </li>", "#", "Table:" + row["Table"].ToString() + ". Food:" + row["Food"].ToString(), dtime.ToString("hh:mmtt dd-MMM"));
                    }
                }
                return notifications;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}