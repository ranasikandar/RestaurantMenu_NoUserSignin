using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantMenu.Menu
{
    public partial class Order : System.Web.UI.Page
    {
        public static string TotalFoodItems = "None";
        public static string RestaurantName = "Restaurant";
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
                            //load menu items
                            Int32 tableId = new BLL.Restaurants().GetTableId(Convert.ToInt32(Request.QueryString["res"]), new Helpers.Global().RemoveSpecialCharacters(Request.QueryString["tbl"]));
                            TotalFoodItems = new BLL.Restaurants().CountTotalFoods("AND [Enable]=1 AND [RestaurantId]=" + new Helpers.Global().RemoveSpecialCharacters(Request.QueryString["res"]) + "").ToString();
                            RestaurantName = new BLL.Restaurants().GetRestaurantName(new Helpers.Global().RemoveSpecialCharacters(Request.QueryString["res"]));
                            TableName = new BLL.Restaurants().GetTableName(Convert.ToInt32(Request.QueryString["res"]), tableId);

                            DataTable dtCategories = new BLL.Restaurants().GetProductCategories(" AND RestaurantId=" + new Helpers.Global().RemoveSpecialCharacters(Request.QueryString["res"]) + " ORDER BY CategoryName");
                            DataRow row = dtCategories.NewRow();
                            dtCategories.Rows.InsertAt(row, 0);
                            dtCategories.Rows[0]["Id"] = 0;
                            dtCategories.Rows[0]["CategoryName"] = "All";
                            ddlCategory.DataSource = dtCategories;
                            ddlCategory.DataBind();

                            this.pnlDashboard.Controls.Add(new LiteralControl(getDashboardData(tableId,0)));
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
            }
        }
        
        private string getDashboardData(Int32 tblId, Int32 categoryId)
        {
            string htmlStr="";
            string currencyCode = "";

            try
            {
                string query = "";
                if (true)
                {
                    
                }

                DataTable dt;

                if (categoryId>0)
                {
                    query = "AND PRO.[Enable]=1 AND RESUSR.RestaurantId=" + Convert.ToInt32(Request.QueryString["res"]) + " AND PRO.CategoryId="+categoryId+" ORDER BY PRO.Name";
                    dt = new BLL.Restaurants().GetRestaurantProducts(query);
                }
                else
                {
                    query = "AND PRO.[Enable]=1 AND RESUSR.RestaurantId=" + Convert.ToInt32(Request.QueryString["res"]) + " ORDER BY PRO.Name";
                    dt = new BLL.Restaurants().GetRestaurantProductsForMenu(query);
                    dt.DefaultView.Sort = "CategoryId asc";
                    dt = dt.DefaultView.ToTable();
                    dt.DefaultView.Sort = "DisplayOrder asc";
                    dt = dt.DefaultView.ToTable();
                }
                
                if ((dt != null && dt.Rows.Count > 0)&&tblId>0)
                {
                    currencyCode = new BLL.Restaurants().GetCurrencyCode(Convert.ToInt32(Request.QueryString["res"]));
                    
                    htmlStr += "<div class='row'>";//begin 1div
                    int catIdShown=0;


                    foreach (DataRow row in dt.Rows)
                    {

                        if (categoryId == 0)
                        {
                            if (catIdShown!= Convert.ToInt32(row["CategoryId"]))
                            {
                                DataTable productCat = new BLL.Restaurants().GetRestaurantWaiterProducts($"AND PRO.Id={row["Id"]}");

                                catIdShown = Convert.ToInt32(row["CategoryId"]);
                                htmlStr += "<div class='col-xl-3 col-md-4 col-sm-6'>";//begin category header
                                htmlStr += $"<h3 class='mt3 mb-3'>"+productCat.Rows[0]["CategoryName"].ToString()+"</h3>";
                                htmlStr += "</div>";
                            }
                        }

                        htmlStr += "<div class='col-xl-3 col-md-4 col-sm-6'>";//begin product
                        htmlStr += "<div class='widget has-shadow'>";
                        htmlStr += "<figure class='img-hover-01'>";
                        htmlStr += string.Format(@"<img src='{0}' class='img-fluid' alt='...'/>"
                            , (string.IsNullOrEmpty(row["ImagePath"].ToString())) ? ResolveUrl("~/Template/assets/img/albums/01.jpg") : ResolveUrl("~/Reso/res/" + Convert.ToInt32(Request.QueryString["res"]) + "/p/tsm/" + row["ImagePath"].ToString() + ".jpg"));
                        htmlStr += "<div><a><span>" + Convert.ToDouble(row["Price"].ToString()) +" "+currencyCode+ "</span></a></div>";
                        htmlStr += "</figure>";
                        htmlStr += "<div class='widget-body text-center'>";
                        htmlStr += "<h3 class='mt-3 mb-3'>" + row["Name"].ToString() + "</h3>";
                        htmlStr += "<h6 class='mt-3 mb-3'>" + row["Discription"].ToString() + "</h6>";
                        htmlStr += "<h4 class='mt-3 mb-3'>" + Convert.ToDouble(row["Price"].ToString()) + " " + currencyCode + "</h4>";
                        htmlStr += "</div>";
                        htmlStr += "</div>";
                        htmlStr += "</div>";//end product
                    }

                    htmlStr += "</div>";//end 1div
                }
                else
                {
                    htmlStr += string.Format(@"<div class='widget-body text-center'><h3 class='mt3 mb-3'>No Food Item Available</h3></div>");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return htmlStr;
        }
        
        private bool restaurantTblIdValid(Int32 res, string tbl)
        {
            try
            {
                bool? _tmp = new BLL.Restaurants().RestaurantTblAndIdIsValid(res, tbl);
                if (_tmp!=null&&_tmp==true)
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

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 tableId = new BLL.Restaurants().GetTableId(Convert.ToInt32(Request.QueryString["res"]), new Helpers.Global().RemoveSpecialCharacters(Request.QueryString["tbl"]));
            this.pnlDashboard.Controls.Add(new LiteralControl(getDashboardData(tableId, Convert.ToInt32(ddlCategory.SelectedValue))));
        }

    }
}