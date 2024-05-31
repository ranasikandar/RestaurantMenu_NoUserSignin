using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RestaurantMenu.BLL
{
    public class Users
    {
        ListDictionary parameters = new ListDictionary();
        public DataTable AuthenticateActiveUserGetData(string _email, string _password)
        {
            try
            {
                DataTable dt = new DAL.Database().AuthenticateActiveUserGetData(_email, _password);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public bool AuthenticateActiveUser(string _email, string _password)
        {
            try
            {
                return new DAL.Database().AuthenticateActiveUser(_email, _password);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void InsertLastLogin(Int32 userId)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_UserID", SqlDbType.Int), userId);

                new DAL.Database().ExecuteNonQueryOnly("Sps_Insert_LastLogin", parameters);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public bool CheckEmailExist(string valEmail)//assume email is already senetized to prevent sql injections
        {
            try
            {
                DataTable dt = new DAL.Database().ExecuteForDataTable(@"SELECT Id FROM Users WHERE Email='" + valEmail + "'");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetUsers(string where)//check sql inj
        {
            try
            {
                string sql = string.Format(@"SELECT * FROM [Users] WHERE 1=1 {0}", where);//ORDER BY [Name]

                DataTable dt = new DAL.Database().ExecuteForDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    return dt;
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
        public bool UserRestaurantSignup(Objects.User obj)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_Name", SqlDbType.NVarChar, 50), obj.Name);
                parameters.Add(new SqlParameter("@p_Email", SqlDbType.NVarChar, 100), obj.Email);
                parameters.Add(new SqlParameter("@p_Password", SqlDbType.NVarChar, 500), obj.Password);
                parameters.Add(new SqlParameter("@p_LostPassKey", SqlDbType.VarChar, 10), obj.LostPassKey);

                return new DAL.Database().ExecuteNonQueryOnly("Sps_UserRestaurant_Signup", parameters);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public bool UserRestaurantEmailVerification(string _email, string _key)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_Email", SqlDbType.NVarChar, 100), _email);
                parameters.Add(new SqlParameter("@p_LostPassKey", SqlDbType.VarChar, 10), _key);

                return new DAL.Database().ExecuteNonQueryOnly("Sps_UserRestaurantEmailVerification", parameters);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public bool UserWaiterEmailVerification(string _email, string _key)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_Email", SqlDbType.NVarChar, 100), _email);
                parameters.Add(new SqlParameter("@p_LostPassKey", SqlDbType.VarChar, 10), _key);

                return new DAL.Database().ExecuteNonQueryOnly("Sps_UserWaiterEmailVerification", parameters);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public bool ForgotUserPassword(string valEmail, string Pass)
        {
            try
            {
                return new DAL.Database().ExecuteNonQueryOnly(@"UPDATE [Users] SET [LostPassKey]='" + Pass + "' WHERE [Email]='" + valEmail + "'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ChangePassword(int id, string password)
        {
            try
            {
                string sql = string.Format(@"UPDATE Users SET Password='{0}' WHERE Id={1}", password, id);

                return new DAL.Database().ExecuteNonQueryOnly(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool PasswordReset(string email, string key, string Pass)
        {
            try
            {
                return new DAL.Database().ExecuteNonQueryOnly(@"UPDATE [Users] SET [LostPassKey]=NULL, [Password]='" + Pass + "' WHERE [Email]='" + email + "' AND [LostPassKey]='" + key + "'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetRestaurantUsers(string where)//check sql inj
        {
            try
            {
                string sql = string.Format(@"SELECT USR.Id AS UserID, USR.Name AS UserName, USR.Email, USR.[Password], USR.[Enable], USR.LastLogin, USR.LostPassKey
                                            , USR.Details, USR.EmailVerify, REST.Id AS RestaurantID, REST.Name AS RestaurantName, REST.RegisterDate, REST.Country
                                            , REST.City, REST.[Address], REST.Phone, REST.NotificationEmail, REST.ValidityDate, REST.AllowedTables, REST.CurrencyCode, REST.SendEmailNotification FROM [Users] USR 
                                            INNER JOIN [RestaurantUsers] REUSR ON USR.Id=REUSR.UserId 
                                            INNER JOIN [Restaurants] REST ON REUSR.RestaurantId=REST.Id 
                                            WHERE 1=1 {0}", where);

                DataTable dt = new DAL.Database().ExecuteForDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    return dt;
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
        public bool UpdateRestaurantsByAdmin(DateTime validity, bool enableUser, Int32 restaurantID, string details)
        {
            bool valid1; bool valid2;
            try
            {
                valid1 = new DAL.Database().ExecuteNonQueryOnly(@"UPDATE Restaurants SET ValidityDate=CONVERT(DATE,'" + validity.ToString("yyyy-MM-dd") + "',23) WHERE Id=" + restaurantID + "");
                valid2 = new DAL.Database().ExecuteNonQueryOnly(
                string.Format(@"UPDATE Users SET [Enable]={0}, Details='{2}' WHERE Id=(SELECT UserId FROM RestaurantUsers WHERE RestaurantId={1})", (enableUser) ? "1" : "0", restaurantID, details));

                if (valid1 && valid2)
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
        public bool RestaurantEmailVerified(string andWhere)
        {
            try
            {
                return new DAL.Database().ExecuteForBool(@"SELECT Id FROM [Users] WHERE [EmailVerify]=1 AND [Type]=2 "+andWhere+"");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetLog(string top, string where)
        {
            try
            {
                string sql = string.Format(@"SELECT {0} USR.Name,UST.UserType,USL.LogDateTime,USL.LogDetail FROM UserLogs USL
                                            INNER JOIN Users USR ON USL.UserID=USR.Id 
                                            INNER JOIN UserTypes UST ON USR.[Type]=UST.Id WHERE 1=1 {1}", top, where);

                DataTable dt = new DAL.Database().ExecuteForDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt;
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
        public DataTable GetWaiterUsers(string where)
        {
            try
            {
                string sql = string.Format(@"SELECT USR.Id AS UserId, RESW.Id, USR.Name, USR.Email, USR.[Password], USR.[Enable], USR.Details FROM Users USR 
                INNER JOIN RestaurantWaiters RESW ON RESW.UserId=USR.Id 
                WHERE 1=1 {0}", where);//AND USR.[Enable]=1 AND USR.[Type]=3 AND RESW.RestaurantId=6 ORDER BY USR.Name

                DataTable dt = new DAL.Database().ExecuteForDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    return dt;
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
        public string GetUserEmail(Int32 userId)
        {
            try
            {
                return new DAL.Database().ExecuteForStr(@"SELECT Email FROM [Users] WHERE [Id]=" + userId+ "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool InsertWaiterUser(Objects.Waiter obj,Int32 userId)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_ByUserId", SqlDbType.Int), userId);
                parameters.Add(new SqlParameter("@p_Name", SqlDbType.NVarChar, 50), obj.Name);
                parameters.Add(new SqlParameter("@p_Email", SqlDbType.NVarChar, 100), obj.Email);
                parameters.Add(new SqlParameter("@p_Password", SqlDbType.NVarChar, 500), obj.Password);
                parameters.Add(new SqlParameter("@p_Enable", SqlDbType.Bit), (obj.Enable)?1:0);
                parameters.Add(new SqlParameter("@p_LostPassKey", SqlDbType.VarChar, 10), obj.LostPassKey);
                parameters.Add(new SqlParameter("@p_Details", SqlDbType.NVarChar, 500), obj.Details);
                parameters.Add(new SqlParameter("@p_RestaurantId", SqlDbType.Int), obj.RestaurantId);

                return new DAL.Database().ExecuteNonQueryOnly("Sps_Insert_UserWaiter", parameters);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public bool UpdateWaiterUser(Objects.Waiter obj, Int32 userId, bool waiterEmailUpdate)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_UserId", SqlDbType.Int), obj.UserId);
                parameters.Add(new SqlParameter("@p_ByUserId", SqlDbType.Int), userId);
                parameters.Add(new SqlParameter("@p_Name", SqlDbType.NVarChar, 50), obj.Name);
                parameters.Add(new SqlParameter("@p_Email", SqlDbType.NVarChar, 100), obj.Email);
                parameters.Add(new SqlParameter("@p_Password", SqlDbType.NVarChar, 500), obj.Password);
                parameters.Add(new SqlParameter("@p_Enable", SqlDbType.Bit), (obj.Enable) ? 1 : 0);
                parameters.Add(new SqlParameter("@p_LostPassKey", SqlDbType.VarChar, 10), obj.LostPassKey);
                parameters.Add(new SqlParameter("@p_Details", SqlDbType.NVarChar, 500), obj.Details);
                parameters.Add(new SqlParameter("@p_RestaurantId", SqlDbType.Int), obj.RestaurantId);
                parameters.Add(new SqlParameter("@p_WaiterEmailUpdate", SqlDbType.Bit), (waiterEmailUpdate) ? 1 : 0);

                return new DAL.Database().ExecuteNonQueryOnly("Sps_Update_UserWaiter", parameters);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public bool DeleteUserWaiter(Int32 Id, Int32 byUserId)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_Id", SqlDbType.Int), Id);
                parameters.Add(new SqlParameter("@p_ByUserId", SqlDbType.Int), byUserId);

                return new DAL.Database().ExecuteNonQueryOnly("Sps_DeleteUserWaiter", parameters);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public DataTable GetClients(string where)
        {
            try
            {
                string sql = string.Format(@"SELECT USR.Id AS UserId, RESC.Id, USR.Name, USR.Email, RESC.CardNumber, USR.[Password], USR.[Enable], USR.LastLogin, USR.Details,USR.EmailVerify FROM Users USR 
                INNER JOIN RestaurantClients RESC ON RESC.UserId=USR.Id 
                WHERE 1=1 {0}", where);//AND USR.[Type]=4 ORDER BY USR.Name

                DataTable dt = new DAL.Database().ExecuteForDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    return dt;
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
        public bool UserRestaurantClientSignup(Objects.RestaurantClient obj)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_Name", SqlDbType.NVarChar, 50), obj.Name);
                parameters.Add(new SqlParameter("@p_Email", SqlDbType.NVarChar, 100), obj.Email);
                parameters.Add(new SqlParameter("@p_Password", SqlDbType.NVarChar, 500), obj.Password);
                parameters.Add(new SqlParameter("@p_LostPassKey", SqlDbType.VarChar, 10), obj.LostPassKey);

                parameters.Add(new SqlParameter("@p_CC", SqlDbType.NVarChar, 50), (obj.CardNumber.Length>0)?obj.CardNumber:null);
                parameters.Add(new SqlParameter("@p_CCExpMonth", SqlDbType.Int), (obj.CCExpMonth>0)?obj.CCExpMonth:0);
                parameters.Add(new SqlParameter("@p_CCExpYear", SqlDbType.Int), (obj.CCExpYear>0)?obj.CCExpYear:0);
                parameters.Add(new SqlParameter("@p_CVV", SqlDbType.NVarChar, 10), (obj.CVV.Length>0)?obj.CVV:null);

                return new DAL.Database().ExecuteNonQueryOnly("Sps_UserRestaurantClient_Signup", parameters);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public DataTable GetClientsDetails(string where)//check sql inj
        {
            try
            {
                string sql = string.Format(@"SELECT USR.*,RESCLI.Id AS ClientId,RESCLI.CardNumber,RESCLI.CCExpMonth,RESCLI.CCExpYear,RESCLI.CVV FROM [Users] USR 
                INNER JOIN [RestaurantClients] RESCLI ON RESCLI.UserId=USR.Id 
                WHERE 1=1 {0}", where);

                DataTable dt = new DAL.Database().ExecuteForDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    return dt;
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
        public bool UserRestaurantClientProfileUpdate(Objects.RestaurantClient obj)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_UserId", SqlDbType.Int), obj.UserId);
                parameters.Add(new SqlParameter("@p_Name", SqlDbType.NVarChar, 50), obj.Name);
                parameters.Add(new SqlParameter("@p_CC", SqlDbType.NVarChar, 50), (obj.CardNumber.Length > 0) ? obj.CardNumber : null);
                parameters.Add(new SqlParameter("@p_CCExpMonth", SqlDbType.Int), (obj.CCExpMonth > 0) ? obj.CCExpMonth : 0);
                parameters.Add(new SqlParameter("@p_CCExpYear", SqlDbType.Int), (obj.CCExpYear > 0) ? obj.CCExpYear : 0);
                parameters.Add(new SqlParameter("@p_CVV", SqlDbType.NVarChar, 10), (obj.CVV.Length > 0) ? obj.CVV : null);

                return new DAL.Database().ExecuteNonQueryOnly("Sps_UserRestaurantClient_Update", parameters);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public bool UserClientEmailVerification(string _email, string _key)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_Email", SqlDbType.NVarChar, 100), _email);
                parameters.Add(new SqlParameter("@p_LostPassKey", SqlDbType.VarChar, 10), _key);

                return new DAL.Database().ExecuteNonQueryOnly("Sps_UserClientEmailVerification", parameters);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}