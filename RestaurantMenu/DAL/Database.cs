using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace RestaurantMenu.DAL
{
    //      ExecuteScalar() only returns the value from the first column of the first row of your query.
    //      ExecuteReader() returns an object that can iterate over the entire result set while only keeping one record in memory at a time.
    //      ExecuteNonQuery() does not return data at all: only the number of rows affected by an insert, update, or delete.

    //      The @@identity function returns the last identity created in the same session.
    //      The scope_identity() function returns the last identity created in the same session and the same scope.
    //      The ident_current(name) returns the last identity created for a specific table or view in any session.
    //      The identity() function is not used to get an identity, it's used to create an identity in a select...into query.
    public class Database
    {
        ListDictionary parameters = new ListDictionary();
        public string GetConnectionString()
        {
            try
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public bool ExecuteNonQueryOnly(string strQry)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd;
            con.ConnectionString = GetConnectionString();

            try
            {
                con.Open();
                cmd = new SqlCommand(strQry);
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 1000;
                if ((int)cmd.ExecuteNonQuery() > 0)
                {
                    return (true);
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
            finally
            {
                con.Close();
            }
        }

        public bool ExecuteNonQueryOnly(string proName, System.Collections.Specialized.ListDictionary Parameters)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd;
            con.ConnectionString = GetConnectionString();
            try
            {
                con.Open();
                cmd = new SqlCommand(proName);
                cmd.CommandType = CommandType.StoredProcedure;
                IDataParameter p;
                if (Parameters != null)
                {
                    foreach (System.Collections.DictionaryEntry param in Parameters)
                    {
                        p = param.Key as IDataParameter;
                        if (null == p)
                        {
                            p.ParameterName = (string)param.Key;
                            p.Value = param.Value;
                        }
                        else
                        {
                            p.Value = param.Value;
                        }
                        cmd.Parameters.Add(p);
                    }
                }
                cmd.Connection = con;
                cmd.CommandTimeout = 1000;
                if ((int)cmd.ExecuteNonQuery() > 0)
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
            finally
            {
                con.Close();
            }
        }

        public bool ExecuteScalerOnly(string strQry)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd;
            con.ConnectionString = GetConnectionString();

            try
            {
                con.Open();
                cmd = new SqlCommand(strQry);
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 1000;
                cmd.ExecuteScalar();
                return (true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public DataSet ExecuteForDataSet(string strQuery)
        {
            SqlConnection con = new SqlConnection();
            SqlDataAdapter myAdapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlCommand cmd;
            con.ConnectionString = GetConnectionString();
            try
            {
                con.Open();
                cmd = new SqlCommand(strQuery);
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                myAdapter.SelectCommand = cmd;
                cmd.CommandTimeout = 1000;
                myAdapter.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public DataSet ExecuteForDataSet(string strQuery, System.Collections.Specialized.ListDictionary Parameters)
        {
            SqlConnection con = new SqlConnection();
            SqlDataAdapter myAdapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlCommand cmd;
            con.ConnectionString = GetConnectionString();
            try
            {
                con.Open();
                cmd = new SqlCommand(strQuery);
                cmd.CommandType = CommandType.StoredProcedure;
                IDataParameter p;
                if (Parameters != null)
                {
                    foreach (System.Collections.DictionaryEntry param in Parameters)
                    {
                        p = param.Key as IDataParameter;
                        if (null == p)
                        {
                            p.ParameterName = (string)param.Key;
                            p.Value = param.Value;
                        }
                        else
                        {
                            p.Value = param.Value;
                        }
                        cmd.Parameters.Add(p);
                    }
                }
                cmd.Connection = con;
                myAdapter.SelectCommand = cmd;
                cmd.CommandTimeout = 1000;
                cmd.ExecuteNonQuery();
                myAdapter.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public DataRow ExecuteForDataRow(string strQuery)
        {
            SqlConnection con = new SqlConnection();
            SqlDataAdapter myAdapter = new SqlDataAdapter();
            DataSet ds = new DataSet();

            SqlCommand cmd;
            DataRow myDataRow = null;
            con.ConnectionString = GetConnectionString();

            try
            {
                con.Open();

                cmd = new SqlCommand(strQuery);
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 1000;
                myAdapter.SelectCommand = cmd;
                myAdapter.Fill(ds);

                System.Data.DataTable dataTable = (ds != null && ds.Tables.Count > 0) ? ds.Tables[0] : null;
                myDataRow = (dataTable != null && dataTable.Rows.Count > 0) ? myDataRow = ds.Tables[0].Rows[0] : null;

                return myDataRow;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        public bool AuthenticateActiveUser(string valEmail, string valPassword)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_Email", SqlDbType.NVarChar, 100), valEmail);
                parameters.Add(new SqlParameter("@p_Password", SqlDbType.NVarChar, 500), valPassword);

                DataTable dt = ExecuteForDataTable("Sps_AuthenticateActiveUser", parameters);
                if (dt!=null&& dt.Rows.Count > 0)
                {
                    return (true);
                }
                else
                {
                    return (false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable AuthenticateActiveUserGetData(string valEmail, string valPassword)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_Email", SqlDbType.NVarChar, 100), valEmail);
                parameters.Add(new SqlParameter("@p_Password", SqlDbType.NVarChar, 500), valPassword);

                return ExecuteForDataTable("Sps_AuthenticateActiveUser", parameters);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ExecuteForDataTable(string strQuery)
        {
            SqlConnection con = new SqlConnection();
            SqlDataAdapter myAdapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlCommand cmd;
            con.ConnectionString = GetConnectionString();
            try
            {
                con.Open();
                cmd = new SqlCommand(strQuery);
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                myAdapter.SelectCommand = cmd;
                cmd.CommandTimeout = 1000;
                myAdapter.Fill(ds);

                DataTable dt = null;
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables.Count > 1)
                    {
                        if (ds.Tables[0].Columns.Count > 1)
                        {
                            dt = ds.Tables[0];
                        }
                        else
                        {
                            dt = ds.Tables[1];
                        }
                    }
                    else
                    {
                        dt = ds.Tables[0];
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable ExecuteForDataTable(string strQuery, System.Collections.Specialized.ListDictionary Parameters)
        {
            SqlConnection con = new SqlConnection();
            SqlDataAdapter myAdapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlCommand cmd;
            con.ConnectionString = GetConnectionString();
            try
            {
                con.Open();
                cmd = new SqlCommand(strQuery);
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;

                IDataParameter p;
                if (Parameters != null)
                {
                    foreach (System.Collections.DictionaryEntry param in Parameters)
                    {
                        p = param.Key as IDataParameter;
                        if (null == p)
                        {
                            p.ParameterName = (string)param.Key;
                            p.Value = param.Value;
                        }
                        else
                        {
                            p.Value = param.Value;
                        }
                        cmd.Parameters.Add(p);
                    }
                }

                myAdapter.SelectCommand = cmd;
                cmd.CommandTimeout = 1000;
                myAdapter.Fill(ds);

                DataTable dt = null;
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables.Count > 1)
                    {
                        if (ds.Tables[0].Columns.Count > 1)
                        {
                            dt = ds.Tables[0];
                        }
                        else
                        {
                            dt = ds.Tables[1];
                        }
                    }
                    else
                    {
                        dt = ds.Tables[0];
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public int ExecuteForInt(string strQuery)
        {
            try
            {
                DataSet ds = ExecuteForDataSet(strQuery);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()) || Regex.IsMatch(ds.Tables[0].Rows[0][0].ToString(), @"[a-zA-Z]"))
                    {
                        return -0;
                    }
                    else
                    {
                        return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    }
                    
                }
                else
                {
                    return -0;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public Int32? ExecuteForInt(string strQuery, System.Collections.Specialized.ListDictionary Parameters)
        {
            try
            {
                DataSet ds = ExecuteForDataSet(strQuery, Parameters);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()) || Regex.IsMatch(ds.Tables[0].Rows[0][0].ToString(), @"[a-zA-Z]"))
                    {
                        return null;
                    }
                    else
                    {
                        return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    }
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
        public bool ExecuteForBool(string strQuery)
        {
            try
            {
                DataSet ds = ExecuteForDataSet(strQuery);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()) || Regex.IsMatch(ds.Tables[0].Rows[0][0].ToString(), @"[a-zA-Z]"))
                    {
                        return false;
                    }
                    else
                    {
                        return Convert.ToBoolean(ds.Tables[0].Rows[0][0]);
                    }
                }
                else
                {
                    return false;
                }               

            }
            catch (Exception exception)
            {
                
                throw exception;
            }
        }
        public bool? ExecuteForBool(string strQuery, System.Collections.Specialized.ListDictionary Parameters)
        {
            try
            {
                DataSet ds = ExecuteForDataSet(strQuery, Parameters);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()) || Regex.IsMatch(ds.Tables[0].Rows[0][0].ToString(), @"[a-zA-Z]"))
                    {
                        return null;
                    }
                    else
                    {
                        return Convert.ToBoolean(ds.Tables[0].Rows[0][0]);
                    }                    
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
        public double? ExecuteForDouble(string strQuery)
        {
            try
            {
                DataSet ds = ExecuteForDataSet(strQuery);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()) || Regex.IsMatch(ds.Tables[0].Rows[0][0].ToString(),@"[a-zA-Z]"))
                    {
                        return null;
                    }
                    else
                    {
                        return Convert.ToDouble(ds.Tables[0].Rows[0][0]);
                    }
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
        public string ExecuteForStr(string strQuery)
        {
            try
            {
                DataSet ds = ExecuteForDataSet(strQuery);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToString(ds.Tables[0].Rows[0][0]);
                }
                else
                {
                    return string.Empty;
                }
                
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}