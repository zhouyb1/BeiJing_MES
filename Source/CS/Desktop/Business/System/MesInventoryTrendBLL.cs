using System;
using System.Collections.Generic;
using System.Linq;
using Model.Dto;
using Model;
using DataAccess.SqlServer;
using System.Text;
using System.Data.SqlClient;

namespace Business.System
{
    /// <summary>
    /// 描述: 业务层 -- Mes_InventoryTrend
    /// </summary>
    public partial class MesInventoryTrendBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
		public List<MesInventoryTrendEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_InventoryTrend");
                var rows = db.ExecuteObjects<MesInventoryTrendEntity>(strSql.ToString());
                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        /// <summary>
        /// 通过主键获取实体
        /// </summary>
		/// <param name="keyValue">主键</param>
        /// <returns>MesInventoryTrend</returns>
		public MesInventoryTrendEntity GetEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_InventoryTrend");
                strSql.Append(" WHERE ID=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var rowData = db.ExecuteObject<MesInventoryTrendEntity>(strSql.ToString(),paramList.ToArray());
                return rowData;
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
				strSql.Append("DELETE Mes_InventoryTrend");
                strSql.Append(" WHERE ID=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var result = db.ExecuteNonQuery(strSql.ToString(),paramList.ToArray());
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
        public int SaveEntity(string keyValue,MesInventoryTrendEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO Mes_InventoryTrend(");
                     strSql.Append("ID,");
                     strSql.Append("I_OrderKind,");
                     strSql.Append("I_StockCode,");
                     strSql.Append("I_StockName,");
                     strSql.Append("I_GoodsCode,");
                     strSql.Append("I_GoodsName,");
                     strSql.Append("I_Unit,");
                     strSql.Append("I_Batch,");
                     strSql.Append("I_Period,");
                     strSql.Append("I_OrderNo,");
                     strSql.Append("I_QtyOld,");
                     strSql.Append("I_QtyNew,");
                     strSql.Append("I_QtyTrend,");
                     strSql.Append("I_Remark");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@ID,");
                     strSql.Append("@I_OrderKind,");
                     strSql.Append("@I_StockCode,");
                     strSql.Append("@I_StockName,");
                     strSql.Append("@I_GoodsCode,");
                     strSql.Append("@I_GoodsName,");
                     strSql.Append("@I_Unit,");
                     strSql.Append("@I_Batch,");
                     strSql.Append("@I_Period,");
                     strSql.Append("@I_OrderNo,");
                     strSql.Append("@I_QtyOld,");
                     strSql.Append("@I_QtyNew,");
                     strSql.Append("@I_QtyTrend,");
                     strSql.Append("@I_Remark");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@ID",Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE Mes_InventoryTrend SET ");
                     strSql.Append("I_OrderKind=@I_OrderKind,");
                     strSql.Append("I_StockCode=@I_StockCode,");
                     strSql.Append("I_StockName=@I_StockName,");
                     strSql.Append("I_GoodsCode=@I_GoodsCode,");
                     strSql.Append("I_GoodsName=@I_GoodsName,");
                     strSql.Append("I_Unit=@I_Unit,");
                     strSql.Append("I_Batch=@I_Batch,");
                     strSql.Append("I_Period=@I_Period,");
                     strSql.Append("I_OrderNo=@I_OrderNo,");
                     strSql.Append("I_QtyOld=@I_QtyOld,");
                     strSql.Append("I_QtyNew=@I_QtyNew,");
                     strSql.Append("I_QtyTrend=@I_QtyTrend,");
                     strSql.Append("I_Remark=@I_Remark ");
                     strSql.Append(" WHERE ID=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@I_OrderKind",entity.I_OrderKind));
                paramList.Add(new SqlParameter("@I_StockCode",entity.I_StockCode));
                paramList.Add(new SqlParameter("@I_StockName",entity.I_StockName));
                paramList.Add(new SqlParameter("@I_GoodsCode",entity.I_GoodsCode));
                paramList.Add(new SqlParameter("@I_GoodsName",entity.I_GoodsName));
                paramList.Add(new SqlParameter("@I_Unit",entity.I_Unit));
                paramList.Add(new SqlParameter("@I_Batch",entity.I_Batch));
                paramList.Add(new SqlParameter("@I_Period",entity.I_Period));
                paramList.Add(new SqlParameter("@I_OrderNo",entity.I_OrderNo));
                paramList.Add(new SqlParameter("@I_QtyOld",entity.I_QtyOld));
                paramList.Add(new SqlParameter("@I_QtyNew",entity.I_QtyNew));
                paramList.Add(new SqlParameter("@I_QtyTrend",entity.I_QtyTrend));
                paramList.Add(new SqlParameter("@I_Remark",entity.I_Remark));
				var result = db.ExecuteNonQuery(strSql.ToString(),paramList.ToArray());
                return result;
            }
            catch (Exception)
            {
                throw;
            }			
		}
        
    }
}
