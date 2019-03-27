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
    /// 描述: 业务层 -- Mes_OrgResHead
    /// </summary>
    public partial class MesOrgResHeadBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
		public List<MesOrgResHeadEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_OrgResHead");
                var rows = db.ExecuteObjects<MesOrgResHeadEntity>(strSql.ToString());
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
        /// <returns>MesOrgResHead</returns>
		public MesOrgResHeadEntity GetEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_OrgResHead");
                strSql.Append(" WHERE ID=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var rowData = db.ExecuteObject<MesOrgResHeadEntity>(strSql.ToString(),paramList.ToArray());
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
				strSql.Append("DELETE Mes_OrgResHead");
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
        public int SaveEntity(string keyValue,MesOrgResHeadEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO Mes_OrgResHead(");
                     strSql.Append("ID,");
                     strSql.Append("O_OrgResNo,");
                     strSql.Append("O_StockCode,");
                     strSql.Append("O_StockName,");
                     strSql.Append("O_OrderNo,");
                     strSql.Append("O_OrderDate,");
                     strSql.Append("O_Status,");
                     strSql.Append("O_CreateBy,");
                     strSql.Append("O_CreateDate,");
                     strSql.Append("O_UpdateBy,");
                     strSql.Append("O_UpdateDate,");
                     strSql.Append("O_Remark,");
                     strSql.Append("O_DeleteBy,");
                     strSql.Append("O_DeleteDate,");
                     strSql.Append("O_UploadBy,");
                     strSql.Append("O_UploadDate");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@ID,");
                     strSql.Append("@O_OrgResNo,");
                     strSql.Append("@O_StockCode,");
                     strSql.Append("@O_StockName,");
                     strSql.Append("@O_OrderNo,");
                     strSql.Append("@O_OrderDate,");
                     strSql.Append("@O_Status,");
                     strSql.Append("@O_CreateBy,");
                     strSql.Append("@O_CreateDate,");
                     strSql.Append("@O_UpdateBy,");
                     strSql.Append("@O_UpdateDate,");
                     strSql.Append("@O_Remark,");
                     strSql.Append("@O_DeleteBy,");
                     strSql.Append("@O_DeleteDate,");
                     strSql.Append("@O_UploadBy,");
                     strSql.Append("@O_UploadDate");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@ID",Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE Mes_OrgResHead SET ");
                     strSql.Append("O_OrgResNo=@O_OrgResNo,");
                     strSql.Append("O_StockCode=@O_StockCode,");
                     strSql.Append("O_StockName=@O_StockName,");
                     strSql.Append("O_OrderNo=@O_OrderNo,");
                     strSql.Append("O_OrderDate=@O_OrderDate,");
                     strSql.Append("O_Status=@O_Status,");
                     strSql.Append("O_CreateBy=@O_CreateBy,");
                     strSql.Append("O_CreateDate=@O_CreateDate,");
                     strSql.Append("O_UpdateBy=@O_UpdateBy,");
                     strSql.Append("O_UpdateDate=@O_UpdateDate,");
                     strSql.Append("O_Remark=@O_Remark,");
                     strSql.Append("O_DeleteBy=@O_DeleteBy,");
                     strSql.Append("O_DeleteDate=@O_DeleteDate,");
                     strSql.Append("O_UploadBy=@O_UploadBy,");
                     strSql.Append("O_UploadDate=@O_UploadDate ");
                     strSql.Append(" WHERE ID=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@O_OrgResNo",entity.O_OrgResNo));
                paramList.Add(new SqlParameter("@O_StockCode",entity.O_StockCode));
                paramList.Add(new SqlParameter("@O_StockName",entity.O_StockName));
                paramList.Add(new SqlParameter("@O_OrderNo",entity.O_OrderNo));
                paramList.Add(new SqlParameter("@O_OrderDate",entity.O_OrderDate));
                paramList.Add(new SqlParameter("@O_Status",entity.O_Status));
                paramList.Add(new SqlParameter("@O_CreateBy",entity.O_CreateBy));
                paramList.Add(new SqlParameter("@O_CreateDate",entity.O_CreateDate));
                paramList.Add(new SqlParameter("@O_UpdateBy",entity.O_UpdateBy));
                paramList.Add(new SqlParameter("@O_UpdateDate",entity.O_UpdateDate));
                paramList.Add(new SqlParameter("@O_Remark",entity.O_Remark));
                paramList.Add(new SqlParameter("@O_DeleteBy",entity.O_DeleteBy));
                paramList.Add(new SqlParameter("@O_DeleteDate",entity.O_DeleteDate));
                paramList.Add(new SqlParameter("@O_UploadBy",entity.O_UploadBy));
                paramList.Add(new SqlParameter("@O_UploadDate",entity.O_UploadDate));
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
