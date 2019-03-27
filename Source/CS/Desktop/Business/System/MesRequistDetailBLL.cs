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
    /// 描述: 业务层 -- Mes_RequistDetail
    /// </summary>
    public partial class MesRequistDetailBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
		public List<MesRequistDetailEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_RequistDetail");
                var rows = db.ExecuteObjects<MesRequistDetailEntity>(strSql.ToString());
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
        /// <returns>MesRequistDetail</returns>
		public MesRequistDetailEntity GetEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_RequistDetail");
                strSql.Append(" WHERE ID=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var rowData = db.ExecuteObject<MesRequistDetailEntity>(strSql.ToString(),paramList.ToArray());
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
				strSql.Append("DELETE Mes_RequistDetail");
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
        public int SaveEntity(string keyValue,MesRequistDetailEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO Mes_RequistDetail(");
                     strSql.Append("ID,");
                     strSql.Append("R_RequistNo,");
                     strSql.Append("P_OrderNo,");
                     strSql.Append("R_GoodsCode,");
                     strSql.Append("R_GoodsName,");
                     strSql.Append("R_Unit,");
                     strSql.Append("R_Qty,");
                     strSql.Append("R_Batch,");
                     strSql.Append("R_Remark");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@ID,");
                     strSql.Append("@R_RequistNo,");
                     strSql.Append("@P_OrderNo,");
                     strSql.Append("@R_GoodsCode,");
                     strSql.Append("@R_GoodsName,");
                     strSql.Append("@R_Unit,");
                     strSql.Append("@R_Qty,");
                     strSql.Append("@R_Batch,");
                     strSql.Append("@R_Remark");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@ID",Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE Mes_RequistDetail SET ");
                     strSql.Append("R_RequistNo=@R_RequistNo,");
                     strSql.Append("P_OrderNo=@P_OrderNo,");
                     strSql.Append("R_GoodsCode=@R_GoodsCode,");
                     strSql.Append("R_GoodsName=@R_GoodsName,");
                     strSql.Append("R_Unit=@R_Unit,");
                     strSql.Append("R_Qty=@R_Qty,");
                     strSql.Append("R_Batch=@R_Batch,");
                     strSql.Append("R_Remark=@R_Remark ");
                     strSql.Append(" WHERE ID=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@R_RequistNo",entity.R_RequistNo));
                paramList.Add(new SqlParameter("@P_OrderNo",entity.P_OrderNo));
                paramList.Add(new SqlParameter("@R_GoodsCode",entity.R_GoodsCode));
                paramList.Add(new SqlParameter("@R_GoodsName",entity.R_GoodsName));
                paramList.Add(new SqlParameter("@R_Unit",entity.R_Unit));
                paramList.Add(new SqlParameter("@R_Qty",entity.R_Qty));
                paramList.Add(new SqlParameter("@R_Batch",entity.R_Batch));
                paramList.Add(new SqlParameter("@R_Remark",entity.R_Remark));
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
