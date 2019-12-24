using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp
{
    class Globels
    {
        public static string strSupplyCode = ""; //供应商编码
        public static string strUser = ""; //登录用户
        public static string strName = ""; //登录名称
        public static string strOrderNo = "";//订单编号
        public static string strWorkShop = ""; //车间编码
        public static string strWorkShopName = "";
        public static string strRecord = ""; //工艺代码
        public static string strRecordName = "";
        public static string strProce = ""; //工序
        public static string strProceName = "";

        public static string strTeam = ""; //班组
        public static string strStockCode = "";//仓库

        public static string strCom = "COM1"; //串口
        //public static string strProce = "01"; //工序
    //    public class BaseClass
    //    {
    //        public string connString = ConfigurationManager.AppSettings["connectionstring"].ToString();
    //        public SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connectionstring"].ToString());

    //        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
    //        {
    //            if (conn.State != ConnectionState.Open)
    //                conn.Open();
    //            cmd.Connection = conn;
    //            cmd.CommandText = cmdText;
    //            cmd.CommandTimeout = 600;
    //            if (trans != null)
    //                cmd.Transaction = trans;
    //            cmd.CommandType = CommandType.Text;//cmdType;
    //            if (cmdParms != null)
    //            {
    //                foreach (SqlParameter parameter in cmdParms)
    //                {
    //                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
    //                        (parameter.Value == null))
    //                    {
    //                        parameter.Value = DBNull.Value;
    //                    }
    //                    cmd.Parameters.Add(parameter);
    //                }
    //            }
    //        }
    //        ////////////////////////////////////////存取过程///////////////////////////////////////////////////
    //        /// <summary>
    //        /// 调用存储过程
    //        /// </summary>
    //        /// <param name="storedProcName">存储过程名称</param>
    //        /// <param name="parameters">参数</param>
    //        /// <returns>SqlDataReader</returns>
    //        public static SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
    //        {
    //            SqlConnection connection = new SqlConnection(connString);
    //            SqlDataReader returnReader;
    //            connection.Open();
    //            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
    //            command.CommandType = CommandType.StoredProcedure;
    //            returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
    //            return returnReader;
    //        }


    //        /// <summary>
    //        /// 执行存储过程
    //        /// </summary>
    //        /// <param name="connection">连接字符串</param>
    //        /// <param name="storedProcName">存储过程名称</param>
    //        /// <param name="parameters">参数</param>
    //        /// <returns>SqlCommand</returns>
    //        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
    //        {
    //            SqlCommand command = new SqlCommand(storedProcName, connection);
    //            command.CommandType = CommandType.StoredProcedure;
    //            foreach (SqlParameter parameter in parameters)
    //            {
    //                if (parameter != null)
    //                {
    //                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
    //                        (parameter.Value == null))
    //                    {
    //                        parameter.Value = DBNull.Value;
    //                    }
    //                    command.Parameters.Add(parameter);
    //                }
    //            }

    //            return command;
    //        }


    //    }
    }
}
