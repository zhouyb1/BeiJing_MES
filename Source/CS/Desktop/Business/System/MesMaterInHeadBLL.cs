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
    /// 描述: 业务层 -- Mes_MaterInHead
    /// </summary>
    public partial class MesMaterInHeadBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<MesMaterInHeadEntity> GetList(string M_MaterInNo)
        {
            try
            {
                var strSql = new StringBuilder();

                if (string.IsNullOrEmpty(M_MaterInNo))
                {
                    strSql.Append(@"SELECT [ID]
      ,(CASE M_Kind WHEN 0 THEN '原材料' 
                    WHEN 1 THEN '半成品'
					WHEN 2 THEN '成品'
                    ELSE '' END) M_Kind
      ,[M_MaterInNo]
      ,[M_StockCode]
      ,[M_StockName]
      ,[M_OrderNo]
      ,[M_OrderDate]
      , M_Status
      ,[M_CreateBy]
      ,[M_CreateDate]
      ,[M_UpdateBy]
      ,[M_UpdateDate]
      ,[M_Remark]
      ,[M_DeleteBy]
      ,[M_DeleteDate]
      ,[M_UploadBy]
      ,[M_UploadDate] FROM Mes_MaterInHead");
                    var rows = db.ExecuteObjects<MesMaterInHeadEntity>(strSql.ToString());
                    return rows;
                }
                else
                {
                    strSql.Append(@"SELECT [ID]
      ,(CASE M_OrderKind WHEN 1 THEN '半成品'
					WHEN 2 THEN '成品'
                    ELSE '' END) M_OrderKind
      ,[M_MaterInNo]
      ,[M_StockCode]
      ,[M_StockName]
      ,[M_OrderNo]
      ,[M_OrderDate]
      , M_Status
      ,[M_CreateBy]
      ,[M_CreateDate]
      ,[M_UpdateBy]
      ,[M_UpdateDate]
      ,[M_Remark]
      ,[M_DeleteBy]
      ,[M_DeleteDate]
      ,[M_UploadBy]
      ,[M_UploadDate] FROM Mes_MaterInHead");
                    strSql.Append(" WHERE M_MaterInNo LIKE @M_MaterInNo");
                    var paramList = new List<SqlParameter>();
                    paramList.Add(new SqlParameter("@M_MaterInNo", string.Format("%{0}%", M_MaterInNo)));
                    var rows = db.ExecuteObjects<MesMaterInHeadEntity>(strSql.ToString(), paramList.ToArray());
                    return rows;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

         /// <summary>
         /// 获取Mes_MaterInHead表最近的入库单
         /// </summary>
         /// <returns></returns>
         public List<MesMaterInHeadEntity> GetListMax()
         {
             try
             {
                 var strSql = new StringBuilder();
                 strSql.Append("SELECT top(1)* FROM Mes_MaterInHead");
                 strSql.Append(" order by M_MaterInNo desc");
                 var rows = db.ExecuteObjects<MesMaterInHeadEntity>(strSql.ToString());
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
        /// <returns>MesMaterInHead</returns>
		public MesMaterInHeadEntity GetEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_MaterInHead");
                strSql.Append(" WHERE ID=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var rowData = db.ExecuteObject<MesMaterInHeadEntity>(strSql.ToString(),paramList.ToArray());
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
				strSql.Append("DELETE Mes_MaterInHead");
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
        public int SaveEntity(string keyValue,MesMaterInHeadEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO Mes_MaterInHead(");
                     strSql.Append("ID,");
                     strSql.Append("M_MaterInNo,");
                     strSql.Append("M_StockCode,");
                     strSql.Append("M_Kind,");
                     strSql.Append("M_StockName,");
                     strSql.Append("M_OrderNo,");
                     strSql.Append("M_OrderDate,");
                     strSql.Append("M_Status,");
                     strSql.Append("M_CreateBy,");
                     strSql.Append("M_CreateDate,");
                     strSql.Append("M_UpdateBy,");
                     strSql.Append("M_UpdateDate,");
                     strSql.Append("M_Remark,");
                     strSql.Append("M_DeleteBy,");
                     strSql.Append("M_DeleteDate,");
                     strSql.Append("M_UploadBy,");
                     strSql.Append("M_UploadDate");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@ID,");
                     strSql.Append("@M_MaterInNo,");
                     strSql.Append("@M_StockCode,");
                     strSql.Append("@M_Kind,");
                     strSql.Append("@M_StockName,");
                     strSql.Append("@M_OrderNo,");
                     strSql.Append("@M_OrderDate,");
                     strSql.Append("@M_Status,");
                     strSql.Append("@M_CreateBy,");
                     strSql.Append("@M_CreateDate,");
                     strSql.Append("@M_UpdateBy,");
                     strSql.Append("@M_UpdateDate,");
                     strSql.Append("@M_Remark,");
                     strSql.Append("@M_DeleteBy,");
                     strSql.Append("@M_DeleteDate,");
                     strSql.Append("@M_UploadBy,");
                     strSql.Append("@M_UploadDate");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@ID",Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE Mes_MaterInHead SET ");
                     strSql.Append("M_MaterInNo=@M_MaterInNo,");
                     strSql.Append("M_StockCode=@M_StockCode,");
                     strSql.Append("M_Kind=@M_Kind,");
                     strSql.Append("M_StockName=@M_StockName,");
                     strSql.Append("M_OrderNo=@M_OrderNo,");
                     strSql.Append("M_OrderDate=@M_OrderDate,");
                     strSql.Append("M_Status=@M_Status,");
                     strSql.Append("M_CreateBy=@M_CreateBy,");
                     strSql.Append("M_CreateDate=@M_CreateDate,");
                     strSql.Append("M_UpdateBy=@M_UpdateBy,");
                     strSql.Append("M_UpdateDate=@M_UpdateDate,");
                     strSql.Append("M_Remark=@M_Remark,");
                     strSql.Append("M_DeleteBy=@M_DeleteBy,");
                     strSql.Append("M_DeleteDate=@M_DeleteDate,");
                     strSql.Append("M_UploadBy=@M_UploadBy,");
                     strSql.Append("M_UploadDate=@M_UploadDate ");
                     strSql.Append(" WHERE ID=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@M_MaterInNo",entity.M_MaterInNo));
                paramList.Add(new SqlParameter("@M_StockCode",entity.M_StockCode));
                paramList.Add(new SqlParameter("@M_Kind", entity.M_Kind));
                paramList.Add(new SqlParameter("@M_StockName",entity.M_StockName));
                paramList.Add(new SqlParameter("@M_OrderNo",entity.M_OrderNo));
                paramList.Add(new SqlParameter("@M_OrderDate",entity.M_OrderDate));
                paramList.Add(new SqlParameter("@M_Status", entity.M_Status));
                paramList.Add(new SqlParameter("@M_CreateBy",entity.M_CreateBy));
                paramList.Add(new SqlParameter("@M_CreateDate",entity.M_CreateDate));
                paramList.Add(new SqlParameter("@M_UpdateBy",entity.M_UpdateBy));
                paramList.Add(new SqlParameter("@M_UpdateDate",entity.M_UpdateDate));
                paramList.Add(new SqlParameter("@M_Remark",entity.M_Remark));
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
