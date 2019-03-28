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
    /// 描述: 业务层 -- AM_Base_User
    /// </summary>
    public partial class AMBaseUserBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
		public List<AMBaseUserEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM AM_Base_User");
                var rows = db.ExecuteObjects<AMBaseUserEntity>(strSql.ToString());
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
        /// <returns>AMBaseUser</returns>
		public AMBaseUserEntity GetEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM AM_Base_User");
                strSql.Append(" WHERE F_UserId=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var rowData = db.ExecuteObject<AMBaseUserEntity>(strSql.ToString(),paramList.ToArray());
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
				strSql.Append("DELETE AM_Base_User");
                strSql.Append(" WHERE F_UserId=@ID");
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
        public int SaveEntity(string keyValue,AMBaseUserEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO AM_Base_User(");
                     strSql.Append("F_UserId,");
                     strSql.Append("F_EnCode,");
                     strSql.Append("F_Account,");
                     strSql.Append("F_Password,");
                     strSql.Append("F_Secretkey,");
                     strSql.Append("F_RealName,");
                     strSql.Append("F_NickName,");
                     strSql.Append("F_HeadIcon,");
                     strSql.Append("F_QuickQuery,");
                     strSql.Append("F_SimpleSpelling,");
                     strSql.Append("F_Gender,");
                     strSql.Append("F_Birthday,");
                     strSql.Append("F_Mobile,");
                     strSql.Append("F_Telephone,");
                     strSql.Append("F_Email,");
                     strSql.Append("F_OICQ,");
                     strSql.Append("F_WeChat,");
                     strSql.Append("F_MSN,");
                     strSql.Append("F_CompanyId,");
                     strSql.Append("F_DepartmentId,");
                     strSql.Append("F_SecurityLevel,");
                     strSql.Append("F_OpenId,");
                     strSql.Append("F_Question,");
                     strSql.Append("F_AnswerQuestion,");
                     strSql.Append("F_CheckOnLine,");
                     strSql.Append("F_AllowStartTime,");
                     strSql.Append("F_AllowEndTime,");
                     strSql.Append("F_LockStartDate,");
                     strSql.Append("F_LockEndDate,");
                     strSql.Append("F_SortCode,");
                     strSql.Append("F_DeleteMark,");
                     strSql.Append("F_EnabledMark,");
                     strSql.Append("F_Description,");
                     strSql.Append("F_CreateDate,");
                     strSql.Append("F_CreateUserId,");
                     strSql.Append("F_CreateUserName,");
                     strSql.Append("F_ModifyDate,");
                     strSql.Append("F_ModifyUserId,");
                     strSql.Append("F_ModifyUserName,");
                     strSql.Append("U_Address,");
                     strSql.Append("D_Code,");
                     strSql.Append("R_Code,");
                     strSql.Append("F_Kind,");
                     strSql.Append("F_RFIDCode,");
                     strSql.Append("F_Group,");
                     strSql.Append("F_Indate,");
                     strSql.Append("F_Outdate,");
                     strSql.Append("F_Cert,");
                     strSql.Append("F_Nation,");
                     strSql.Append("F_Record,");
                     strSql.Append("F_Origin,");
                     strSql.Append("F_Picture1,");
                     strSql.Append("F_Picture2,");
                     strSql.Append("F_Picture3,");
                     strSql.Append("F_Picture4,");
                     strSql.Append("F_Picture5");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@F_UserId,");
                     strSql.Append("@F_EnCode,");
                     strSql.Append("@F_Account,");
                     strSql.Append("@F_Password,");
                     strSql.Append("@F_Secretkey,");
                     strSql.Append("@F_RealName,");
                     strSql.Append("@F_NickName,");
                     strSql.Append("@F_HeadIcon,");
                     strSql.Append("@F_QuickQuery,");
                     strSql.Append("@F_SimpleSpelling,");
                     strSql.Append("@F_Gender,");
                     strSql.Append("@F_Birthday,");
                     strSql.Append("@F_Mobile,");
                     strSql.Append("@F_Telephone,");
                     strSql.Append("@F_Email,");
                     strSql.Append("@F_OICQ,");
                     strSql.Append("@F_WeChat,");
                     strSql.Append("@F_MSN,");
                     strSql.Append("@F_CompanyId,");
                     strSql.Append("@F_DepartmentId,");
                     strSql.Append("@F_SecurityLevel,");
                     strSql.Append("@F_OpenId,");
                     strSql.Append("@F_Question,");
                     strSql.Append("@F_AnswerQuestion,");
                     strSql.Append("@F_CheckOnLine,");
                     strSql.Append("@F_AllowStartTime,");
                     strSql.Append("@F_AllowEndTime,");
                     strSql.Append("@F_LockStartDate,");
                     strSql.Append("@F_LockEndDate,");
                     strSql.Append("@F_SortCode,");
                     strSql.Append("@F_DeleteMark,");
                     strSql.Append("@F_EnabledMark,");
                     strSql.Append("@F_Description,");
                     strSql.Append("@F_CreateDate,");
                     strSql.Append("@F_CreateUserId,");
                     strSql.Append("@F_CreateUserName,");
                     strSql.Append("@F_ModifyDate,");
                     strSql.Append("@F_ModifyUserId,");
                     strSql.Append("@F_ModifyUserName,");
                     strSql.Append("@U_Address,");
                     strSql.Append("@D_Code,");
                     strSql.Append("@R_Code,");
                     strSql.Append("@F_Kind,");
                     strSql.Append("@F_RFIDCode,");
                     strSql.Append("@F_Group,");
                     strSql.Append("@F_Indate,");
                     strSql.Append("@F_Outdate,");
                     strSql.Append("@F_Cert,");
                     strSql.Append("@F_Nation,");
                     strSql.Append("@F_Record,");
                     strSql.Append("@F_Origin,");
                     strSql.Append("@F_Picture1,");
                     strSql.Append("@F_Picture2,");
                     strSql.Append("@F_Picture3,");
                     strSql.Append("@F_Picture4,");
                     strSql.Append("@F_Picture5");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@F_UserId",Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE AM_Base_User SET ");
                     strSql.Append("F_EnCode=@F_EnCode,");
                     strSql.Append("F_Account=@F_Account,");
                     strSql.Append("F_Password=@F_Password,");
                     strSql.Append("F_Secretkey=@F_Secretkey,");
                     strSql.Append("F_RealName=@F_RealName,");
                     strSql.Append("F_NickName=@F_NickName,");
                     strSql.Append("F_HeadIcon=@F_HeadIcon,");
                     strSql.Append("F_QuickQuery=@F_QuickQuery,");
                     strSql.Append("F_SimpleSpelling=@F_SimpleSpelling,");
                     strSql.Append("F_Gender=@F_Gender,");
                     strSql.Append("F_Birthday=@F_Birthday,");
                     strSql.Append("F_Mobile=@F_Mobile,");
                     strSql.Append("F_Telephone=@F_Telephone,");
                     strSql.Append("F_Email=@F_Email,");
                     strSql.Append("F_OICQ=@F_OICQ,");
                     strSql.Append("F_WeChat=@F_WeChat,");
                     strSql.Append("F_MSN=@F_MSN,");
                     strSql.Append("F_CompanyId=@F_CompanyId,");
                     strSql.Append("F_DepartmentId=@F_DepartmentId,");
                     strSql.Append("F_SecurityLevel=@F_SecurityLevel,");
                     strSql.Append("F_OpenId=@F_OpenId,");
                     strSql.Append("F_Question=@F_Question,");
                     strSql.Append("F_AnswerQuestion=@F_AnswerQuestion,");
                     strSql.Append("F_CheckOnLine=@F_CheckOnLine,");
                     strSql.Append("F_AllowStartTime=@F_AllowStartTime,");
                     strSql.Append("F_AllowEndTime=@F_AllowEndTime,");
                     strSql.Append("F_LockStartDate=@F_LockStartDate,");
                     strSql.Append("F_LockEndDate=@F_LockEndDate,");
                     strSql.Append("F_SortCode=@F_SortCode,");
                     strSql.Append("F_DeleteMark=@F_DeleteMark,");
                     strSql.Append("F_EnabledMark=@F_EnabledMark,");
                     strSql.Append("F_Description=@F_Description,");
                     strSql.Append("F_CreateDate=@F_CreateDate,");
                     strSql.Append("F_CreateUserId=@F_CreateUserId,");
                     strSql.Append("F_CreateUserName=@F_CreateUserName,");
                     strSql.Append("F_ModifyDate=@F_ModifyDate,");
                     strSql.Append("F_ModifyUserId=@F_ModifyUserId,");
                     strSql.Append("F_ModifyUserName=@F_ModifyUserName,");
                     strSql.Append("U_Address=@U_Address,");
                     strSql.Append("D_Code=@D_Code,");
                     strSql.Append("R_Code=@R_Code,");
                     strSql.Append("F_Kind=@F_Kind,");
                     strSql.Append("F_RFIDCode=@F_RFIDCode,");
                     strSql.Append("F_Group=@F_Group,");
                     strSql.Append("F_Indate=@F_Indate,");
                     strSql.Append("F_Outdate=@F_Outdate,");
                     strSql.Append("F_Cert=@F_Cert,");
                     strSql.Append("F_Nation=@F_Nation,");
                     strSql.Append("F_Record=@F_Record,");
                     strSql.Append("F_Origin=@F_Origin,");
                     strSql.Append("F_Picture1=@F_Picture1,");
                     strSql.Append("F_Picture2=@F_Picture2,");
                     strSql.Append("F_Picture3=@F_Picture3,");
                     strSql.Append("F_Picture4=@F_Picture4,");
                     strSql.Append("F_Picture5=@F_Picture5 ");
                     strSql.Append(" WHERE F_UserId=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@F_EnCode",entity.F_EnCode));
                paramList.Add(new SqlParameter("@F_Account",entity.F_Account));
                paramList.Add(new SqlParameter("@F_Password",entity.F_Password));
                paramList.Add(new SqlParameter("@F_Secretkey",entity.F_Secretkey));
                paramList.Add(new SqlParameter("@F_RealName",entity.F_RealName));
                paramList.Add(new SqlParameter("@F_NickName",entity.F_NickName));
                paramList.Add(new SqlParameter("@F_HeadIcon",entity.F_HeadIcon));
                paramList.Add(new SqlParameter("@F_QuickQuery",entity.F_QuickQuery));
                paramList.Add(new SqlParameter("@F_SimpleSpelling",entity.F_SimpleSpelling));
                paramList.Add(new SqlParameter("@F_Gender",entity.F_Gender));
                paramList.Add(new SqlParameter("@F_Birthday",entity.F_Birthday));
                paramList.Add(new SqlParameter("@F_Mobile",entity.F_Mobile));
                paramList.Add(new SqlParameter("@F_Telephone",entity.F_Telephone));
                paramList.Add(new SqlParameter("@F_Email",entity.F_Email));
                paramList.Add(new SqlParameter("@F_OICQ",entity.F_OICQ));
                paramList.Add(new SqlParameter("@F_WeChat",entity.F_WeChat));
                paramList.Add(new SqlParameter("@F_MSN",entity.F_MSN));
                paramList.Add(new SqlParameter("@F_CompanyId",entity.F_CompanyId));
                paramList.Add(new SqlParameter("@F_DepartmentId",entity.F_DepartmentId));
                paramList.Add(new SqlParameter("@F_SecurityLevel",entity.F_SecurityLevel));
                paramList.Add(new SqlParameter("@F_OpenId",entity.F_OpenId));
                paramList.Add(new SqlParameter("@F_Question",entity.F_Question));
                paramList.Add(new SqlParameter("@F_AnswerQuestion",entity.F_AnswerQuestion));
                paramList.Add(new SqlParameter("@F_CheckOnLine",entity.F_CheckOnLine));
                paramList.Add(new SqlParameter("@F_AllowStartTime",entity.F_AllowStartTime));
                paramList.Add(new SqlParameter("@F_AllowEndTime",entity.F_AllowEndTime));
                paramList.Add(new SqlParameter("@F_LockStartDate",entity.F_LockStartDate));
                paramList.Add(new SqlParameter("@F_LockEndDate",entity.F_LockEndDate));
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
                paramList.Add(new SqlParameter("@U_Address",entity.U_Address));
                paramList.Add(new SqlParameter("@D_Code",entity.D_Code));
                paramList.Add(new SqlParameter("@R_Code",entity.R_Code));
                paramList.Add(new SqlParameter("@F_Kind",entity.F_Kind));
                paramList.Add(new SqlParameter("@F_RFIDCode",entity.F_RFIDCode));
                paramList.Add(new SqlParameter("@F_Group",entity.F_Group));
                paramList.Add(new SqlParameter("@F_Indate",entity.F_Indate));
                paramList.Add(new SqlParameter("@F_Outdate",entity.F_Outdate));
                paramList.Add(new SqlParameter("@F_Cert",entity.F_Cert));
                paramList.Add(new SqlParameter("@F_Nation",entity.F_Nation));
                paramList.Add(new SqlParameter("@F_Record",entity.F_Record));
                paramList.Add(new SqlParameter("@F_Origin",entity.F_Origin));
                paramList.Add(new SqlParameter("@F_Picture1",entity.F_Picture1));
                paramList.Add(new SqlParameter("@F_Picture2",entity.F_Picture2));
                paramList.Add(new SqlParameter("@F_Picture3",entity.F_Picture3));
                paramList.Add(new SqlParameter("@F_Picture4",entity.F_Picture4));
                paramList.Add(new SqlParameter("@F_Picture5",entity.F_Picture5));
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
