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
    /// 描述: 业务层 -- Mes_WeightRecord
    /// </summary>
    public partial class MesWeightRecordBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
		public List<MesWeightRecordEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_WeightRecord");
                var rows = db.ExecuteObjects<MesWeightRecordEntity>(strSql.ToString());
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
        /// <returns>MesWeightRecord</returns>
		public MesWeightRecordEntity GetEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_WeightRecord");
                strSql.Append(" WHERE ID=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var rowData = db.ExecuteObject<MesWeightRecordEntity>(strSql.ToString(),paramList.ToArray());
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
				strSql.Append("DELETE Mes_WeightRecord");
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
        public int SaveEntity(string keyValue,MesWeightRecordEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO Mes_WeightRecord(");
                     strSql.Append("ID,");
                     strSql.Append("P_OrderNo,");
                     strSql.Append("W_Kind,");
                     strSql.Append("W_Date,");
                     strSql.Append("W_GoodsCode,");
                     strSql.Append("W_GoodsName,");
                     strSql.Append("W_Unit,");
                     strSql.Append("W_Qty,");
                     strSql.Append("W_Batch");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@ID,");
                     strSql.Append("@P_OrderNo,");
                     strSql.Append("@W_Kind,");
                     strSql.Append("@W_Date,");
                     strSql.Append("@W_GoodsCode,");
                     strSql.Append("@W_GoodsName,");
                     strSql.Append("@W_Unit,");
                     strSql.Append("@W_Qty,");
                     strSql.Append("@W_Batch");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@ID",Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE Mes_WeightRecord SET ");
                     strSql.Append("P_OrderNo=@P_OrderNo,");
                     strSql.Append("W_Kind=@W_Kind,");
                     strSql.Append("W_Date=@W_Date,");
                     strSql.Append("W_GoodsCode=@W_GoodsCode,");
                     strSql.Append("W_GoodsName=@W_GoodsName,");
                     strSql.Append("W_Unit=@W_Unit,");
                     strSql.Append("W_Qty=@W_Qty,");
                     strSql.Append("W_Batch=@W_Batch ");
                     strSql.Append(" WHERE ID=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@P_OrderNo",entity.P_OrderNo));
                paramList.Add(new SqlParameter("@W_Kind",entity.W_Kind));
                paramList.Add(new SqlParameter("@W_Date",entity.W_Date));
                paramList.Add(new SqlParameter("@W_GoodsCode",entity.W_GoodsCode));
                paramList.Add(new SqlParameter("@W_GoodsName",entity.W_GoodsName));
                paramList.Add(new SqlParameter("@W_Unit",entity.W_Unit));
                paramList.Add(new SqlParameter("@W_Qty",entity.W_Qty));
                paramList.Add(new SqlParameter("@W_Batch",entity.W_Batch));
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
