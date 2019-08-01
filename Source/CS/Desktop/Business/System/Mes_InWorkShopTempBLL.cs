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
    public partial class Mes_InWorkShopTempBLL
    {
        SqlHelper db = new SqlHelper();

        /// <summary>
        /// 通过框名称获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<Mes_InWorkShopTempEntity> GetList_InWorkShopTemp(string condit)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT * FROM Mes_InWorkShopTemp ");
                strSql.Append(condit);
                var paramList = new List<SqlParameter>();
                //paramList.Add(new SqlParameter("@B_BasketName", string.Format("{0}", B_BasketName)));
                var rows = db.ExecuteObjects<Mes_InWorkShopTempEntity>(strSql.ToString(), paramList.ToArray());
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
                strSql.Append("DELETE Mes_InWorkShopTemp");
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
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns>返回值大于0:删除成功</returns>
        public int DeleteData(string condit)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("DELETE Mes_InWorkShopTemp ");
                strSql.Append(condit);
                var result = db.ExecuteNonQuery(strSql.ToString());
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
        public int SaveEntity(string keyValue, Mes_InWorkShopTempEntity entity)
        {
            try
            {
                var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
                    strSql.Append("INSERT INTO Mes_InWorkShopTemp(");
                    strSql.Append("ID,");
                    strSql.Append("I_StockCode,");
                    strSql.Append("I_StockName,");
                    strSql.Append("I_WorkShop,");
                    strSql.Append("I_WorkShopName,");
                    strSql.Append("I_OrderNo,");
                    strSql.Append("I_Status,");
                    strSql.Append("I_CreateBy,");
                    strSql.Append("I_CreateDate,");
                    strSql.Append("I_GoodsCode,");
                    strSql.Append("I_GoodsName,");
                    strSql.Append("I_Unit,");
                    strSql.Append("I_Qty,");
                    strSql.Append("I_Batch,");
                    strSql.Append("I_Remark,");
                    strSql.Append("I_Barcode,");
                    strSql.Append("I_Price,");
                    strSql.Append("I_Record");
                    strSql.Append(")");
                    strSql.Append(" VALUES (");
                    strSql.Append("@ID,");
                    strSql.Append("@I_StockCode,");
                    strSql.Append("@I_StockName,");
                    strSql.Append("@I_WorkShop,");
                    strSql.Append("@I_WorkShopName,");
                    strSql.Append("@I_OrderNo,");
                    strSql.Append("@I_Status,");
                    strSql.Append("@I_CreateBy,");
                    strSql.Append("@I_CreateDate,");
                    strSql.Append("@I_GoodsCode,");
                    strSql.Append("@I_GoodsName,");
                    strSql.Append("@I_Unit,");
                    strSql.Append("@I_Qty,");
                    strSql.Append("@I_Batch,");
                    strSql.Append("@I_Remark,");
                    strSql.Append("@I_Barcode,");
                    strSql.Append("@I_Price,");
                    strSql.Append("@I_Record");
                    strSql.Append(")");
                    paramList.Add(new SqlParameter("@ID", Guid.NewGuid().ToString()));
                }
                else
                {
                    strSql.Append("UPDATE Mes_InWorkShopTemp SET ");
                    strSql.Append("B_BasketCode=@B_BasketCode,");
                    strSql.Append("B_BasketName=@B_BasketName,");
                    strSql.Append("M_Weight=@M_Weight ");
                    strSql.Append(" WHERE ID=@ID");
                    paramList.Add(new SqlParameter("@ID", keyValue));
                }
                paramList.Add(new SqlParameter("@I_StockCode", entity.I_StockCode));
                paramList.Add(new SqlParameter("@I_StockName", entity.I_StockName));
                paramList.Add(new SqlParameter("@I_WorkShop", entity.I_WorkShop));
                paramList.Add(new SqlParameter("@I_WorkShopName", entity.I_WorkShopName));
                paramList.Add(new SqlParameter("@I_OrderNo", entity.I_OrderNo));
                paramList.Add(new SqlParameter("@I_Status", entity.I_Status));
                paramList.Add(new SqlParameter("@I_CreateBy", entity.I_CreateBy));
                paramList.Add(new SqlParameter("@I_CreateDate", entity.I_CreateDate));
                paramList.Add(new SqlParameter("@I_GoodsCode", entity.I_GoodsCode));
                paramList.Add(new SqlParameter("@I_GoodsName", entity.I_GoodsName));
                paramList.Add(new SqlParameter("@I_Unit", entity.I_Unit));
                paramList.Add(new SqlParameter("@I_Qty", entity.I_Qty));
                paramList.Add(new SqlParameter("@I_Batch", entity.I_Batch));
                paramList.Add(new SqlParameter("@I_Remark", entity.I_Remark));
                paramList.Add(new SqlParameter("@I_Barcode", entity.I_Barcode));
                paramList.Add(new SqlParameter("@I_Price", entity.I_Price));
                paramList.Add(new SqlParameter("@I_Record", entity.I_Record));
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
