using DataAccess.SqlServer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.System
{
    public partial class MesWorkShopBLL
    {
        SqlHelper db = new SqlHelper();

         //<summary>
         //获取数据列表
         //</summary>
         //<returns></returns>
        public List<MesWorkShopEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT W_Code,W_Name,W_Remark,CreateDate,CreateUserName,ModifyDate,ModifyUserName FROM Mes_WorkShop");
                var rows = db.ExecuteObjects<MesWorkShopEntity>(strSql.ToString());
                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<MesWorkShopEntity> GetData(string Condit)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT W_Code,W_Name,W_Remark,CreateDate,CreateUserName,ModifyDate,ModifyUserName FROM Mes_WorkShop ");
                strSql.Append(Condit);
                var rows = db.ExecuteObjects<MesWorkShopEntity>(strSql.ToString());
                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
