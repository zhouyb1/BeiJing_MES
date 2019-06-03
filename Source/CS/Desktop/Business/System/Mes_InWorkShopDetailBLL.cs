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
    public partial class Mes_InWorkShopDetailBLL
    {
        SqlHelper db = new SqlHelper();

        /// <summary>
        /// 保存实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns>返回值大于0:操作成功</returns>
        public int SaveEntity(string keyValue, Mes_InWorkShopDetailEntity entity)
        {
            try
            {
                var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {

                    strSql.Append("INSERT INTO Mes_InWorkShopDetail(");
                    strSql.Append("ID,");
                    strSql.Append("I_InNo,");
                    strSql.Append("I_GoodsCode,");
                    strSql.Append("I_GoodsName,");
                    strSql.Append("I_Unit,");
                    strSql.Append("I_Qty,");
                    strSql.Append("I_Batch,");
                    strSql.Append("I_Remark,");
                    strSql.Append("I_Price");
                    strSql.Append(")");
                    strSql.Append(" VALUES (");
                    strSql.Append("@ID,");
                    strSql.Append("@I_InNo,");
                    strSql.Append("@I_GoodsCode,");
                    strSql.Append("@I_GoodsName,");
                    strSql.Append("@I_Unit,");
                    strSql.Append("@I_Qty,");
                    strSql.Append("@I_Batch,");
                    strSql.Append("@I_Remark,");
                    strSql.Append("@I_Price");
                    strSql.Append(")");
                    paramList.Add(new SqlParameter("@ID", Guid.NewGuid().ToString()));
                }
                else
                {
                    strSql.Append("UPDATE Mes_InWorkShopDetail SET ");
                    strSql.Append("B_BasketCode=@B_BasketCode,");
                    strSql.Append("B_BasketName=@B_BasketName,");
                    strSql.Append("M_Weight=@M_Weight ");
                    strSql.Append(" WHERE ID=@ID");
                    paramList.Add(new SqlParameter("@ID", keyValue));
                }
                paramList.Add(new SqlParameter("@I_InNo", entity.I_InNo));
                paramList.Add(new SqlParameter("@I_GoodsCode", entity.I_GoodsCode));
                paramList.Add(new SqlParameter("@I_GoodsName", entity.I_GoodsName));
                paramList.Add(new SqlParameter("@I_Unit", entity.I_Unit));
                paramList.Add(new SqlParameter("@I_Qty", entity.I_Qty));
                paramList.Add(new SqlParameter("@I_Batch", entity.I_Batch));
                paramList.Add(new SqlParameter("@I_Remark", entity.I_Remark));
                paramList.Add(new SqlParameter("@I_Price", entity.I_Price));
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
