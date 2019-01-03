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
         /// ���ݱ���
         /// </summary>
         /// <param name="filename">����·��</param>
         /// <param name="format">�Ƿ��ʽ��</param>
         /// <returns></returns>
        public static bool Backup(string filename, bool format)
        {
            try
            {
                string sql = "";

                //��ʽ����ԭ
                if (format)
                {

                    sql = string.Format("backup database DesktopApp to disk = '{0}' with format", filename);
                }
                //һ�㻹ԭ
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
        /// ��ԭ���ݿ�
        /// </summary>
        /// <param name="filename">�����ļ��������ļ���</param>
        /// <param name="file">��Ҫ��ԭ�ı��ݼ�</param>
        /// <returns></returns>
        public static bool RestoeData(string filename, int file)
        {
            try
            {
                //ɱ�����ж����ݿ���ʵĽ���
                string sqlKill = "exec proc_Kill 'DesktopApp'";
                SqlHelper dbKill = new SqlHelper(Tools.ConfigManager.ReadValueByKey(ConfigurationFile.AppConfig, "restore"));
                int row = dbKill.ExecuteNonQuery(sqlKill);

                //��ԭ���ݿ�
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
        /// �õ����ݿ�ı�����־
        /// </summary>
        /// <param name="filename">�����ļ��������ļ���</param>
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
