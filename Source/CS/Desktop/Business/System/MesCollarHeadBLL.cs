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
    /// 描述: 业务层 -- Mes_CollarHead
    /// </summary>
    public partial class MesCollarHeadBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
		public List<MesCollarHeadEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_CollarHead");
                var rows = db.ExecuteObjects<MesCollarHeadEntity>(strSql.ToString());
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
        /// <returns>MesCollarHead</returns>
		public MesCollarHeadEntity GetEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_CollarHead");
                strSql.Append(" WHERE ID=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var rowData = db.ExecuteObject<MesCollarHeadEntity>(strSql.ToString(),paramList.ToArray());
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
				strSql.Append("DELETE Mes_CollarHead");
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
        public int SaveEntity(string keyValue,MesCollarHeadEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO Mes_CollarHead(");
                     strSql.Append("ID,");
                     strSql.Append("C_CollarNo,");
                     strSql.Append("C_StockCode,");
                     strSql.Append("C_StockName,");
                     strSql.Append("C_StockToCode,");
                     strSql.Append("C_StockToName,");
                     strSql.Append("P_OrderNo,");
                     strSql.Append("P_OrderDate,");
                     strSql.Append("P_Status,");
                     strSql.Append("C_CreateBy,");
                     strSql.Append("C_CreateDate,");
                     strSql.Append("C_UpdateBy,");
                     strSql.Append("C_UpdateDate,");
                     strSql.Append("C_Remark,");
                     strSql.Append("M_DeleteBy,");
                     strSql.Append("M_DeleteDate,");
                     strSql.Append("M_UploadBy,");
                     strSql.Append("M_UploadDate");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@ID,");
                     strSql.Append("@C_CollarNo,");
                     strSql.Append("@C_StockCode,");
                     strSql.Append("@C_StockName,");
                     strSql.Append("@C_StockToCode,");
                     strSql.Append("@C_StockToName,");
                     strSql.Append("@P_OrderNo,");
                     strSql.Append("@P_OrderDate,");
                     strSql.Append("@P_Status,");
                     strSql.Append("@C_CreateBy,");
                     strSql.Append("@C_CreateDate,");
                     strSql.Append("@C_UpdateBy,");
                     strSql.Append("@C_UpdateDate,");
                     strSql.Append("@C_Remark,");
                     strSql.Append("@M_DeleteBy,");
                     strSql.Append("@M_DeleteDate,");
                     strSql.Append("@M_UploadBy,");
                     strSql.Append("@M_UploadDate");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@ID",Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE Mes_CollarHead SET ");
                     strSql.Append("C_CollarNo=@C_CollarNo,");
                     strSql.Append("C_StockCode=@C_StockCode,");
                     strSql.Append("C_StockName=@C_StockName,");
                     strSql.Append("C_StockToCode=@C_StockToCode,");
                     strSql.Append("C_StockToName=@C_StockToName,");
                     strSql.Append("P_OrderNo=@P_OrderNo,");
                     strSql.Append("P_OrderDate=@P_OrderDate,");
                     strSql.Append("P_Status=@P_Status,");
                     strSql.Append("C_CreateBy=@C_CreateBy,");
                     strSql.Append("C_CreateDate=@C_CreateDate,");
                     strSql.Append("C_UpdateBy=@C_UpdateBy,");
                     strSql.Append("C_UpdateDate=@C_UpdateDate,");
                     strSql.Append("C_Remark=@C_Remark,");
                     strSql.Append("M_DeleteBy=@M_DeleteBy,");
                     strSql.Append("M_DeleteDate=@M_DeleteDate,");
                     strSql.Append("M_UploadBy=@M_UploadBy,");
                     strSql.Append("M_UploadDate=@M_UploadDate ");
                     strSql.Append(" WHERE ID=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@C_CollarNo",entity.C_CollarNo));
                paramList.Add(new SqlParameter("@C_StockCode",entity.C_StockCode));
                paramList.Add(new SqlParameter("@C_StockName",entity.C_StockName));
                paramList.Add(new SqlParameter("@C_StockToCode",entity.C_StockToCode));
                paramList.Add(new SqlParameter("@C_StockToName",entity.C_StockToName));
                paramList.Add(new SqlParameter("@P_OrderNo",entity.P_OrderNo));
                paramList.Add(new SqlParameter("@P_OrderDate",entity.P_OrderDate));
                paramList.Add(new SqlParameter("@P_Status",entity.P_Status));
                paramList.Add(new SqlParameter("@C_CreateBy",entity.C_CreateBy));
                paramList.Add(new SqlParameter("@C_CreateDate",entity.C_CreateDate));
                paramList.Add(new SqlParameter("@C_UpdateBy",entity.C_UpdateBy));
                paramList.Add(new SqlParameter("@C_UpdateDate",entity.C_UpdateDate));
                paramList.Add(new SqlParameter("@C_Remark",entity.C_Remark));
                paramList.Add(new SqlParameter("@M_DeleteBy",entity.M_DeleteBy));
                paramList.Add(new SqlParameter("@M_DeleteDate",entity.M_DeleteDate));
                paramList.Add(new SqlParameter("@M_UploadBy",entity.M_UploadBy));
                paramList.Add(new SqlParameter("@M_UploadDate",entity.M_UploadDate));
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
