using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace DataAccess.Oracle
{
    public sealed partial class OracleHelper
    {
        public static T ExecuteObject<T>(string commandText, params OracleParameter[] parms)
        {
            DataSet ds = Query(commandText, parms);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return AutoMapper.Mapper.DynamicMap<List<T>>(ds.Tables[0].CreateDataReader()).FirstOrDefault();
            }
            else
            {
                return default(T);
            }
        }

        public static List<T> ExecuteObjects<T>(string commandText, params OracleParameter[] parms)
        {
            DataSet ds = Query(commandText, parms);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return AutoMapper.Mapper.DynamicMap<List<T>>(ds.Tables[0].CreateDataReader());
            }
            else
            {
                return default(List<T>);
            }
        }
    }
}
