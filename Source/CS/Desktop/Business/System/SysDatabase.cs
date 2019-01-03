using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccess.SqlServer;
using DataAccess.SqlServer.Extend;
using Tools;

namespace Business
{
    public class SysDatabase
    {
         /// <summary>
         /// 数据备份
         /// </summary>
         /// <param name="filename">备份路径</param>
         /// <param name="format">是否格式化</param>
         /// <returns></returns>
        public static bool Backup(string filename, bool format)
        {
            try
            {
                string sql = "";

                //格式化还原
                if (format)
                {

                    sql = string.Format("backup database DesktopApp to disk = '{0}' with format", filename);
                }
                //一般还原
                else
                {
                    sql = string.Format("backup database DesktopApp to disk = '{0}' with noformat", filename);

                }
                SqlHelper db = new SqlHelper();
                int row = db.ExecuteNonQuery(sql);

                if (row > 0)
                    return true;
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 还原数据库
        /// </summary>
        /// <param name="filename">备份文件的完整文件名</param>
        /// <param name="file">所要还原的备份集</param>
        /// <returns></returns>
        public static bool RestoeData(string filename, int file)
        {
            try
            {
                //杀死所有对数据库访问的进程
                string sqlKill = "exec proc_Kill 'DesktopApp'";
                SqlHelper dbKill = new SqlHelper(Tools.ConfigManager.ReadValueByKey(ConfigurationFile.AppConfig, "restore"));
                int row = dbKill.ExecuteNonQuery(sqlKill);

                //还原数据库
                if (row > 0)
                {
                    SqlHelper dbRestore = new SqlHelper();
                    string sqlRestore = string.Format(" use master restore database DesktopApp from disk='{0}'  with replace,file={1} ", filename, file);
                    var result = dbRestore.ExecuteNonQuery(sqlRestore);

                    if (result > 0)
                        return true;
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                
                throw;
            }

        }


        /// <summary>
        /// 得到数据库的备份日志
        /// </summary>
        /// <param name="filename">备份文件的完整文件名</param>
        /// <returns></returns>
        public static DataTable GetBackupLog(string filename)
        {
            try
            {
                string sql = string.Format("restore headeronly from disk='{0}'", filename);
                SqlHelper db = new SqlHelper();
                DataTable dt = db.ExecuteDataTable(sql);
                return dt != null ? dt : null;
            }
            catch (Exception)
            {
                
                throw;
            }

        }
       

      
    }
}
