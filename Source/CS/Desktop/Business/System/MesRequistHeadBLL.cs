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
    /// 描述: 业务层 -- Mes_RequistHead
    /// </summary>
    public partial class MesRequistHeadBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
		public List<MesRequistHeadEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_RequistHead");
                var rows = db.ExecuteObjects<MesRequistHeadEntity>(strSql.ToString());
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
        /// <returns>MesRequistHead</returns>
		public MesRequistHeadEntity GetEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_RequistHead");
                strSql.Append(" WHERE ID=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var rowData = db.ExecuteObject<MesRequistHeadEntity>(strSql.ToString(),paramList.ToArray());
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
				strSql.Append("DELETE Mes_RequistHead");
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
        public int SaveEntity(string keyValue,MesRequistHeadEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO Mes_RequistHead(");
                     strSql.Append("ID,");
                     strSql.Append("R_RequistNo,");
                     strSql.Append("R_StockCode,");
                     strSql.Append("R_StockName,");
                     strSql.Append("R_StockToCode,");
                     strSql.Append("R_StockToName,");
                     strSql.Append("P_OrderNo,");
                     strSql.Append("P_OrderDate,");
                     strSql.Append("R_Status,");
                     strSql.Append("R_CreateBy,");
                     strSql.Append("R_CreateDate,");
                     strSql.Append("R_UpdateBy,");
                     strSql.Append("R_UpdateDate,");
                     strSql.Append("R_Remark,");
                     strSql.Append("R_DeleteBy,");
                     strSql.Append("R_DeleteDate,");
                     strSql.Append("R_UploadBy,");
                     strSql.Append("R_UploadDate");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@ID,");
                     strSql.Append("@R_RequistNo,");
                     strSql.Append("@R_StockCode,");
                     strSql.Append("@R_StockName,");
                     strSql.Append("@R_StockToCode,");
                     strSql.Append("@R_StockToName,");
                     strSql.Append("@P_OrderNo,");
                     strSql.Append("@P_OrderDate,");
                     strSql.Append("@R_Status,");
                     strSql.Append("@R_CreateBy,");
                     strSql.Append("@R_CreateDate,");
                     strSql.Append("@R_UpdateBy,");
                     strSql.Append("@R_UpdateDate,");
                     strSql.Append("@R_Remark,");
                     strSql.Append("@R_DeleteBy,");
                     strSql.Append("@R_DeleteDate,");
                     strSql.Append("@R_UploadBy,");
                     strSql.Append("@R_UploadDate");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@ID",Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE Mes_RequistHead SET ");
                     strSql.Append("R_RequistNo=@R_RequistNo,");
                     strSql.Append("R_StockCode=@R_StockCode,");
                     strSql.Append("R_StockName=@R_StockName,");
                     strSql.Append("R_StockToCode=@R_StockToCode,");
                     strSql.Append("R_StockToName=@R_StockToName,");
                     strSql.Append("P_OrderNo=@P_OrderNo,");
                     strSql.Append("P_OrderDate=@P_OrderDate,");
                     strSql.Append("R_Status=@R_Status,");
                     strSql.Append("R_CreateBy=@R_CreateBy,");
                     strSql.Append("R_CreateDate=@R_CreateDate,");
                     strSql.Append("R_UpdateBy=@R_UpdateBy,");
                     strSql.Append("R_UpdateDate=@R_UpdateDate,");
                     strSql.Append("R_Remark=@R_Remark,");
                     strSql.Append("R_DeleteBy=@R_DeleteBy,");
                     strSql.Append("R_DeleteDate=@R_DeleteDate,");
                     strSql.Append("R_UploadBy=@R_UploadBy,");
                     strSql.Append("R_UploadDate=@R_UploadDate ");
                     strSql.Append(" WHERE ID=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@R_RequistNo",entity.R_RequistNo));
                paramList.Add(new SqlParameter("@R_StockCode",entity.R_StockCode));
                paramList.Add(new SqlParameter("@R_StockName",entity.R_StockName));
                paramList.Add(new SqlParameter("@R_StockToCode",entity.R_StockToCode));
                paramList.Add(new SqlParameter("@R_StockToName",entity.R_StockToName));
                paramList.Add(new SqlParameter("@P_OrderNo",entity.P_OrderNo));
                paramList.Add(new SqlParameter("@P_OrderDate",entity.P_OrderDate));
                paramList.Add(new SqlParameter("@R_Status",entity.R_Status));
                paramList.Add(new SqlParameter("@R_CreateBy",entity.R_CreateBy));
                paramList.Add(new SqlParameter("@R_CreateDate",entity.R_CreateDate));
                paramList.Add(new SqlParameter("@R_UpdateBy",entity.R_UpdateBy));
                paramList.Add(new SqlParameter("@R_UpdateDate",entity.R_UpdateDate));
                paramList.Add(new SqlParameter("@R_Remark",entity.R_Remark));
                paramList.Add(new SqlParameter("@R_DeleteBy",entity.R_DeleteBy));
                paramList.Add(new SqlParameter("@R_DeleteDate",entity.R_DeleteDate));
                paramList.Add(new SqlParameter("@R_UploadBy",entity.R_UploadBy));
                paramList.Add(new SqlParameter("@R_UploadDate",entity.R_UploadDate));
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
