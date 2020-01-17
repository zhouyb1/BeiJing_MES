using DataAccess.SqlServer;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.System
{
    public partial class Mes_WorkShopScanBLL
    {
        SqlHelper db = new SqlHelper();



        /// <summary>
        /// 通过框名称获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<Mes_WorkShopScanEntity> GetList_WorkShopScan(string condit)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT W_GoodsCode,W_GoodsName,W_Batch,W_Qty,W_Price,W_WorkShop,W_Status,W_Unit,ID,W_Remark,W_RecordCode,W_StockCode,W_StockName FROM Mes_WorkShopScan ");
                strSql.Append(condit);
                var paramList = new List<SqlParameter>();
                //paramList.Add(new SqlParameter("@B_BasketName", string.Format("{0}", B_BasketName)));
                var rows = db.ExecuteObjects<Mes_WorkShopScanEntity>(strSql.ToString(), paramList.ToArray());
                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetList_WorkShopScan2(string condit)
        {
            try
            {
                var strSql = new StringBuilder();
                //strSql.Append("SELECT W_GoodsCode,W_GoodsName,W_Batch,W_Qty,W_Price,W_WorkShop,W_Status,W_Unit,ID,W_Remark,W_RecordCode,W_StockCode,W_StockName FROM Mes_WorkShopScan ");
                strSql.Append(condit);
                //var paramList = new List<SqlParameter>();
                //paramList.Add(new SqlParameter("@B_BasketName", string.Format("{0}", B_BasketName)));
                DataSet ds = db.ExecuteDataSet(condit);
                return ds;
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
        public int SaveEntity(string keyValue, Mes_WorkShopScanEntity entity)
        {
            try
            {
                var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
                    strSql.Append("INSERT INTO Mes_WorkShopScan(");
                    strSql.Append("ID,");
                    strSql.Append("W_RecordCode,");
                    strSql.Append("W_WorkShop,");
                    strSql.Append("W_Status,");
                    strSql.Append("W_GoodsCode,");
                    strSql.Append("W_GoodsName,");
                    strSql.Append("W_Unit,");
                    strSql.Append("W_Qty,");
                    strSql.Append("W_Price,");
                    strSql.Append("W_Batch,");
                    strSql.Append("W_Remark");
                    strSql.Append(")");
                    strSql.Append(" VALUES (");
                    strSql.Append("@ID,");
                    strSql.Append("@W_RecordCode,");
                    strSql.Append("@W_WorkShop,");
                    strSql.Append("@W_Status,");
                    strSql.Append("@W_GoodsCode,");
                    strSql.Append("@W_GoodsName,");
                    strSql.Append("@W_Unit,");
                    strSql.Append("@W_Qty,");
                    strSql.Append("@W_Price,");
                    strSql.Append("@W_Batch,");
                    strSql.Append("@W_Remark");
                    strSql.Append(")");
                    paramList.Add(new SqlParameter("@ID", Guid.NewGuid().ToString()));
                }
                else
                {
                    
                }
                paramList.Add(new SqlParameter("@W_RecordCode", entity.W_RecordCode));
                paramList.Add(new SqlParameter("@W_WorkShop", entity.W_WorkShop));
                paramList.Add(new SqlParameter("@W_Status", entity.W_Status));
                paramList.Add(new SqlParameter("@W_GoodsCode", entity.W_GoodsCode));
                paramList.Add(new SqlParameter("@W_GoodsName", entity.W_GoodsName));
                paramList.Add(new SqlParameter("@W_Unit", entity.W_Unit));
                paramList.Add(new SqlParameter("@W_Qty", entity.W_Qty));
                paramList.Add(new SqlParameter("@W_Price", entity.W_Price));
                paramList.Add(new SqlParameter("@W_Batch", entity.W_Batch));
                paramList.Add(new SqlParameter("@W_Remark", entity.W_Remark));
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
        public int DeleteEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("DELETE Mes_WorkShopScan");
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
        public int UpdateEntity(string keyValue, decimal dQty)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("update Mes_WorkShopScan set W_Qty = '" + dQty + "'");
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
    }
}
