using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantMenu.Public
{
    public partial class Pricing : System.Web.UI.Page
    {
        public static string PricingMessage;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[new Helpers.SessionNames().SiteUserId] != null && !string.IsNullOrEmpty(Session[new Helpers.SessionNames().SiteUserId].ToString()))
            {
                try
                {
                    string ValidityPeriod = new BLL.Restaurants().GetValidityDate(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]));
                    ValidityPeriod = (string.IsNullOrEmpty(ValidityPeriod)) ? "not Valid" : Convert.ToDateTime(ValidityPeriod).ToString("dd-MMM-yyyy");

                    PricingMessage = string.Format(@"<h3>Hello {0}. Your Restaurant Validity Date is : {1} UTC.</h3>"
                        , Session[new Helpers.SessionNames().SiteUserName]
                        , ValidityPeriod);

                    if (Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserType].ToString())==2)
                    {
                        string[] para=new string[1];
                        para[0] = Session[new Helpers.SessionNames().SiteUserId].ToString();
                        Task.Run(() => sendEmail(para));
                    }
                }
                catch (Exception ex)
                {
                    Helpers.WriteLogToFile.Write(ex);
                }
            }
        }
        private static void sendEmail(string[] param)
        {
            try
            {
                DataTable dt = new BLL.Users().GetRestaurantUsers(" AND USR.Id=" + param[0] + "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    string emailBody = string.Format(@"Restaurant: {0}. Owner: {1}. Email: {2}. Restaurant Phone: {3}. Address: {4}, {5}. Validity Date: {6}."
                    , dt.Rows[0]["RestaurantName"].ToString()
                    , dt.Rows[0]["UserName"].ToString()
                    , dt.Rows[0]["Email"].ToString()
                    , dt.Rows[0]["Phone"].ToString()
                    , dt.Rows[0]["Address"].ToString()
                    , dt.Rows[0]["City"].ToString()
                    , (string.IsNullOrEmpty(dt.Rows[0]["ValidityDate"].ToString()))?"Not set Yet. New Restaurant Signup": Convert.ToDateTime(dt.Rows[0]["ValidityDate"].ToString()).ToString("dd-MMM-yyyy"));

                    Helpers.Email.Send(ConfigurationManager.AppSettings["AppEmail"].ToString(), ConfigurationManager.AppSettings["BusinessName"].ToString()
                        , ConfigurationManager.AppSettings["BusinessEmail"].ToString(), ConfigurationManager.AppSettings["BusinessOwnerName"].ToString()
                        , "Landed on Pricing Page.", emailBody, false, null);
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