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
    public partial class Mes_OutWorkShopTempBLL
    {
        SqlHelper db = new SqlHelper();

        /// <summary>
        /// 通过框名称获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<Mes_OutWorkShopTempEntity> GetList_OutWorkShopTemp(string condit)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT * FROM Mes_OutWorkShopTemp ");
                strSql.Append(condit);
                var paramList = new List<SqlParameter>();
                //paramList.Add(new SqlParameter("@B_BasketName", string.Format("{0}", B_BasketName)));
                var rows = db.ExecuteObjects<Mes_OutWorkShopTempEntity>(strSql.ToString(), paramList.ToArray());
                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns>返回值大于0:删除成功</returns>
        public int DeleteEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("DELETE Mes_OutWorkShopTemp");
                strSql.Append(" WHERE ID=@ID");
                var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID", keyValue));
                var result = db.ExecuteNonQuery(strSql.ToString(), paramList.ToArray());
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 保存实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns>返回值大于0:操作成功</returns>
        public int SaveEntity(string keyValue, Mes_OutWorkShopTempEntity entity)
        {
            try
            {
                var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
                    strSql.Append("INSERT INTO Mes_OutWorkShopTemp(");
                    strSql.Append("ID,");
                    strSql.Append("O_StockCode,");
                    strSql.Append("O_StockName,");
                    strSql.Append("O_WorkShop,");
                    strSql.Append("O_WorkShopName,");
                    strSql.Append("O_OrderNo,");
                    strSql.Append("O_Status,");
                    strSql.Append("O_CreateBy,");
                    strSql.Append("O_CreateDate,");
                    strSql.Append("O_GoodsCode,");
                    strSql.Append("O_GoodsName,");
                    strSql.Append("O_Unit,");
                    strSql.Append("O_Qty,");
                    strSql.Append("O_Batch,");
                    strSql.Append("O_Remark,");
                    strSql.Append("O_Barcode,");
                    strSql.Append("O_Price,");
                    strSql.Append("O_Record");
                    strSql.Append(")");
                    strSql.Append(" VALUES (");
                    strSql.Append("@ID,");
                    strSql.Append("@O_StockCode,");
                    strSql.Append("@O_StockName,");
                    strSql.Append("@O_WorkShop,");
                    strSql.Append("@O_WorkShopName,");
                    strSql.Append("@O_OrderNo,");
                    strSql.Append("@O_Status,");
                    strSql.Append("@O_CreateBy,");
                    strSql.Append("@O_CreateDate,");
                    strSql.Append("@O_GoodsCode,");
                    strSql.Append("@O_GoodsName,");
                    strSql.Append("@O_Unit,");
                    strSql.Append("@O_Qty,");
                    strSql.Append("@O_Batch,");
                    strSql.Append("@O_Remark,");
                    strSql.Append("@O_Barcode,");
                    strSql.Append("@O_Price,");
                    strSql.Append("@O_Record");
                    strSql.Append(")");
                    paramList.Add(new SqlParameter("@ID", Guid.NewGuid().ToString()));
                }
                else
                {
                    strSql.Append("UPDATE Mes_OutWorkShopTemp SET ");
                    strSql.Append("B_BasketCode=@B_BasketCode,");
                    strSql.Append("B_BasketName=@B_BasketName,");
                    strSql.Append("M_Weight=@M_Weight ");
                    strSql.Append(" WHERE ID=@ID");
                    paramList.Add(new SqlParameter("@ID", keyValue));
                }
                
                paramList.Add(new SqlParameter("@O_StockCode", entity.O_StockCode));
                paramList.Add(new SqlParameter("@O_StockName", entity.O_StockName));
                paramList.Add(new SqlParameter("@O_WorkShop", entity.O_WorkShop));
                paramList.Add(new SqlParameter("@O_WorkShopName", entity.O_WorkShopName));
                paramList.Add(new SqlParameter("@O_OrderNo", entity.O_OrderNo));
                paramList.Add(new SqlParameter("@O_Status", entity.O_Status));
                paramList.Add(new SqlParameter("@O_CreateBy", entity.O_CreateBy));
                paramList.Add(new SqlParameter("@O_CreateDate", entity.O_CreateDate));
                paramList.Add(new SqlParameter("@O_GoodsCode", entity.O_GoodsCode));
                paramList.Add(new SqlParameter("@O_GoodsName", entity.O_GoodsName));
                paramList.Add(new SqlParameter("@O_Unit", entity.O_Unit));
                paramList.Add(new SqlParameter("@O_Qty", entity.O_Qty));
                paramList.Add(new SqlParameter("@O_Batch", entity.O_Batch));
                paramList.Add(new SqlParameter("@O_Remark", entity.O_Remark));
                paramList.Add(new SqlParameter("@O_Barcode", entity.O_Barcode));
                paramList.Add(new SqlParameter("@O_Price", entity.O_Price));
                paramList.Add(new SqlParameter("@O_Record", entity.O_Record));
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
