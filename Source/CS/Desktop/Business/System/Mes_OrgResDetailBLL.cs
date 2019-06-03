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
    public partial class Mes_OrgResDetailBLL
    {
        SqlHelper db = new SqlHelper();

        /// <summary>
        /// 保存实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns>返回值大于0:操作成功</returns>
        public int SaveEntity(string keyValue, Mes_OrgResDetailEntity entity)
        {
            try
            {
                var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
                    

                    strSql.Append("INSERT INTO Mes_OrgResDetail(");
                    strSql.Append("ID,");
                    strSql.Append("O_OrgResNo,");
                    strSql.Append("O_GoodsCode,");
                    strSql.Append("O_GoodsName,");
                    strSql.Append("O_Unit,");
                    strSql.Append("O_Qty,");
                    strSql.Append("O_Batch,");
                    strSql.Append("O_Price,");
                    strSql.Append("O_SecGoodsCode,");
                    strSql.Append("O_SecGoodsName,");
                    strSql.Append("O_SecUnit,");
                    strSql.Append("O_SecQty,");
                    strSql.Append("O_SecBatch,");
                    strSql.Append("O_SecPrice");
                    strSql.Append(")");
                    strSql.Append(" VALUES (");
                    strSql.Append("@ID,");
                    strSql.Append("@O_OrgResNo,");
                    strSql.Append("@O_GoodsCode,");
                    strSql.Append("@O_GoodsName,");
                    strSql.Append("@O_Unit,");
                    strSql.Append("@O_Qty,");
                    strSql.Append("@O_Batch,");
                    strSql.Append("@O_Price,");
                    strSql.Append("@O_SecGoodsCode,");
                    strSql.Append("@O_SecGoodsName,");
                    strSql.Append("@O_SecUnit,");
                    strSql.Append("@O_SecQty,");
                    strSql.Append("@O_SecBatch,");
                    strSql.Append("@O_SecPrice");
                    strSql.Append(")");
                    paramList.Add(new SqlParameter("@ID", Guid.NewGuid().ToString()));
                }
                else
                {
                }
                paramList.Add(new SqlParameter("@O_OrgResNo", entity.O_OrgResNo));
                paramList.Add(new SqlParameter("@O_GoodsCode", entity.O_GoodsCode));
                paramList.Add(new SqlParameter("@O_GoodsName", entity.O_GoodsName));
                paramList.Add(new SqlParameter("@O_Unit", entity.O_Unit));
                paramList.Add(new SqlParameter("@O_Qty", entity.O_Qty));
                paramList.Add(new SqlParameter("@O_Batch", entity.O_Batch));
                paramList.Add(new SqlParameter("@O_Price", entity.O_Price));
                paramList.Add(new SqlParameter("@O_SecGoodsCode", entity.O_SecGoodsCode));
                paramList.Add(new SqlParameter("@O_SecGoodsName", entity.O_SecGoodsName));
                paramList.Add(new SqlParameter("@O_SecUnit", entity.O_SecUnit));
                paramList.Add(new SqlParameter("@O_SecQty", entity.O_SecQty));
                paramList.Add(new SqlParameter("@O_SecBatch", entity.O_SecBatch));
                paramList.Add(new SqlParameter("@O_SecPrice", entity.O_SecPrice));
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
