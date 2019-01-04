using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Oracle;
using DataAccess.SqlServer;
using Model;

namespace Business
{
    public class SysModuleBLL
    {
        public List<SysModule> loadData(string role)
        {
            try
            {
               
                string sql = @"SELECT Sys_Module.M_Code,
    Sys_Module.M_Name,
    Sys_Module.M_ParentCode,
    Sys_Module.M_Remark,
    Sys_Module.M_Active,
    Sys_Module.M_CreateBy,
    Sys_Module.M_CreateDate,
    Sys_Module.M_UpdateBy,
    Sys_Module.M_UpdateDate,
    (CASE
         WHEN Sys_ModulePower.M_Code IS NULL THEN
             0
         ELSE
             1
     END
    ) M_Choice
FROM Sys_Module
    LEFT JOIN Sys_ModulePower
        ON Sys_ModulePower.M_Code = Sys_Module.M_Code
           AND Sys_ModulePower.R_Code = '{0}'
WHERE Sys_Module.M_Active = 1";

              

                SqlHelper db = new SqlHelper();
                var rows = db.ExecuteObjects<SysModule>(string.Format(sql,role));

                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int saveData(string role, List<SysModule> modules)
        {

            SqlTrans trans = new SqlTrans();
            SqlConnection con = trans.TranConn(); ;
            SqlTransaction tran= trans.TransBegin(con);;
            try
            {
                string sql = @"DELETE FROM [Sys_ModulePower] WHERE  R_Code='{0}'";
                SqlHelper.ExecuteNonQuery(tran, CommandType.Text, string.Format(sql, role));

                string insert = @"INSERT INTO [Sys_ModulePower]
           (ID,R_Code,M_Code)
     VALUES
           (newid(),'{0}','{1}')";

                foreach (var row in modules)
                {
                    SqlHelper.ExecuteNonQuery(tran, CommandType.Text, string.Format(insert, role, row.M_Code));
                }

                trans.TransCommit(tran);
                trans.TransEnd(con);

                return 1;
            }
            catch (Exception ex)
            {
                trans.TransRollback(tran);
                trans.TransEnd(con);

                throw;
            }
        }


        /// <summary>
        /// 加载用户模块
        /// </summary>
        /// <returns></returns>
        public List<SysModule> LoadRoleModule(string role)
        {
            try
            {

                string sql = @"SELECT M_Code FROM Sys_ModulePower WHERE R_Code='{0}'";

                SqlHelper db = new SqlHelper();
                var rows = db.ExecuteObjects<SysModule>(string.Format(sql, role));

                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}