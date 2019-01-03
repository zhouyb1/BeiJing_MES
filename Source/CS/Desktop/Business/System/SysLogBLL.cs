using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DataAccess.SqlServer;
using Model;

namespace Business.System
{
    public class SysLogBLL
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public int WriteLog(SysLog log)
        {
            try
            {
                string sql = @"INSERT INTO [Sys_Log]
           ([L_Date]
           ,[L_User]
           ,[L_Module]
           ,[L_Button]
           ,[L_Key]
           ,[L_Result]
           ,[L_Describe])
     VALUES
           (@L_Date
           ,@L_User
           ,@L_Module
           ,@L_Button
           ,@L_Key
           ,@L_Result
           ,@L_Describe)";

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@L_Date", log.L_Date));
                parameters.Add(new SqlParameter("@L_User", log.L_User));
                parameters.Add(new SqlParameter("@L_Module", log.L_Module));
                parameters.Add(new SqlParameter("@L_Button", log.L_Button));
                parameters.Add(new SqlParameter("@L_Key", log.L_Key));
                parameters.Add(new SqlParameter("@L_Result", log.L_Result));
                parameters.Add(new SqlParameter("@L_Describe", log.L_Describe));

                if (!string.IsNullOrEmpty(log.L_Describe))
                {
                    parameters.Add(new SqlParameter("@A_Parent", log.L_Describe));
                }
                else
                {
                    parameters.Add(new SqlParameter("@A_Parent", DBNull.Value));
                }

                SqlHelper db = new SqlHelper();
                var row = db.ExecuteNonQuery(sql, parameters.ToArray());

                return row;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}