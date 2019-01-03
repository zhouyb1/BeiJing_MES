using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace DataAccess.SqlServer
{
    /// <summary>
    /// 数据库操作
    /// </summary>
    public class SqlTrans
    {
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        /// <returns></returns>
        public SqlConnection TranConn()
        {
            string connectionString = Tools.ConfigManager.ReadValueByKey(ConfigurationFile.AppConfig,
                    "connectionStrings"); 
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }

        /// <summary>
        /// 开始事物
        /// </summary>
        /// <param name="TranConn"></param>
        /// <returns></returns>
        public SqlTransaction TransBegin(SqlConnection TranConn)
        {
            SqlTransaction tran = TranConn.BeginTransaction();
            return tran;
        }

        /// <summary>
        /// 结束事物
        /// </summary>
        /// <param name="TranConn"></param>
        public void TransEnd(SqlConnection TranConn)
        {
            TranConn.Close();
        }

        /// <summary>
        /// 提交事物
        /// </summary>
        /// <param name="trans"></param>
        public void TransCommit(SqlTransaction trans)
        {
            trans.Commit();
        }

        /// <summary>
        /// 回滚事物
        /// </summary>
        /// <param name="trans"></param>
        public void TransRollback(SqlTransaction trans)
        {
            trans.Rollback();
        }
    }
}
