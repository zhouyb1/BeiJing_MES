using DataAccess.SqlServer;
using Model;
using Model.System;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.System
{
    public partial class Mes_OutWorkShopDetailBLL
    {
        SqlHelper db = new SqlHelper();

        /// <summary>
        /// 保存实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns>返回值大于0:操作成功</returns>
        public int SaveEntity(string keyValue, Mes_OutWorkShopDetailEntity entity)
        {
            try
            {
                var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {

                    strSql.Append("INSERT INTO Mes_OutWorkShopDetail(");
                    strSql.Append("ID,");
                    strSql.Append("O_OutNo,");
                    strSql.Append("O_GoodsCode,");
                    strSql.Append("O_GoodsName,");
                    strSql.Append("O_Unit,");
                    strSql.Append("O_Qty,");
                    strSql.Append("O_Batch,");
                    strSql.Append("O_Remark,");
                    strSql.Append("O_Price");
                    strSql.Append(")");
                    strSql.Append(" VALUES (");
                    strSql.Append("@ID,");
                    strSql.Append("@O_OutNo,");
                    strSql.Append("@O_GoodsCode,");
                    strSql.Append("@O_GoodsName,");
                    strSql.Append("@O_Unit,");
                    strSql.Append("@O_Qty,");
                    strSql.Append("@O_Batch,");
                    strSql.Append("@O_Remark,");
                    strSql.Append("@O_Price");
                    strSql.Append(")");
                    paramList.Add(new SqlParameter("@ID", Guid.NewGuid().ToString()));
                }
                else
                {
                    
                }
                paramList.Add(new SqlParameter("@O_OutNo", entity.O_OutNo));
                paramList.Add(new SqlParameter("@O_GoodsCode", entity.O_GoodsCode));
                paramList.Add(new SqlParameter("@O_GoodsName", entity.O_GoodsName));
                paramList.Add(new SqlParameter("@O_Unit", entity.O_Unit));
                paramList.Add(new SqlParameter("@O_Qty", entity.O_Qty));
                paramList.Add(new SqlParameter("@O_Batch", entity.O_Batch));
                paramList.Add(new SqlParameter("@O_Remark", entity.O_Remark));
                paramList.Add(new SqlParameter("@O_Price", entity.O_Price));
                var result = db.ExecuteNonQuery(strSql.ToString(), paramList.ToArray());
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
