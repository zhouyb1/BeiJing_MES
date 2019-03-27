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
    /// 描述: 业务层 -- Mes_ProOutHead
    /// </summary>
    public partial class MesProOutHeadBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
		public List<MesProOutHeadEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_ProOutHead");
                var rows = db.ExecuteObjects<MesProOutHeadEntity>(strSql.ToString());
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
        /// <returns>MesProOutHead</returns>
		public MesProOutHeadEntity GetEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_ProOutHead");
                strSql.Append(" WHERE ID=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var rowData = db.ExecuteObject<MesProOutHeadEntity>(strSql.ToString(),paramList.ToArray());
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
				strSql.Append("DELETE Mes_ProOutHead");
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
        public int SaveEntity(string keyValue,MesProOutHeadEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO Mes_ProOutHead(");
                     strSql.Append("ID,");
                     strSql.Append("P_ProOutNo,");
                     strSql.Append("P_StockCode,");
                     strSql.Append("P_StockName,");
                     strSql.Append("P_OrderNo,");
                     strSql.Append("P_OrderDate,");
                     strSql.Append("P_Status,");
                     strSql.Append("P_CreateBy,");
                     strSql.Append("P_CreateDate,");
                     strSql.Append("P_UpdateBy,");
                     strSql.Append("P_UpdateDate,");
                     strSql.Append("P_Remark,");
                     strSql.Append("P_DeleteBy,");
                     strSql.Append("P_DeleteDate,");
                     strSql.Append("P_UploadBy,");
                     strSql.Append("P_UploadDate");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@ID,");
                     strSql.Append("@P_ProOutNo,");
                     strSql.Append("@P_StockCode,");
                     strSql.Append("@P_StockName,");
                     strSql.Append("@P_OrderNo,");
                     strSql.Append("@P_OrderDate,");
                     strSql.Append("@P_Status,");
                     strSql.Append("@P_CreateBy,");
                     strSql.Append("@P_CreateDate,");
                     strSql.Append("@P_UpdateBy,");
                     strSql.Append("@P_UpdateDate,");
                     strSql.Append("@P_Remark,");
                     strSql.Append("@P_DeleteBy,");
                     strSql.Append("@P_DeleteDate,");
                     strSql.Append("@P_UploadBy,");
                     strSql.Append("@P_UploadDate");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@ID",Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE Mes_ProOutHead SET ");
                     strSql.Append("P_ProOutNo=@P_ProOutNo,");
                     strSql.Append("P_StockCode=@P_StockCode,");
                     strSql.Append("P_StockName=@P_StockName,");
                     strSql.Append("P_OrderNo=@P_OrderNo,");
                     strSql.Append("P_OrderDate=@P_OrderDate,");
                     strSql.Append("P_Status=@P_Status,");
                     strSql.Append("P_CreateBy=@P_CreateBy,");
                     strSql.Append("P_CreateDate=@P_CreateDate,");
                     strSql.Append("P_UpdateBy=@P_UpdateBy,");
                     strSql.Append("P_UpdateDate=@P_UpdateDate,");
                     strSql.Append("P_Remark=@P_Remark,");
                     strSql.Append("P_DeleteBy=@P_DeleteBy,");
                     strSql.Append("P_DeleteDate=@P_DeleteDate,");
                     strSql.Append("P_UploadBy=@P_UploadBy,");
                     strSql.Append("P_UploadDate=@P_UploadDate ");
                     strSql.Append(" WHERE ID=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@P_ProOutNo",entity.P_ProOutNo));
                paramList.Add(new SqlParameter("@P_StockCode",entity.P_StockCode));
                paramList.Add(new SqlParameter("@P_StockName",entity.P_StockName));
                paramList.Add(new SqlParameter("@P_OrderNo",entity.P_OrderNo));
                paramList.Add(new SqlParameter("@P_OrderDate",entity.P_OrderDate));
                paramList.Add(new SqlParameter("@P_Status",entity.P_Status));
                paramList.Add(new SqlParameter("@P_CreateBy",entity.P_CreateBy));
                paramList.Add(new SqlParameter("@P_CreateDate",entity.P_CreateDate));
                paramList.Add(new SqlParameter("@P_UpdateBy",entity.P_UpdateBy));
                paramList.Add(new SqlParameter("@P_UpdateDate",entity.P_UpdateDate));
                paramList.Add(new SqlParameter("@P_Remark",entity.P_Remark));
                paramList.Add(new SqlParameter("@P_DeleteBy",entity.P_DeleteBy));
                paramList.Add(new SqlParameter("@P_DeleteDate",entity.P_DeleteDate));
                paramList.Add(new SqlParameter("@P_UploadBy",entity.P_UploadBy));
                paramList.Add(new SqlParameter("@P_UploadDate",entity.P_UploadDate));
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
