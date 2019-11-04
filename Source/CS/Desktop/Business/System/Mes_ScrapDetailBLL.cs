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
    public class Mes_ScrapDetailBLL
    {
        SqlHelper db = new SqlHelper();

        /// <summary>
        /// 保存实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns>返回值大于0:操作成功</returns>
        public int SaveEntity(string keyValue, Mes_ScrapDetailEntity entity)
        {
            try
            {
                var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
                    strSql.Append("INSERT INTO Mes_ScrapDetail(");
                    strSql.Append("ID,");
                    strSql.Append("S_ScrapNo,");
                    strSql.Append("S_GoodsCode,");
                    strSql.Append("S_GoodsName,");
                    strSql.Append("S_Unit,");
                    strSql.Append("S_Qty,");
                    strSql.Append("S_Batch,");
                    strSql.Append("S_Remark,");
                    strSql.Append("S_Price");
                    strSql.Append(")");
                    strSql.Append(" VALUES (");
                    strSql.Append("@ID,");
                    strSql.Append("@S_ScrapNo,");
                    strSql.Append("@S_GoodsCode,");
                    strSql.Append("@S_GoodsName,");
                    strSql.Append("@S_Unit,");
                    strSql.Append("@S_Qty,");
                    strSql.Append("@S_Batch,");
                    strSql.Append("@S_Remark,");
                    strSql.Append("@S_Price");
                    strSql.Append(")");
                    paramList.Add(new SqlParameter("@ID", Guid.NewGuid().ToString()));
                }
                else
                {
                    
                }
                paramList.Add(new SqlParameter("@S_ScrapNo", entity.S_ScrapNo));
                paramList.Add(new SqlParameter("@S_GoodsCode", entity.S_GoodsCode));
                paramList.Add(new SqlParameter("@S_GoodsName", entity.S_GoodsName));
                paramList.Add(new SqlParameter("@S_Unit", entity.S_Unit));
                paramList.Add(new SqlParameter("@S_Qty", entity.S_Qty));
                paramList.Add(new SqlParameter("@S_Batch", entity.S_Batch));
                paramList.Add(new SqlParameter("@S_Remark", entity.S_Remark));
                paramList.Add(new SqlParameter("@S_Price", entity.S_Price));
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
