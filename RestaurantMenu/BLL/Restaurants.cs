using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RestaurantMenu.BLL
{
    public class Restaurants
    {
        ListDictionary parameters = new ListDictionary();

        public Int32 CountTotalRestaurants(string where)
        {
            try
            {
                return new DAL.Database().ExecuteForInt(@"SELECT COUNT(USR.Id) AS TotalRestaurants FROM Users USR 
                INNER JOIN RestaurantUsers RESUS ON RESUS.UserId=USR.Id WHERE 1=1 "+where+"");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetLastRegisterRestaurantName()
        {
            try
            {
                return new DAL.Database().ExecuteForStr(@"SELECT Name FROM Restaurants ORDER BY RegisterDate DESC");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 CountValidRestaurants()
        {
            try
            {
                return new DAL.Database().ExecuteForInt(@"SELECT COUNT(Id) FROM Restaurants WHERE ValidityDate>CONVERT(DATE,GETUTCDATE())");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 CountValidityExpRestaurants()
        {
            try
            {
                return new DAL.Database().ExecuteForInt(@"SELECT COUNT(Id) FROM Restaurants WHERE ValidityDate<CONVERT(DATE,GETUTCDATE())");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 CountDisabledRestaurants()
        {
            try
            {
                return new DAL.Database().ExecuteForInt(@"SELECT COUNT(Id) FROM Users WHERE [Enable]=0 AND [Type]=2");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 CountWithoutEmailVerityRestaurants()
        {
            try
            {
                return new DAL.Database().ExecuteForInt(@"SELECT COUNT(Id) FROM [Users] WHERE [EmailVerify]=0 AND [Type]=2");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 CountIncompleteProfiles()
        {
            try
            {
                return new DAL.Database().ExecuteForInt(@"SELECT COUNT(Id) FROM [Restaurants] WHERE ((DATALENGTH([Name])=0) OR ([Name] IS NULL)) AND ((DATALENGTH([Address])=0) OR ([Address] IS NULL)) AND ((DATALENGTH([Phone])=0) OR ([Phone] IS NULL))");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 CountValidityExpToday()
        {
            try
            {
                return new DAL.Database().ExecuteForInt(@"SELECT COUNT(Id) FROM Restaurants WHERE ValidityDate=CONVERT(DATE,GETUTCDATE())");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 CountTotalTables(string where)
        {
            try
            {
                return new DAL.Database().ExecuteForInt(@"SELECT COUNT(Id) FROM [RestaurantTables] WHERE 1=1 "+where+"");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 CountTotalFoods(string where)
        {
            try
            {
                return new DAL.Database().ExecuteForInt(@"SELECT COUNT(Id) FROM [Products] WHERE 1=1 " + where + "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetRestaurantName(Int32 restaurantUserId)
        {
            try
            {
                return new DAL.Database().ExecuteForStr(@"SELECT REST.Name FROM Restaurants REST 
                                                            INNER JOIN RestaurantUsers RESUSR ON RESUSR.RestaurantId=REST.Id 
                                                            INNER JOIN Users USR ON USR.Id=RESUSR.UserId 
                                                            WHERE USR.Id="+restaurantUserId+"");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetRestaurantNameWaiter(Int32 waiterUserId)
        {
            try
            {
                return new DAL.Database().ExecuteForStr(@"SELECT REST.Name FROM Restaurants REST 
                INNER JOIN RestaurantWaiters RESWAIT ON RESWAIT.RestaurantId=REST.Id 
                INNER JOIN Users USR ON USR.Id=RESWAIT.UserId 
                WHERE USR.Id=" + waiterUserId + "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IncompleteProfile(Int32 userId)
        {
            try
            {
                return new DAL.Database().ExecuteForBool(@"SELECT REST.Id FROM [Restaurants] REST 
                INNER JOIN RestaurantUsers RESUSR ON RESUSR.RestaurantId=REST.Id 
                INNER JOIN Users USR ON USR.Id=RESUSR.UserId 
                WHERE ((DATALENGTH(REST.[Name])=0) OR (REST.[Name] IS NULL)) AND ((DATALENGTH(REST.[Address])=0) OR (REST.[Address] IS NULL)) AND ((DATALENGTH(REST.[Phone])=0) OR (REST.[Phone] IS NULL)) AND USR.Id="+userId+"");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UserRestaurantProfileUpdate(Objects.RestaurantUser obj)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_UId", SqlDbType.Int), obj.UId);
                parameters.Add(new SqlParameter("@p_UName", SqlDbType.NVarChar, 50), obj.UName);
                parameters.Add(new SqlParameter("@p_RName", SqlDbType.NVarChar, 100), obj.RName);
                parameters.Add(new SqlParameter("@p_Country", SqlDbType.NVarChar, 50), obj.RCountry);
                parameters.Add(new SqlParameter("@p_City", SqlDbType.NVarChar, 50), obj.RCity);
                parameters.Add(new SqlParameter("@p_Address", SqlDbType.NVarChar, 50), obj.RAddress);
                parameters.Add(new SqlParameter("@p_Phone", SqlDbType.NVarChar, 20), obj.RPhone);
                parameters.Add(new SqlParameter("@p_CurrencyCode", SqlDbType.NVarChar, 3), obj.RCurrencyCode);
                parameters.Add(new SqlParameter("@p_NotiEmail", SqlDbType.NVarChar, 50), obj.RNotificationEmail);
                parameters.Add(new SqlParameter("@p_SendNotiEmail", SqlDbType.Bit), (obj.RSendEmailNotification)?1:0);

                parameters.Add(new SqlParameter("@p_RLogoPath", SqlDbType.NVarChar, 500), (string.IsNullOrEmpty(obj.RLogoPath)) ? "0" : obj.RLogoPath);
                parameters.Add(new SqlParameter("@p_RLogoSmallPath", SqlDbType.NVarChar, 500), (string.IsNullOrEmpty(obj.RLogoSmallPath)) ? "0" : obj.RLogoSmallPath);

                return new DAL.Database().ExecuteNonQueryOnly("Sps_UserRestaurant_ProfileUpdate", parameters);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public string GetValidityDate(Int32 userId)
        {
            try
            {
                return new DAL.Database().ExecuteForStr(@"SELECT REST.ValidityDate FROM [Restaurants] REST 
                INNER JOIN RestaurantUsers RESUSR ON RESUSR.RestaurantId=REST.Id 
                INNER JOIN Users USR ON USR.Id=RESUSR.UserId 
				WHERE REST.Id=(SELECT [RestaurantId] FROM [RestaurantUsers] WHERE [RestaurantUsers].[UserId]="+userId+")");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsValidityDateExp(Int32 userId)
        {
            try
            {
                return new DAL.Database().ExecuteForBool(@"SELECT CASE 
                WHEN CONVERT(DATE,REST.ValidityDate) >= CONVERT(DATE,GETUTCDATE())
                THEN 0
                ELSE 1
                END AS ValidityExp 
                FROM [Restaurants] REST 
                INNER JOIN RestaurantUsers RESUSR ON RESUSR.RestaurantId=REST.Id 
                INNER JOIN Users USR ON USR.Id=RESUSR.UserId 
                WHERE REST.Id=(SELECT [RestaurantId] FROM [RestaurantUsers] WHERE [RestaurantUsers].[UserId]="+userId+")");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsValidityDateExpWaiter(Int32 restaurantId)
        {
            try
            {
                return new DAL.Database().ExecuteForBool(@"SELECT CASE 
                WHEN CONVERT(DATE,REST.ValidityDate) >= CONVERT(DATE,GETUTCDATE())
                THEN 0
                ELSE 1
                END AS ValidityExp 
                FROM [Restaurants] REST 
                WHERE REST.Id=" + restaurantId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetRestaurantProducts(string where)
        {
            try
            {
                return new DAL.Database().ExecuteForDataTable(@"SELECT PRO.* FROM Products PRO 
                INNER JOIN RestaurantUsers RESUSR ON RESUSR.RestaurantId=PRO.RestaurantId
                INNER JOIN Users USR ON USR.Id=RESUSR.UserId 
                WHERE 1=1 " + where + "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetRestaurantProductsForMenu(string where)
        {
            try
            {
                return new DAL.Database().ExecuteForDataTable(@"SELECT PRO.*, PRC.DisplayOrder FROM Products PRO 
                INNER JOIN RestaurantUsers RESUSR ON RESUSR.RestaurantId=PRO.RestaurantId
                INNER JOIN Users USR ON USR.Id=RESUSR.UserId 
                INNER JOIN ProductCategories PRC ON PRC.Id=PRO.CategoryId
                WHERE 1=1  " + where + "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetRestaurantProductsWithCatPri(string where)
        {
            try
            {
                return new DAL.Database().ExecuteForDataTable(@"SELECT PRO.Id, PRO.Name, PROCAT.CategoryName, PRO.Price FROM Products PRO 
                INNER JOIN RestaurantUsers RESUSR ON RESUSR.RestaurantId=PRO.RestaurantId 
                INNER JOIN ProductCategories PROCAT ON PROCAT.Id=PRO.CategoryId 
                INNER JOIN Users USR ON USR.Id=RESUSR.UserId 
                WHERE 1=1 " + where + "");//AND PRO.[Enable]=1 AND USR.Id=X ORDER BY PRO.Name // AND RESUSR.RestaurantId=6
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetRestaurantWaiterProducts(string where)
        {
            try
            {
                return new DAL.Database().ExecuteForDataTable(@"SELECT PRO.Id, PRO.Name, PROCAT.CategoryName, PRO.Price FROM Products PRO 
                INNER JOIN ProductCategories PROCAT ON PROCAT.Id=PRO.CategoryId 
                WHERE 1=1 " + where + "");//AND PRO.[Enable]=1 AND USR.Id=X ORDER BY PRO.Name
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 GetRestaurantId(Int32 userId)
        {
            try
            {
                return new DAL.Database().ExecuteForInt(@"SELECT [RestaurantId] FROM [RestaurantUsers] WHERE [UserId]=" + userId + "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 GetWaiterRestaurantId(Int32 userId)
        {
            try
            {
                return new DAL.Database().ExecuteForInt(@"SELECT [RestaurantId] FROM [RestaurantWaiters] WHERE [UserId]=" + userId + "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool InsertFoodProduct(Objects.Product obj,Int32 userId)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_UserId", SqlDbType.Int),userId);
                parameters.Add(new SqlParameter("@p_CategoryId", SqlDbType.Int), obj.CategoryId);
                parameters.Add(new SqlParameter("@p_Name", SqlDbType.NVarChar, 50), obj.Name);
                parameters.Add(new SqlParameter("@p_Discription", SqlDbType.NVarChar, 500), obj.Discription);
                parameters.Add(new SqlParameter("@p_ImagePath", SqlDbType.NVarChar, 500), (string.IsNullOrEmpty(obj.ImagePath) ? "" : obj.ImagePath));
                parameters.Add(new SqlParameter("@p_Price", SqlDbType.Money), obj.Price);
                parameters.Add(new SqlParameter("@p_RestaurantId", SqlDbType.Int), obj.RestaurantId);
                parameters.Add(new SqlParameter("@p_Enable", SqlDbType.Bit), (obj.Enable)?1:0);

                return new DAL.Database().ExecuteNonQueryOnly("Sps_InsertProduct", parameters);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public bool UpdateFoodProduct(Objects.Product obj, Int32 userId)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_Id", SqlDbType.BigInt), obj.Id);
                parameters.Add(new SqlParameter("@p_UserId", SqlDbType.Int), userId);
                parameters.Add(new SqlParameter("@p_CategoryId", SqlDbType.Int), obj.CategoryId);
                parameters.Add(new SqlParameter("@p_Name", SqlDbType.NVarChar, 50), obj.Name);
                parameters.Add(new SqlParameter("@p_Discription", SqlDbType.NVarChar, 500), obj.Discription);
                parameters.Add(new SqlParameter("@p_ImagePath", SqlDbType.NVarChar, 500), (string.IsNullOrEmpty(obj.ImagePath) ? "" : obj.ImagePath));
                parameters.Add(new SqlParameter("@p_Price", SqlDbType.Money), obj.Price);
                parameters.Add(new SqlParameter("@p_Enable", SqlDbType.Bit), (obj.Enable) ? 1 : 0);

                return new DAL.Database().ExecuteNonQueryOnly("Sps_UpdateProduct", parameters);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public DataTable GetRestaurantTables(string where)
        {
            try
            {
                return new DAL.Database().ExecuteForDataTable(@"SELECT TBL.* FROM RestaurantTables TBL 
                INNER JOIN RestaurantUsers RESUSR ON RESUSR.RestaurantId=TBL.RestaurantId
                INNER JOIN Users USR ON USR.Id=RESUSR.UserId 
                WHERE 1=1 " + where + "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetRestaurantWaiterTables(string where)
        {
            try
            {
                return new DAL.Database().ExecuteForDataTable(@"SELECT TBL.* FROM RestaurantTables TBL 
                INNER JOIN RestaurantWaiters RESWAIT ON RESWAIT.RestaurantId=TBL.RestaurantId
                INNER JOIN Users USR ON USR.Id=RESWAIT.UserId  
                WHERE 1=1 " + where + "");//AND USR.Id=1012 ORDER BY TBL.Name
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool InsertRestaurantTable(Objects.RestaurantTable obj, Int32 userId)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_UserId", SqlDbType.Int), userId);
                parameters.Add(new SqlParameter("@p_Name", SqlDbType.NVarChar, 50), obj.Name);
                parameters.Add(new SqlParameter("@p_RestaurantId", SqlDbType.Int), obj.RestaurantId);
                parameters.Add(new SqlParameter("@p_Discription", SqlDbType.NVarChar, 200), obj.Discription);
                parameters.Add(new SqlParameter("@p_Enable", SqlDbType.Bit), (obj.Enable) ? 1 : 0);
                parameters.Add(new SqlParameter("@p_QRImageLocation", SqlDbType.NVarChar, 500), obj.QRImageLocation);
                parameters.Add(new SqlParameter("@p_QRCodeStr", SqlDbType.NVarChar, 100), obj.QRCodeStr);
                
                return new DAL.Database().ExecuteNonQueryOnly("Sps_InsertTable", parameters);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public bool UpdateRestaurantTable(Objects.RestaurantTable obj, Int32 userId)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_UserId", SqlDbType.Int), userId);
                parameters.Add(new SqlParameter("@p_Id", SqlDbType.Int), obj.Id);
                parameters.Add(new SqlParameter("@p_Name", SqlDbType.NVarChar, 50), obj.Name);
                parameters.Add(new SqlParameter("@p_RestaurantId", SqlDbType.Int), obj.RestaurantId);
                parameters.Add(new SqlParameter("@p_Discription", SqlDbType.NVarChar, 200), obj.Discription);
                parameters.Add(new SqlParameter("@p_Enable", SqlDbType.Bit), (obj.Enable) ? 1 : 0);
                parameters.Add(new SqlParameter("@p_QRImageLocation", SqlDbType.NVarChar, 500), obj.QRImageLocation);
                parameters.Add(new SqlParameter("@p_QRCodeStr", SqlDbType.NVarChar, 100), obj.QRCodeStr);

                return new DAL.Database().ExecuteNonQueryOnly("Sps_UpdateTable", parameters);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public bool? RestaurantTblAndIdIsValid(Int32 resId, string qrCodeStr)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_RestaurantId", SqlDbType.Int), resId);
                parameters.Add(new SqlParameter("@p_QRCodeStr", SqlDbType.NVarChar, 10), qrCodeStr);

                return new DAL.Database().ExecuteForBool("Sps_RestaurantTblIdValid", parameters);

            }
            catch (Exception ex)
            {

                throw ex;
                //return false;
            }
        }
        public string GetCurrencyCode(Int32 restaurantId)
        {
            try
            {
                return new DAL.Database().ExecuteForStr(@"SELECT CurrencyCode FROM Restaurants WHERE Id="+restaurantId+"");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 GetTableId(int restaurantId,string qrCodeStr)
        {
            try
            {
                return new DAL.Database().ExecuteForInt(@"SELECT Id FROM RestaurantTables WHERE RestaurantId="+restaurantId+" AND QRCodeStr='"+qrCodeStr+"'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool PlaceAnOrder(Int32 restaurant, Int32 table, Int32 product, Int32 qty, Int32 orderBy, string note)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_Rest", SqlDbType.Int), restaurant);
                parameters.Add(new SqlParameter("@p_Tbl", SqlDbType.Int), table);
                parameters.Add(new SqlParameter("@p_Pro", SqlDbType.BigInt), product);
                parameters.Add(new SqlParameter("@p_Qty", SqlDbType.Int), qty);
                parameters.Add(new SqlParameter("@p_OrderBy", SqlDbType.Int), orderBy);
                parameters.Add(new SqlParameter("@p_Note", SqlDbType.NVarChar), note);
                return new DAL.Database().ExecuteNonQueryOnly("Sps_PlaceAnOrder", parameters);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public string GetRestaurantName(string restaurantID)
        {
            try
            {
                return new DAL.Database().ExecuteForStr(@"SELECT [Name] FROM [Restaurants] WHERE [Id]="+restaurantID+"");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetTableName(Int32 restaurantID, Int32 tableId)
        {
            try
            {
                return new DAL.Database().ExecuteForStr(@"SELECT Name FROM RestaurantTables WHERE Id="+tableId+" AND RestaurantId="+restaurantID+"");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 CountMyOrders(Int32 restaurantId,Int32 tableId)
        {
            try
            {
                return new DAL.Database().ExecuteForInt(@"SELECT COUNT([Id]) FROM [Orders] WHERE (TableId="+tableId+") AND (RestaurantId="+restaurantId+") AND (StatusNameId=1 OR StatusNameId=3)");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 CountMyOrdersItems(Int32 restaurantId, Int32 tableId)
        {
            try
            {
                return new DAL.Database().ExecuteForInt(@"SELECT SUM([Qty]) FROM [Orders] WHERE (TableId="+tableId+") AND (RestaurantId="+restaurantId+") AND (StatusNameId=1 OR StatusNameId=3)");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetMyOrders(Int32 restaurantId, Int32 tableId)
        {
            try
            {
                //return new DAL.Database().ExecuteForDataTable(@"SELECT ORD.Id, ORD.StatusNameId, ORD.OrderPlacedDTime, ORD.Qty, ORD.OrderBy AS OrderById, USR.Name AS OrderByName, PRO.Name, PRO.Discription, PRO.ImagePath, PRO.Price FROM Orders ORD 
                //INNER JOIN Products PRO ON PRO.Id=ORD.ProductId 
                //INNER JOIN Users USR ON USR.Id=ORD.OrderBy 
                //WHERE ORD.RestaurantId="+restaurantId+" AND ORD.TableId="+tableId+" AND (ORD.StatusNameId=1 OR ORD.StatusNameId=3) ORDER BY USR.Name ");

                return new DAL.Database().ExecuteForDataTable(@"SELECT ORD.Id, ORD.StatusNameId, ORD.OrderPlacedDTime, ORD.Qty, ORD.OrderBy AS OrderById, ORD.Note, USR.Name AS OrderByName, PRO.Name, PRO.Discription, PRO.ImagePath, PRO.Price FROM Orders ORD 
                INNER JOIN Products PRO ON PRO.Id=ORD.ProductId 
                LEFT JOIN Users USR ON USR.Id=ORD.OrderBy 
                WHERE ORD.RestaurantId=" + restaurantId + " AND ORD.TableId=" + tableId + " AND (ORD.StatusNameId=1 OR ORD.StatusNameId=3) ORDER BY USR.Name ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetOrder(Int32 orderId)
        {
            try
            {
                return new DAL.Database().ExecuteForDataTable(@"SELECT * FROM Orders WHERE 1=1 AND Id="+orderId+"");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public double? GetMyOrdersTotalAmount(Int32 restaurantId, Int32 tableId)
        {
            try
            {
                return new DAL.Database().ExecuteForDouble(@"SELECT SUM(PRO.Price*ORD.Qty) AS Total FROM Orders ORD 
                INNER JOIN Products PRO ON PRO.Id=ORD.ProductId 
                WHERE ORD.RestaurantId="+restaurantId+" AND ORD.TableId="+tableId+" AND (ORD.StatusNameId=1 OR ORD.StatusNameId=3)");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetOrderNotifications(string top, Int32 restaurantId)
        {
            try
            {
                return new DAL.Database().ExecuteForDataTable(string.Format(@"SELECT {0} ORD.Id, ORD.OrderPlacedDTime, RESTBL.Name AS [Table], PRO.Name AS [Food] FROM Orders ORD 
                INNER JOIN Products PRO ON PRO.Id=ORD.ProductId 
                INNER JOIN RestaurantTables RESTBL ON RESTBL.Id=ORD.TableId 
                WHERE ORD.RestaurantId={1} AND ORD.StatusNameId=1 ORDER BY ORD.OrderPlacedDTime ASC", top, restaurantId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 CountOrdersWithStatus(Int32 statusId, Int32 restaurantId)
        {
            try
            {
                return new DAL.Database().ExecuteForInt(@"SELECT COUNT(Id) FROM Orders WHERE RestaurantId="+restaurantId+" AND StatusNameId="+statusId+"");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 CountUnPaidOrders(Int32 restaurantId)
        {
            try
            {
                return new DAL.Database().ExecuteForInt(@"SELECT COUNT(Id) FROM Orders WHERE RestaurantId="+restaurantId+" AND StatusNameId=3 AND Paid=0");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 CountTodayCompletedOrders(Int32 restaurantId)
        {
            try
            {
                return new DAL.Database().ExecuteForInt(@"SELECT COUNT(Id) FROM Orders WHERE RestaurantId="+restaurantId+" AND Paid=1 AND CONVERT(DATE,OrderPlacedDTime)=CONVERT(DATE,GETUTCDATE())");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 CountTodayCancelledOrders(Int32 restaurantId)
        {
            try
            {
                return new DAL.Database().ExecuteForInt(@"SELECT COUNT(Id) FROM Orders WHERE RestaurantId="+restaurantId+" AND CONVERT(DATE,OrderPlacedDTime)=CONVERT(DATE,GETUTCDATE()) AND (StatusNameId=2 OR StatusNameId=5)");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public double? TodayTotalSale(Int32 restaurantId)
        {
            try
            {
                return new DAL.Database().ExecuteForDouble(@"SELECT SUM(PRO.Price*ORD.Qty) FROM Orders ORD 
                INNER JOIN Products PRO ON PRO.Id=ORD.ProductId 
                WHERE ORD.RestaurantId=" +restaurantId+" AND CONVERT(DATE,OrderPlacedDTime)=CONVERT(DATE,GETUTCDATE()) AND ORD.Paid=1");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetTablesWithOrder(Int32 restaurantId)
        {
            try
            {
                return new DAL.Database().ExecuteForDataTable(string.Format(@"
                SELECT RESTBL.Id, MAX(RESTBL.Name) AS Name, MAX(RESTBL.Discription) AS Discription, SUM(ORD.Qty) AS Orders FROM Orders ORD 
                INNER JOIN RestaurantTables RESTBL ON RESTBL.Id=ORD.TableId 
                WHERE ORD.RestaurantId={0} AND (ORD.StatusNameId=1 OR ORD.StatusNameId=3) AND ORD.Paid=0
                GROUP BY RESTBL.Id
                ", restaurantId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetOrdersOfTable(Int32 restaurantId, Int32 tableId)
        {
            try
            {
                //return new DAL.Database().ExecuteForDataTable(string.Format(@"SELECT ORD.Id AS OrderID, ORD.RestaurantId, ORD.OrderPlacedDTime,ORD.TableId,PRO.Name AS Food, PRO.Discription, PRO.Price, ORD.Qty, ORDSTATNAME.Name AS [Status], PRO.ImagePath, ORD.StatusNameId FROM Orders ORD 
                //INNER JOIN Products PRO ON PRO.Id=ORD.ProductId 
                //INNER JOIN OrderStatusNames ORDSTATNAME ON ORDSTATNAME.Id=ORD.StatusNameId 
                //WHERE ORD.RestaurantId={0} AND ORD.TableId={1} AND (ORD.StatusNameId=1 OR ORD.StatusNameId=3) AND ORD.Paid=0 
                //", restaurantId, tableId));

                return new DAL.Database().ExecuteForDataTable(string.Format(@"SELECT ORD.Id AS OrderID, ORD.RestaurantId, ORD.OrderPlacedDTime,USTYP.UserType, USR.Name, USR.Email, ORD.TableId, PRO.Name AS Food, PROCAT.CategoryName, PRO.Discription, PRO.Price, ORD.Qty, ORDSTATNAME.Name AS [Status], ORD.Note, PRO.ImagePath, ORD.StatusNameId FROM Orders ORD 
                INNER JOIN Products PRO ON PRO.Id=ORD.ProductId 
                INNER JOIN OrderStatusNames ORDSTATNAME ON ORDSTATNAME.Id=ORD.StatusNameId 
                INNER JOIN Users USR ON USR.Id=ORD.OrderBy 
                INNER JOIN ProductCategories PROCAT ON PROCAT.Id= PRO.CategoryId 
                INNER JOIN UserTypes USTYP ON USTYP.Id=USR.[Type]
                WHERE ORD.RestaurantId={0} AND ORD.TableId={1} AND (ORD.StatusNameId=1 OR ORD.StatusNameId=3) AND ORD.Paid=0 ORDER BY ORD.OrderPlacedDTime--, ORD.OrderBy, PROCAT.Id"
                , restaurantId, tableId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool MarkOrderStatus(Int32 orderId, Int32 statusId, Int32 userId)//cancel by restaurant=2//processed=3//paid=4
        {
            try
            {
                parameters.Add(new SqlParameter("@p_OrdId", SqlDbType.Int), orderId);
                parameters.Add(new SqlParameter("@p_Status", SqlDbType.Int), statusId);
                parameters.Add(new SqlParameter("@p_UserId", SqlDbType.Int), userId);

                return new DAL.Database().ExecuteNonQueryOnly("Sps_MarkOrderStatus", parameters);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public bool MarkOrderPaidUnPaid(Int32 orderId, bool paid, Int32 userId)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_OrdId", SqlDbType.Int), orderId);
                parameters.Add(new SqlParameter("@p_Paid", SqlDbType.Bit), (paid) ? 1 : 0);
                parameters.Add(new SqlParameter("@p_UserId", SqlDbType.Int), userId);

                return new DAL.Database().ExecuteNonQueryOnly("Sps_MarkOrderPaidUnPaid", parameters);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public bool MarkOrdersPaid(Int32 tableId, Int32 restaurantId, Int32 userId)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_TblId", SqlDbType.Int), tableId);
                parameters.Add(new SqlParameter("@p_ResId", SqlDbType.Int), restaurantId);
                parameters.Add(new SqlParameter("@p_UserId", SqlDbType.Int), userId);

                return new DAL.Database().ExecuteNonQueryOnly("Sps_MarkOrdersPaid", parameters);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public bool MarkOrdersCancel(Int32 tableId, Int32 restaurantId, Int32 userId)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_TblId", SqlDbType.Int), tableId);
                parameters.Add(new SqlParameter("@p_ResId", SqlDbType.Int), restaurantId);
                parameters.Add(new SqlParameter("@p_UserId", SqlDbType.Int), userId);

                return new DAL.Database().ExecuteNonQueryOnly("Sps_MarkOrdersCancel", parameters);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public DataTable GetRestaurants(Int32 restaurantId)
        {
            try
            {
                return new DAL.Database().ExecuteForDataTable(string.Format(@"SELECT * FROM Restaurants WHERE Id={0}", restaurantId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetProductCategories(string where)
        {
            try
            {
                return new DAL.Database().ExecuteForDataTable(@"SELECT * FROM ProductCategories WHERE 1=1 " + where + "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool InsertFoodProductCategory(Objects.ProductCategories obj, Int32 userId)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_UserId", SqlDbType.Int), userId);
                parameters.Add(new SqlParameter("@p_RestaurantId", SqlDbType.Int), obj.RestaurantId);
                parameters.Add(new SqlParameter("@p_Name", SqlDbType.NVarChar, 50), obj.CategoryName);
                parameters.Add(new SqlParameter("@p_Discription", SqlDbType.NVarChar, 500), obj.Discription);
                parameters.Add(new SqlParameter("@p_DisplayOrder", SqlDbType.Int), obj.DisplayOrder);

                return new DAL.Database().ExecuteNonQueryOnly("Sps_InsertProductCategory", parameters);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public bool UpdateFoodProductCategory(Objects.ProductCategories obj, Int32 userId)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_Id", SqlDbType.Int), obj.Id);
                parameters.Add(new SqlParameter("@p_UserId", SqlDbType.Int), userId);
                parameters.Add(new SqlParameter("@p_RestaurantId", SqlDbType.Int), obj.RestaurantId);
                parameters.Add(new SqlParameter("@p_Name", SqlDbType.NVarChar, 50), obj.CategoryName);
                parameters.Add(new SqlParameter("@p_Discription", SqlDbType.NVarChar, 500), obj.Discription);
                parameters.Add(new SqlParameter("@p_DisplayOrder", SqlDbType.Int), obj.DisplayOrder);

                return new DAL.Database().ExecuteNonQueryOnly("Sps_UpdateProductCategory", parameters);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public bool DeleteFoodProductCategory(Int32 Id, Int32 userId)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_Id", SqlDbType.Int), Id);
                parameters.Add(new SqlParameter("@p_UserId", SqlDbType.Int), userId);
                
                return new DAL.Database().ExecuteNonQueryOnly("Sps_DeleteProductCategory", parameters);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public Int32? CountQRTblId(Int32 resId, string qrCodeStr)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_RestaurantId", SqlDbType.Int), resId);
                parameters.Add(new SqlParameter("@p_QRCodeStr", SqlDbType.NVarChar, 10), qrCodeStr);

                return new DAL.Database().ExecuteForInt("Sps_CountQRTblId", parameters);

            }
            catch (Exception ex)
            {

                throw ex;
                //return false;
            }
        }
        public string GetTableQRCode(int restaurantId, int tableId)
        {
            try
            {
                return new DAL.Database().ExecuteForStr(@"SELECT QRCodeStr FROM RestaurantTables WHERE RestaurantId=" + restaurantId + " AND Id=" + tableId + "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string[] GetBrandingLogos(Int32 restaurantId)
        {
            try
            {
                DataTable dtLogos= new DAL.Database().ExecuteForDataTable(@"SELECT LogoPath,LogoSmallPath FROM Restaurants WHERE Id=" + restaurantId + "");
                if (dtLogos!=null&&dtLogos.Rows.Count>0)
                {
                    string[] logos=new string[2];
                    logos[0] = dtLogos.Rows[0]["LogoPath"].ToString();
                    logos[1] = dtLogos.Rows[0]["LogoSmallPath"].ToString();
                    return logos;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateBrandingLogos(Objects.Restaurant obj, Int32 userId)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_Id", SqlDbType.Int), obj.Id);
                parameters.Add(new SqlParameter("@p_UserId", SqlDbType.Int), userId);
                parameters.Add(new SqlParameter("@p_LogoPath", SqlDbType.NVarChar, 500), (!string.IsNullOrEmpty(obj.LogoPath))? obj.LogoPath:"0");
                parameters.Add(new SqlParameter("@p_LogoSmallPath", SqlDbType.NVarChar, 500), (!string.IsNullOrEmpty(obj.LogoSmallPath))? obj.LogoSmallPath:"0");

                return new DAL.Database().ExecuteNonQueryOnly("Sps_UpdateBrandingLogos", parameters);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public DataTable GetClientsSummaryForRestaurant(Int32 restaurantId)
        {
            try
            {
                return new DAL.Database().ExecuteForDataTable(string.Format(@"
                SELECT USR.Id AS UserId, USR.Name AS ClientName, USR.Email, USR.[Enable], USR.EmailVerify, RESTCLI.Id AS ClientId, USR.LastLogin FROM Users USR 
                INNER JOIN Orders ORD ON ORD.OrderBy=USR.Id 
                INNER JOIN RestaurantClients RESTCLI ON RESTCLI.UserId=USR.Id 
                WHERE 1=1 AND ORD.RestaurantId={0}
                GROUP BY USR.Id,USR.Name,USR.Email,USR.[Enable],USR.EmailVerify,RESTCLI.Id,USR.LastLogin
                ", restaurantId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int32 CountClientOrders(Int32 restaurantId,Int32 clientUserId)
        {
            try
            {
                return new DAL.Database().ExecuteForInt(@"SELECT COUNT(ORD.Id) FROM Orders ORD 
                INNER JOIN RestaurantClients RESCLI ON RESCLI.UserId=ORD.OrderBy 
                WHERE ORD.RestaurantId="+restaurantId+" AND ORD.OrderBy="+clientUserId+"");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetClientOrdersDetails(Int32 restaurantId, Int32 clientUserId)
        {
            try
            {
                return new DAL.Database().ExecuteForDataTable(string.Format(@"
                 SELECT ORD.Id, ORD.OrderPlacedDTime, ORD.Paid, ORD.Qty, ORD.Note, ORDSTAT.Details AS OrderStatus
                , PRO.Name AS ProductName, PRO.Discription AS ProductDescription, PRO.Price, PROCAT.CategoryName
                , RESTBL.Name AS TableName FROM Orders ORD
                INNER JOIN RestaurantTables RESTBL ON RESTBL.Id = ORD.TableId
                INNER JOIN Products PRO ON PRO.Id = ORD.ProductId
                INNER JOIN ProductCategories PROCAT ON PROCAT.Id = PRO.CategoryId
                INNER JOIN OrderStatusNames ORDSTAT ON ORDSTAT.Id = ORD.StatusNameId
                WHERE ORD.RestaurantId = {0} AND ORD.OrderBy = {1}
                ", restaurantId, clientUserId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}