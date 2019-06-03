using DataAccess.SqlServer;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.System
{
    public partial class Mes_ProceBLL
    {
        SqlHelper db = new SqlHelper();

        /// <summary>
        /// 通过框名称获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<Mes_ProceEntity> GetList_Proce(string condit)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT * FROM Mes_Proce ");
                strSql.Append(condit);
                var paramList = new List<SqlParameter>();
                //paramList.Add(new SqlParameter("@B_BasketName", string.Format("{0}", B_BasketName)));
                var rows = db.ExecuteObjects<Mes_ProceEntity>(strSql.ToString(), paramList.ToArray());
                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
