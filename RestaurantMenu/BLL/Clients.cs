using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RestaurantMenu.BLL
{
    public class Clients
    {
        ListDictionary parameters = new ListDictionary();
        public bool ChangeClientStatus(Int32 clientId, bool status,Int32 statusChangedBy)
        {
            try
            {
                parameters.Add(new SqlParameter("@p_ClientId", SqlDbType.Int), clientId);
                parameters.Add(new SqlParameter("@p_Status", SqlDbType.Bit), (status)?1:0);
                parameters.Add(new SqlParameter("@p_StatusChangedBy", SqlDbType.Int), statusChangedBy);

                return new DAL.Database().ExecuteNonQueryOnly("Sps_ChangeClientStatus", parameters);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}