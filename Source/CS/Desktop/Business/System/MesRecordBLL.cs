using DataAccess.SqlServer;
using Model;
using Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.System
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MesRecordBLL
    {
        SqlHelper db = new SqlHelper();

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<MesRecordEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT * FROM Mes_Record order by R_Record");
                var rows = db.ExecuteObjects<MesRecordEntity>(strSql.ToString());
                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<MesRecordEntity> GetData(string condit)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT * FROM Mes_Record");
                strSql.Append(condit);
                var rows = db.ExecuteObjects<MesRecordEntity>(strSql.ToString());
                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
