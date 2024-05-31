using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantMenu.Public
{
    public partial class EmailVerification : System.Web.UI.Page
    {
        public static string MessageOut;
        DataTable dtUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            var email = Request.QueryString["email"];
            var key = Request.QueryString["key"];

            if ((email != null && !string.IsNullOrEmpty(email.ToString())) && (key != null && !string.IsNullOrEmpty(key.ToString())))
            {
                try
                {
                    MailAddress m = new MailAddress(email);
                }
                catch (FormatException)
                {
                    MessageOut = "Email format is invalid!";
                    return;
                }

                try
                {
                    dtUser = new BLL.Users().GetUsers("AND Email='" + email + "' AND LostPassKey='" + new Helpers.Global().RemoveSpecialCharacters(key) + "'");

                    if (dtUser != null && dtUser.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dtUser.Rows[0]["Type"]) == 2)//restaurant
                        {
                            if (new BLL.Users().UserRestaurantEmailVerification(email, new Helpers.Global().RemoveSpecialCharacters(key)))
                            {
                                MessageOut = "Thank you Restaurant for creating an " + System.Configuration.ConfigurationManager.AppSettings["BusinessNicName"].ToString() + " account<br/>You may Sign in with Email " + email + "";
                            }
                            else
                            {
                                //email varified faild
                                MessageOut = "Sorry Email Verification Faild for " + email + "<br/>Please try again leter";
                                Helpers.WriteLogToFile.Write("Sorry Email Verification Faild for " + email + " Restaurant Please try again leter");
                            }
                        }
                        if (Convert.ToInt32(dtUser.Rows[0]["Type"]) == 3)//waiter
                        {
                            if (new BLL.Users().UserWaiterEmailVerification(email, new Helpers.Global().RemoveSpecialCharacters(key)))
                            {
                                MessageOut = "Thank you Waiter for creating an " + System.Configuration.ConfigurationManager.AppSettings["BusinessNicName"].ToString() + " account<br/>You may Sign in with Email " + email + "";
                            }
                            else
                            {
                                //email varified faild
                                MessageOut = "Sorry Email Verification Faild for " + email + "<br/>Please try again leter";
                                Helpers.WriteLogToFile.Write("Sorry Email Verification Faild for " + email + " Waiter Please try again leter");
                            }
                        }
                    }
                    else
                    {
                        //email already varified or not found
                        MessageOut = "Sorry this Email '" + email + "' <br/>Already varified or not found in our database";
                    }
                }
                catch (Exception ex)
                {
                    //error
                    MessageOut = "Sorry there was an Error while your email Verification<br/>Please Try again leter or contact support";
                    Helpers.WriteLogToFile.Write(ex);
                }
            }
            else//invalid input 
            {
                MessageOut = "Sorry Invalid Request";
            }

        }
    }
}