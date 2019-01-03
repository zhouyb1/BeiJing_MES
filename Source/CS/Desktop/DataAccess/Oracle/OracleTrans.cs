using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Tools;

namespace DataAccess.Oracle
{
    /// <summary>
    /// 数据库操作
    /// </summary>
    public class OracleTrans
    {
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        /// <returns></returns>
        public OracleConnection TranConn()
        {
            string connectionString = Tools.ConfigManager.ReadValueByKey(ConfigurationFile.AppConfig,
                    "connectionOracleStrings");
            OracleConnection con = new OracleConnection(connectionString);
            con.Open();
            return con;
        }

        /// <summary>
        /// 开始事物
        /// </summary>
        /// <param name="TranConn"></param>
        /// <returns></returns>
        public OracleTransaction TransBegin(OracleConnection TranConn)
        {
            OracleTransaction tran = TranConn.BeginTransaction();
            return tran;
        }

        /// <summary>
        /// 结束事物
        /// </summary>
        /// <param name="TranConn"></param>
        public void TransEnd(OracleConnection TranConn)
        {
            TranConn.Close();
        }

        /// <summary>
        /// 提交事物
        /// </summary>
        /// <param name="trans"></param>
        public void TransCommit(OracleTransaction trans)
        {
            trans.Commit();
        }

        /// <summary>
        /// 回滚事物
        /// </summary>
        /// <param name="trans"></param>
        public void TransRollback(OracleTransaction trans)
        {
            trans.Rollback();
        }
    }
}
