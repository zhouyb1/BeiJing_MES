using System;
using System.Collections.Generic;
using System.Linq;
using Model.Dto;
using Model;
using DataAccess.SqlServer;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Business.System
{
    /// <summary>
    /// 描述: 业务层 -- AM_Base_AnnexesFile
    /// </summary>
    public partial class AMBaseAnnexesFileBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
		public List<AMBaseAnnexesFileEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM AM_Base_AnnexesFile");
                var rows = db.ExecuteObjects<AMBaseAnnexesFileEntity>(strSql.ToString());
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
        /// <param name="F_Id">主键</param>
        /// <returns>AMBaseAnnexesFile</returns>
        public AMBaseAnnexesFileEntity GetEntity(string F_Id)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM AM_Base_AnnexesFile");
                strSql.Append(" WHERE F_Id=@F_Id");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@F_Id", F_Id));
                var rowData = db.ExecuteObject<AMBaseAnnexesFileEntity>(strSql.ToString(),paramList.ToArray());
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
				strSql.Append("DELETE AM_Base_AnnexesFile");
                strSql.Append(" WHERE F_Id=@ID");
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
        public int SaveEntity(string keyValue,AMBaseAnnexesFileEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO AM_Base_AnnexesFile(");
                     strSql.Append("F_Id,");
                     strSql.Append("F_FolderId,");
                     strSql.Append("F_FileName,");
                     strSql.Append("F_FilePath,");
                     strSql.Append("F_FileSize,");
                     strSql.Append("F_FileExtensions,");
                     strSql.Append("F_FileType,");
                     strSql.Append("F_DownloadCount,");
                     strSql.Append("F_CreateDate,");
                     strSql.Append("F_CreateUserId,");
                     strSql.Append("F_CreateUserName");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@F_Id,");
                     strSql.Append("@F_FolderId,");
                     strSql.Append("@F_FileName,");
                     strSql.Append("@F_FilePath,");
                     strSql.Append("@F_FileSize,");
                     strSql.Append("@F_FileExtensions,");
                     strSql.Append("@F_FileType,");
                     strSql.Append("@F_DownloadCount,");
                     strSql.Append("@F_CreateDate,");
                     strSql.Append("@F_CreateUserId,");
                     strSql.Append("@F_CreateUserName");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@F_Id", entity.F_Id));
                     //paramList.Add(new SqlParameter("@F_Id",Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE AM_Base_AnnexesFile SET ");
                     strSql.Append("F_FolderId=@F_FolderId,");
                     strSql.Append("F_FileName=@F_FileName,");
                     strSql.Append("F_FilePath=@F_FilePath,");
                     strSql.Append("F_FileSize=@F_FileSize,");
                     strSql.Append("F_FileExtensions=@F_FileExtensions,");
                     strSql.Append("F_FileType=@F_FileType,");
                     strSql.Append("F_DownloadCount=@F_DownloadCount,");
                     strSql.Append("F_CreateDate=@F_CreateDate,");
                     strSql.Append("F_CreateUserId=@F_CreateUserId,");
                     strSql.Append("F_CreateUserName=@F_CreateUserName ");
                     strSql.Append(" WHERE F_Id=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@F_FolderId",entity.F_FolderId));
                paramList.Add(new SqlParameter("@F_FileName",entity.F_FileName));
                paramList.Add(new SqlParameter("@F_FilePath",entity.F_FilePath));
                paramList.Add(new SqlParameter("@F_FileSize",entity.F_FileSize));
                paramList.Add(new SqlParameter("@F_FileExtensions",entity.F_FileExtensions));
                paramList.Add(new SqlParameter("@F_FileType",entity.F_FileType));
                paramList.Add(new SqlParameter("@F_DownloadCount",entity.F_DownloadCount));
                paramList.Add(new SqlParameter("@F_CreateDate",entity.F_CreateDate));
                paramList.Add(new SqlParameter("@F_CreateUserId",entity.F_CreateUserId));
                paramList.Add(new SqlParameter("@F_CreateUserName",entity.F_CreateUserName));
				var result = db.ExecuteNonQuery(strSql.ToString(),paramList.ToArray());
                return result;
            }
            catch (Exception)
            {
                throw;
            }			
		}
        /// <summary>
        /// 更新数据 事务
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="AMBaseAnnexesFileEntity">实体</param>
        /// <returns>返回值大于0:操作成功</returns>
        public int SaveEntityTrans(string AM_Base_AnnexesFile_keyValue, string AM_Base_User_keyValue, string AM_Base_AnnexesFile_delete_keyValue, AMBaseAnnexesFileEntity AMBaseAnnexesFileEntity, SysUser AMBaseUserEntity)
        {
            SqlTrans trans = new SqlTrans();
            SqlConnection con = trans.TranConn(); ;
            SqlTransaction tran = trans.TransBegin(con);
            string role = "";
            try
            {
                var AM_Base_AnnexesFile_strSql = new StringBuilder();
                var AM_Base_AnnexesFile_paramList = new List<SqlParameter>();
                var AM_Base_User_strSql = new StringBuilder();
                var AM_Base_User_paramList = new List<SqlParameter>();
                var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                
                    AM_Base_AnnexesFile_strSql.Append("INSERT INTO AM_Base_AnnexesFile(");
                    AM_Base_AnnexesFile_strSql.Append("F_Id,");
                    AM_Base_AnnexesFile_strSql.Append("F_FolderId,");
                    AM_Base_AnnexesFile_strSql.Append("F_FileName,");
                    AM_Base_AnnexesFile_strSql.Append("F_FilePath,");
                    AM_Base_AnnexesFile_strSql.Append("F_FileSize,");
                    AM_Base_AnnexesFile_strSql.Append("F_FileExtensions,");
                    AM_Base_AnnexesFile_strSql.Append("F_FileType,");
                    AM_Base_AnnexesFile_strSql.Append("F_DownloadCount,");
                    AM_Base_AnnexesFile_strSql.Append("F_CreateDate,");
                    AM_Base_AnnexesFile_strSql.Append("F_CreateUserId,");
                    AM_Base_AnnexesFile_strSql.Append("F_CreateUserName");
                    AM_Base_AnnexesFile_strSql.Append(")");
                    AM_Base_AnnexesFile_strSql.Append("VALUES(");
                    AM_Base_AnnexesFile_strSql.Append("@F_Id,");
                    AM_Base_AnnexesFile_strSql.Append("@F_FolderId,");
                    AM_Base_AnnexesFile_strSql.Append("@F_FileName,");
                    AM_Base_AnnexesFile_strSql.Append("@F_FilePath,");
                    AM_Base_AnnexesFile_strSql.Append("@F_FileSize,");
                    AM_Base_AnnexesFile_strSql.Append("@F_FileExtensions,");
                    AM_Base_AnnexesFile_strSql.Append("@F_FileType,");
                    AM_Base_AnnexesFile_strSql.Append("@F_DownloadCount,");
                    AM_Base_AnnexesFile_strSql.Append("@F_CreateDate,");
                    AM_Base_AnnexesFile_strSql.Append("@F_CreateUserId,");
                    AM_Base_AnnexesFile_strSql.Append("@F_CreateUserName");
                    AM_Base_AnnexesFile_strSql.Append(")");
                    AM_Base_AnnexesFile_paramList.Add(new SqlParameter("@F_Id", AM_Base_AnnexesFile_keyValue));

                AM_Base_AnnexesFile_paramList.Add(new SqlParameter("@F_FolderId", AMBaseAnnexesFileEntity.F_FolderId));
                AM_Base_AnnexesFile_paramList.Add(new SqlParameter("@F_FileName", AMBaseAnnexesFileEntity.F_FileName));
                AM_Base_AnnexesFile_paramList.Add(new SqlParameter("@F_FilePath", AMBaseAnnexesFileEntity.F_FilePath));
                AM_Base_AnnexesFile_paramList.Add(new SqlParameter("@F_FileSize", AMBaseAnnexesFileEntity.F_FileSize));
                AM_Base_AnnexesFile_paramList.Add(new SqlParameter("@F_FileExtensions", AMBaseAnnexesFileEntity.F_FileExtensions));
                AM_Base_AnnexesFile_paramList.Add(new SqlParameter("@F_FileType", AMBaseAnnexesFileEntity.F_FileType));
                AM_Base_AnnexesFile_paramList.Add(new SqlParameter("@F_DownloadCount", AMBaseAnnexesFileEntity.F_DownloadCount));
                AM_Base_AnnexesFile_paramList.Add(new SqlParameter("@F_CreateDate", AMBaseAnnexesFileEntity.F_CreateDate));
                AM_Base_AnnexesFile_paramList.Add(new SqlParameter("@F_CreateUserId", AMBaseAnnexesFileEntity.F_CreateUserId));
                AM_Base_AnnexesFile_paramList.Add(new SqlParameter("@F_CreateUserName", AMBaseAnnexesFileEntity.F_CreateUserName));

                SqlHelper.ExecuteNonQuery(tran, CommandType.Text, AM_Base_AnnexesFile_strSql.ToString(), AM_Base_AnnexesFile_paramList.ToArray());

                AM_Base_User_strSql.Append("UPDATE AM_Base_User SET ");
                     AM_Base_User_strSql.Append("F_EnCode=@F_EnCode,");
                     AM_Base_User_strSql.Append("F_Account=@F_Account,");
                     AM_Base_User_strSql.Append("F_Password=@F_Password,");
                     AM_Base_User_strSql.Append("F_Secretkey=@F_Secretkey,");
                     AM_Base_User_strSql.Append("F_RealName=@F_RealName,");
                     AM_Base_User_strSql.Append("F_NickName=@F_NickName,");
                     AM_Base_User_strSql.Append("F_HeadIcon=@F_HeadIcon,");
                     AM_Base_User_strSql.Append("F_QuickQuery=@F_QuickQuery,");
                     AM_Base_User_strSql.Append("F_SimpleSpelling=@F_SimpleSpelling,");
                     AM_Base_User_strSql.Append("F_Gender=@F_Gender,");
                     AM_Base_User_strSql.Append("F_Birthday=@F_Birthday,");
                     AM_Base_User_strSql.Append("F_Mobile=@F_Mobile,");
                     AM_Base_User_strSql.Append("F_Telephone=@F_Telephone,");
                     AM_Base_User_strSql.Append("F_Email=@F_Email,");
                     AM_Base_User_strSql.Append("F_OICQ=@F_OICQ,");
                     AM_Base_User_strSql.Append("F_WeChat=@F_WeChat,");
                     AM_Base_User_strSql.Append("F_MSN=@F_MSN,");
                     AM_Base_User_strSql.Append("F_CompanyId=@F_CompanyId,");
                     AM_Base_User_strSql.Append("F_DepartmentId=@F_DepartmentId,");
                     AM_Base_User_strSql.Append("F_SecurityLevel=@F_SecurityLevel,");
                     AM_Base_User_strSql.Append("F_OpenId=@F_OpenId,");
                     AM_Base_User_strSql.Append("F_Question=@F_Question,");
                     AM_Base_User_strSql.Append("F_AnswerQuestion=@F_AnswerQuestion,");
                     AM_Base_User_strSql.Append("F_CheckOnLine=@F_CheckOnLine,");
                     AM_Base_User_strSql.Append("F_AllowStartTime=@F_AllowStartTime,");
                     AM_Base_User_strSql.Append("F_AllowEndTime=@F_AllowEndTime,");
                     AM_Base_User_strSql.Append("F_LockStartDate=@F_LockStartDate,");
                     AM_Base_User_strSql.Append("F_LockEndDate=@F_LockEndDate,");
                     AM_Base_User_strSql.Append("F_SortCode=@F_SortCode,");
                     AM_Base_User_strSql.Append("F_DeleteMark=@F_DeleteMark,");
                     AM_Base_User_strSql.Append("F_EnabledMark=@F_EnabledMark,");
                     AM_Base_User_strSql.Append("F_Description=@F_Description,");
                     AM_Base_User_strSql.Append("F_CreateDate=@F_CreateDate,");
                     AM_Base_User_strSql.Append("F_CreateUserId=@F_CreateUserId,");
                     AM_Base_User_strSql.Append("F_CreateUserName=@F_CreateUserName,");
                     AM_Base_User_strSql.Append("F_ModifyDate=@F_ModifyDate,");
                     AM_Base_User_strSql.Append("F_ModifyUserId=@F_ModifyUserId,");
                     AM_Base_User_strSql.Append("F_ModifyUserName=@F_ModifyUserName,");
                     AM_Base_User_strSql.Append("U_Address=@U_Address,");
                     AM_Base_User_strSql.Append("D_Code=@D_Code,");
                     AM_Base_User_strSql.Append("R_Code=@R_Code,");
                     AM_Base_User_strSql.Append("F_Kind=@F_Kind,");
                     AM_Base_User_strSql.Append("F_RFIDCode=@F_RFIDCode,");
                     AM_Base_User_strSql.Append("F_Group=@F_Group,");
                     AM_Base_User_strSql.Append("F_Indate=@F_Indate,");
                     AM_Base_User_strSql.Append("F_Outdate=@F_Outdate,");
                     AM_Base_User_strSql.Append("F_Cert=@F_Cert,");
                     AM_Base_User_strSql.Append("F_Nation=@F_Nation,");
                     AM_Base_User_strSql.Append("F_Record=@F_Record,");
                     AM_Base_User_strSql.Append("F_Origin=@F_Origin,");
                     AM_Base_User_strSql.Append("F_Picture1=@F_Picture1,");
                     AM_Base_User_strSql.Append("F_Picture2=@F_Picture2,");
                     AM_Base_User_strSql.Append("F_Picture3=@F_Picture3,");
                     AM_Base_User_strSql.Append("F_Picture4=@F_Picture4,");
                     AM_Base_User_strSql.Append("F_Picture5=@F_Picture5 ");
                     AM_Base_User_strSql.Append(" WHERE F_Account=@ID");
                     AM_Base_User_paramList.Add(new SqlParameter("@ID", AM_Base_User_keyValue));

                     AM_Base_User_paramList.Add(new SqlParameter("@F_EnCode", AMBaseUserEntity.F_EnCode));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_Account", AMBaseUserEntity.F_Account));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_Password", AMBaseUserEntity.F_Password));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_Secretkey", AMBaseUserEntity.F_Secretkey));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_RealName", AMBaseUserEntity.F_RealName));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_NickName", AMBaseUserEntity.F_NickName));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_HeadIcon", AMBaseUserEntity.F_HeadIcon));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_QuickQuery", AMBaseUserEntity.F_QuickQuery));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_SimpleSpelling", AMBaseUserEntity.F_SimpleSpelling));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_Gender", AMBaseUserEntity.F_Gender));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_Birthday", AMBaseUserEntity.F_Birthday));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_Mobile", AMBaseUserEntity.F_Mobile));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_Telephone", AMBaseUserEntity.F_Telephone));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_Email", AMBaseUserEntity.F_Email));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_OICQ", AMBaseUserEntity.F_OICQ));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_WeChat", AMBaseUserEntity.F_WeChat));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_MSN", AMBaseUserEntity.F_MSN));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_CompanyId", AMBaseUserEntity.F_CompanyId));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_DepartmentId", AMBaseUserEntity.F_DepartmentId));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_SecurityLevel", AMBaseUserEntity.F_SecurityLevel));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_OpenId", AMBaseUserEntity.F_OpenId));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_Question", AMBaseUserEntity.F_Question));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_AnswerQuestion", AMBaseUserEntity.F_AnswerQuestion));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_CheckOnLine", AMBaseUserEntity.F_CheckOnLine));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_AllowStartTime", AMBaseUserEntity.F_AllowStartTime));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_AllowEndTime", AMBaseUserEntity.F_AllowEndTime));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_LockStartDate", AMBaseUserEntity.F_LockStartDate));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_LockEndDate", AMBaseUserEntity.F_LockEndDate));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_SortCode", AMBaseUserEntity.F_SortCode));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_DeleteMark", AMBaseUserEntity.F_DeleteMark));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_EnabledMark", AMBaseUserEntity.F_EnabledMark));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_Description", AMBaseUserEntity.F_Description));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_CreateDate", AMBaseUserEntity.F_CreateDate));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_CreateUserId", AMBaseUserEntity.F_CreateUserId));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_CreateUserName", AMBaseUserEntity.F_CreateUserName));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_ModifyDate", AMBaseUserEntity.F_ModifyDate));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_ModifyUserId", AMBaseUserEntity.F_ModifyUserId));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_ModifyUserName", AMBaseUserEntity.F_ModifyUserName));
                     AM_Base_User_paramList.Add(new SqlParameter("@U_Address", AMBaseUserEntity.U_Address));
                     AM_Base_User_paramList.Add(new SqlParameter("@D_Code", AMBaseUserEntity.D_Code));
                     AM_Base_User_paramList.Add(new SqlParameter("@R_Code", AMBaseUserEntity.R_Code));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_Kind", AMBaseUserEntity.F_Kind));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_RFIDCode", AMBaseUserEntity.F_RFIDCode));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_Group", AMBaseUserEntity.F_Group));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_Indate", AMBaseUserEntity.F_Indate));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_Outdate", AMBaseUserEntity.F_Outdate));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_Cert", AMBaseUserEntity.F_Cert));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_Nation", AMBaseUserEntity.F_Nation));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_Record", AMBaseUserEntity.F_Record));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_Origin", AMBaseUserEntity.F_Origin));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_Picture1", AMBaseUserEntity.F_Picture1));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_Picture2", AMBaseUserEntity.F_Picture2));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_Picture3", AMBaseUserEntity.F_Picture3));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_Picture4", AMBaseUserEntity.F_Picture4));
                     AM_Base_User_paramList.Add(new SqlParameter("@F_Picture5", AMBaseUserEntity.F_Picture5));

                     SqlHelper.ExecuteNonQuery(tran, CommandType.Text, AM_Base_User_strSql.ToString(), AM_Base_User_paramList.ToArray());

                     strSql.Append("DELETE AM_Base_AnnexesFile");
                     strSql.Append(" WHERE F_Id=@ID");

                     paramList.Add(new SqlParameter("@ID", AM_Base_AnnexesFile_delete_keyValue));

                     SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), paramList.ToArray());

                //var result = db.ExecuteNonQuery(AM_Base_AnnexesFile_strSql.ToString(), AM_Base_AnnexesFile_paramList.ToArray());
                //return result;

                trans.TransCommit(tran);
                trans.TransEnd(con);

                return 1;
            }
            catch (Exception)
            {
                trans.TransRollback(tran);
                trans.TransEnd(con);

                throw;
            }
        }
    }
}
