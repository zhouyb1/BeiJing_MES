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
    /// 描述: 业务层 -- AM_Base_Department
    /// </summary>
    public partial class AMBaseDepartmentBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
		public List<AMBaseDepartmentEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM AM_Base_Department");
                var rows = db.ExecuteObjects<AMBaseDepartmentEntity>(strSql.ToString());
                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 根据部门ID获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<AMBaseDepartmentEntity> GetList_ID(string ID)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT [F_DepartmentId]
      ,[F_CompanyId]
      ,[F_ParentId]
      ,[F_EnCode]
      ,[F_FullName]
      ,[F_ShortName]
      ,[F_Nature]
      ,[F_Manager]
      ,[F_OuterPhone]
      ,[F_InnerPhone]
      ,[F_Email]
      ,[F_Fax]
      ,[F_SortCode]
      ,[F_DeleteMark]
      ,[F_EnabledMark]
      ,[F_Description]
      ,[F_CreateDate]
      ,[F_CreateUserId]
      ,[F_CreateUserName]
      ,[F_ModifyDate]
      ,[F_ModifyUserId]
      ,[F_ModifyUserName]
  FROM [dbo].[AM_Base_Department]");
                strSql.Append(" WHERE F_DepartmentId = @F_DepartmentId");
                var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@F_DepartmentId", string.Format("{0}", ID)));
                var rows = db.ExecuteObjects<AMBaseDepartmentEntity>(strSql.ToString(), paramList.ToArray());
                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 通过部门名称获取部门ID
        /// </summary>
        /// <returns></returns>
        public List<AMBaseDepartmentEntity> GetList_F_ID(string F_FullName)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT F_DepartmentId FROM AM_Base_Department");
                strSql.Append(" WHERE F_FullName = @F_FullName");
                var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@F_FullName", string.Format("{0}", F_FullName)));
                var rows = db.ExecuteObjects<AMBaseDepartmentEntity>(strSql.ToString(), paramList.ToArray());
                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取部门名称
        /// </summary>
        /// <returns></returns>
        public List<AMBaseDepartmentEntity> GetList_D_Name()
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT F_FullName FROM AM_Base_Department where F_DeleteMark='0' and F_CompanyId='207fa1a9-160c-4943-a89b-8fa4db0547ce'");
                var rows = db.ExecuteObjects<AMBaseDepartmentEntity>(strSql.ToString());
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
        /// <returns>AMBaseDepartment</returns>
		public AMBaseDepartmentEntity GetEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM AM_Base_Department");
                strSql.Append(" WHERE F_DepartmentId=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var rowData = db.ExecuteObject<AMBaseDepartmentEntity>(strSql.ToString(),paramList.ToArray());
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
				strSql.Append("DELETE AM_Base_Department");
                strSql.Append(" WHERE F_DepartmentId=@ID");
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
        public int SaveEntity(string keyValue,AMBaseDepartmentEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO AM_Base_Department(");
                     strSql.Append("F_DepartmentId,");
                     strSql.Append("F_CompanyId,");
                     strSql.Append("F_ParentId,");
                     strSql.Append("F_EnCode,");
                     strSql.Append("F_FullName,");
                     strSql.Append("F_ShortName,");
                     strSql.Append("F_Nature,");
                     strSql.Append("F_Manager,");
                     strSql.Append("F_OuterPhone,");
                     strSql.Append("F_InnerPhone,");
                     strSql.Append("F_Email,");
                     strSql.Append("F_Fax,");
                     strSql.Append("F_SortCode,");
                     strSql.Append("F_DeleteMark,");
                     strSql.Append("F_EnabledMark,");
                     strSql.Append("F_Description,");
                     strSql.Append("F_CreateDate,");
                     strSql.Append("F_CreateUserId,");
                     strSql.Append("F_CreateUserName,");
                     strSql.Append("F_ModifyDate,");
                     strSql.Append("F_ModifyUserId,");
                     strSql.Append("F_ModifyUserName");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@F_DepartmentId,");
                     strSql.Append("@F_CompanyId,");
                     strSql.Append("@F_ParentId,");
                     strSql.Append("@F_EnCode,");
                     strSql.Append("@F_FullName,");
                     strSql.Append("@F_ShortName,");
                     strSql.Append("@F_Nature,");
                     strSql.Append("@F_Manager,");
                     strSql.Append("@F_OuterPhone,");
                     strSql.Append("@F_InnerPhone,");
                     strSql.Append("@F_Email,");
                     strSql.Append("@F_Fax,");
                     strSql.Append("@F_SortCode,");
                     strSql.Append("@F_DeleteMark,");
                     strSql.Append("@F_EnabledMark,");
                     strSql.Append("@F_Description,");
                     strSql.Append("@F_CreateDate,");
                     strSql.Append("@F_CreateUserId,");
                     strSql.Append("@F_CreateUserName,");
                     strSql.Append("@F_ModifyDate,");
                     strSql.Append("@F_ModifyUserId,");
                     strSql.Append("@F_ModifyUserName");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@F_DepartmentId",Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE AM_Base_Department SET ");
                     strSql.Append("F_CompanyId=@F_CompanyId,");
                     strSql.Append("F_ParentId=@F_ParentId,");
                     strSql.Append("F_EnCode=@F_EnCode,");
                     strSql.Append("F_FullName=@F_FullName,");
                     strSql.Append("F_ShortName=@F_ShortName,");
                     strSql.Append("F_Nature=@F_Nature,");
                     strSql.Append("F_Manager=@F_Manager,");
                     strSql.Append("F_OuterPhone=@F_OuterPhone,");
                     strSql.Append("F_InnerPhone=@F_InnerPhone,");
                     strSql.Append("F_Email=@F_Email,");
                     strSql.Append("F_Fax=@F_Fax,");
                     strSql.Append("F_SortCode=@F_SortCode,");
                     strSql.Append("F_DeleteMark=@F_DeleteMark,");
                     strSql.Append("F_EnabledMark=@F_EnabledMark,");
                     strSql.Append("F_Description=@F_Description,");
                     strSql.Append("F_CreateDate=@F_CreateDate,");
                     strSql.Append("F_CreateUserId=@F_CreateUserId,");
                     strSql.Append("F_CreateUserName=@F_CreateUserName,");
                     strSql.Append("F_ModifyDate=@F_ModifyDate,");
                     strSql.Append("F_ModifyUserId=@F_ModifyUserId,");
                     strSql.Append("F_ModifyUserName=@F_ModifyUserName ");
                     strSql.Append(" WHERE F_DepartmentId=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@F_CompanyId",entity.F_CompanyId));
                paramList.Add(new SqlParameter("@F_ParentId",entity.F_ParentId));
                paramList.Add(new SqlParameter("@F_EnCode",entity.F_EnCode));
                paramList.Add(new SqlParameter("@F_FullName",entity.F_FullName));
                paramList.Add(new SqlParameter("@F_ShortName",entity.F_ShortName));
                paramList.Add(new SqlParameter("@F_Nature",entity.F_Nature));
                paramList.Add(new SqlParameter("@F_Manager",entity.F_Manager));
                paramList.Add(new SqlParameter("@F_OuterPhone",entity.F_OuterPhone));
                paramList.Add(new SqlParameter("@F_InnerPhone",entity.F_InnerPhone));
                paramList.Add(new SqlParameter("@F_Email",entity.F_Email));
                paramList.Add(new SqlParameter("@F_Fax",entity.F_Fax));
                paramList.Add(new SqlParameter("@F_SortCode",entity.F_SortCode));
                paramList.Add(new SqlParameter("@F_DeleteMark",entity.F_DeleteMark));
                paramList.Add(new SqlParameter("@F_EnabledMark",entity.F_EnabledMark));
                paramList.Add(new SqlParameter("@F_Description",entity.F_Description));
                paramList.Add(new SqlParameter("@F_CreateDate",entity.F_CreateDate));
                paramList.Add(new SqlParameter("@F_CreateUserId",entity.F_CreateUserId));
                paramList.Add(new SqlParameter("@F_CreateUserName",entity.F_CreateUserName));
                paramList.Add(new SqlParameter("@F_ModifyDate",entity.F_ModifyDate));
                paramList.Add(new SqlParameter("@F_ModifyUserId",entity.F_ModifyUserId));
                paramList.Add(new SqlParameter("@F_ModifyUserName",entity.F_ModifyUserName));
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
