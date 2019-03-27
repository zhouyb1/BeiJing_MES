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
    /// 描述: 业务层 -- Mes_PurchaseDetail
    /// </summary>
    public partial class MesPurchaseDetailBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
		public List<MesPurchaseDetailEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_PurchaseDetail");
                var rows = db.ExecuteObjects<MesPurchaseDetailEntity>(strSql.ToString());
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
        /// <returns>MesPurchaseDetail</returns>
		public MesPurchaseDetailEntity GetEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_PurchaseDetail");
                strSql.Append(" WHERE ID=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var rowData = db.ExecuteObject<MesPurchaseDetailEntity>(strSql.ToString(),paramList.ToArray());
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
				strSql.Append("DELETE Mes_PurchaseDetail");
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
        public int SaveEntity(string keyValue,MesPurchaseDetailEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO Mes_PurchaseDetail(");
                     strSql.Append("ID,");
                     strSql.Append("P_PurchaseNo,");
                     strSql.Append("P_OrderNo,");
                     strSql.Append("P_GoodsCode,");
                     strSql.Append("P_GoodsName,");
                     strSql.Append("P_Unit,");
                     strSql.Append("P_Qty,");
                     strSql.Append("P_Price,");
                     strSql.Append("P_Batch,");
                     strSql.Append("P_Remark");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@ID,");
                     strSql.Append("@P_PurchaseNo,");
                     strSql.Append("@P_OrderNo,");
                     strSql.Append("@P_GoodsCode,");
                     strSql.Append("@P_GoodsName,");
                     strSql.Append("@P_Unit,");
                     strSql.Append("@P_Qty,");
                     strSql.Append("@P_Price,");
                     strSql.Append("@P_Batch,");
                     strSql.Append("@P_Remark");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@ID",Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE Mes_PurchaseDetail SET ");
                     strSql.Append("P_PurchaseNo=@P_PurchaseNo,");
                     strSql.Append("P_OrderNo=@P_OrderNo,");
                     strSql.Append("P_GoodsCode=@P_GoodsCode,");
                     strSql.Append("P_GoodsName=@P_GoodsName,");
                     strSql.Append("P_Unit=@P_Unit,");
                     strSql.Append("P_Qty=@P_Qty,");
                     strSql.Append("P_Price=@P_Price,");
                     strSql.Append("P_Batch=@P_Batch,");
                     strSql.Append("P_Remark=@P_Remark ");
                     strSql.Append(" WHERE ID=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@P_PurchaseNo",entity.P_PurchaseNo));
                paramList.Add(new SqlParameter("@P_OrderNo",entity.P_OrderNo));
                paramList.Add(new SqlParameter("@P_GoodsCode",entity.P_GoodsCode));
                paramList.Add(new SqlParameter("@P_GoodsName",entity.P_GoodsName));
                paramList.Add(new SqlParameter("@P_Unit",entity.P_Unit));
                paramList.Add(new SqlParameter("@P_Qty",entity.P_Qty));
                paramList.Add(new SqlParameter("@P_Price",entity.P_Price));
                paramList.Add(new SqlParameter("@P_Batch",entity.P_Batch));
                paramList.Add(new SqlParameter("@P_Remark",entity.P_Remark));
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
