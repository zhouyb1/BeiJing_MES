using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.SqlServer;
using Model;
using Tools;

namespace Business
{
    public class SysUserBLL
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="U_Code"></param>
        /// <param name="U_Pwd"></param>
        /// <returns></returns>
        public SysUser Login(string U_Code, string U_Pwd)
        {
            try
            {
                string sql = @"SELECT * FROM AM_Base_User WHERE F_Account=@F_Account";
                SqlHelper db = new SqlHelper();
                var userEntity = db.ExecuteObject<SysUser>(sql, new SqlParameter("@F_Account", U_Code));
                if (userEntity == null)
                {
                    userEntity = new SysUser()
                    {
                        LoginMsg = "账户不存在!",
                        LoginOk = false
                    };
                    return userEntity;
                }
                //userEntity.LoginOk = false;
                if (userEntity.F_EnabledMark)
                {
                    U_Pwd = Md5Helper.Encrypt(U_Pwd, 32);
                    string dbPassword = Md5Helper.Encrypt(DESEncrypt.Encrypt(U_Pwd.ToLower(), userEntity.F_Secretkey).ToLower(), 32).ToLower();
                    if (dbPassword == userEntity.F_Password)
                    {
                        userEntity.LoginOk = true;
                    }
                    else
                    {
                        userEntity.LoginMsg = "密码和账户名不匹配!";
                    }
                }
                else
                {
                    userEntity.LoginMsg = "账户被系统锁定,请联系管理员!";
                }
                return userEntity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 密码修改
        /// </summary>
        /// <returns></returns>
        public int EditPassword(SysUser user)
        {
            try
            {
                string sql = @"UPDATE AM_Base_User SET F_Password=@F_Password WHERE F_Account=@F_Account";
                SqlHelper db = new SqlHelper();
                var paras = new List<SqlParameter>();
                paras.Add(new SqlParameter("@F_Account", user.F_Account));
                paras.Add(new SqlParameter("@F_Password", user.F_Password));
                return db.ExecuteNonQuery(sql, paras.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 不传入姓名时获取全部数据列表 也可传入姓名参数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public DataTable loadData(string key)
        {
            try
            {
                string sql = "";
                if (string.IsNullOrEmpty(key))//AM_Base_Department.F_FullName
                {
                    sql = @"SELECT  F_UserId,
        F_Account ,
        F_RealName ,
F_TeamName,
        '******' F_Password ,
        CASE F_Gender WHEN 1 THEN '男' ELSE '女' END StrGender ,
         AM_Base_Department.F_FullName  D_Code ,
        Sys_Role.R_Name R_Code ,
        F_Mobile ,
        AM_Base_User.F_Email,
        F_OICQ ,
        F_WeChat ,
        U_Address ,
        AM_Base_User.F_Description ,
        AM_Base_User.F_EnabledMark ,
        AM_Base_User.F_CreateUserName ,
        AM_Base_User.F_CreateDate ,
        AM_Base_User.F_ModifyUserName ,
        AM_Base_User.F_ModifyDate ,
		(CASE F_Kind 
                                       WHEN  1 THEN '正式工' 
                                       WHEN  2 THEN '临时工'
									   WHEN  3 THEN '劳务工'
                                       ELSE '' END) F_Kind,
        F_RFIDCode,
        F_Group,
        F_Indate,
        F_Outdate,
        F_Cert,
        F_Nation,
        F_Record,
        F_Origin,
        AM_Base_User.F_EnCode,
        AM_Base_User.F_DepartmentId,
        F_Picture1,
        F_Picture2,
        F_Picture3,
        F_Picture4,
        F_Picture5
FROM    AM_Base_User
        left JOIN AM_Base_Department ON AM_Base_Department.F_DepartmentId = AM_Base_User.F_DepartmentId
        LEFT JOIN Sys_Role ON Sys_Role.R_Code = AM_Base_User.F_Account";
                }
                else
                {
                    sql = string.Format(@"SELECT F_UserId, F_Account ,
        F_RealName ,
        '******' F_Password ,
        CASE F_Gender WHEN 1 THEN '男' ELSE '女' END StrGender ,
         AM_Base_Department.F_FullName  D_Code ,
        Sys_Role.R_Name R_Code ,
        F_Mobile ,
        AM_Base_User.F_Email,
        F_OICQ ,
        F_WeChat ,
        U_Address ,
        AM_Base_User.F_Description ,
        AM_Base_User.F_EnabledMark ,
        AM_Base_User.F_CreateUserName ,
        AM_Base_User.F_CreateDate ,
        AM_Base_User.F_ModifyUserName ,
        AM_Base_User.F_ModifyDate ,
		(CASE F_Kind 
                                       WHEN  1 THEN '正式工' 
                                       WHEN  2 THEN '临时工'
									   WHEN  3 THEN '劳务工'
                                       ELSE '' END) F_Kind,
        F_RFIDCode,
        F_Group,
        F_Indate,
        F_Outdate,
        F_Cert,
        F_Nation,
        F_Record,
        F_Origin,
        AM_Base_User.F_DepartmentId,
        F_Picture1,
        F_Picture2,
        F_Picture3,
        F_Picture4,
        F_Picture5
FROM    AM_Base_User
        left JOIN AM_Base_Department ON AM_Base_Department.F_DepartmentId = AM_Base_User.F_DepartmentId
        LEFT JOIN Sys_Role ON Sys_Role.R_Code = AM_Base_User.F_Account
  WHERE  AM_Base_User.F_DeleteMark = '0' AND (AM_Base_User.F_Account LIKE '%{0}%' OR AM_Base_User.F_RealName LIKE '%{0}%')", key);
                }

                SqlHelper db = new SqlHelper();
                var rows = db.ExecuteDataTable(sql); //db.ExecuteObjects<SysUser>(sql);

                return rows;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 传入姓名或学号时获取全部数据列表
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public DataTable load_RealName(string key)
        {
            try
            {
              
                 string   sql = string.Format(@"SELECT F_UserId, F_Account ,
        F_RealName ,
        '******' F_Password ,
        CASE F_Gender WHEN 1 THEN '男' ELSE '女' END StrGender ,
         AM_Base_Department.F_FullName  D_Code ,
         AM_Base_User.F_EnCode,
        Sys_Role.R_Name R_Code ,
        F_Mobile ,
        AM_Base_User.F_Email,
        F_OICQ ,
        F_WeChat ,
        U_Address ,
        AM_Base_User.F_Description ,
        AM_Base_User.F_EnabledMark ,
        AM_Base_User.F_CreateUserName ,
        AM_Base_User.F_CreateDate ,
        AM_Base_User.F_ModifyUserName ,
        AM_Base_User.F_ModifyDate ,
		(CASE F_Kind 
                                       WHEN  1 THEN '正式工' 
                                       WHEN  2 THEN '临时工'
									   WHEN  3 THEN '劳务工'
                                       ELSE '' END) F_Kind,
        F_RFIDCode,
        F_Group,
        F_Indate,
        F_Outdate,
        F_Cert,
        F_Nation,
        F_Record,
        F_Origin,
        AM_Base_User.F_DepartmentId,
        F_Picture1,
        F_Picture2,
        F_Picture3,
        F_Picture4,
        F_Picture5
FROM    AM_Base_User
        left JOIN AM_Base_Department ON AM_Base_Department.F_DepartmentId = AM_Base_User.F_DepartmentId
        LEFT JOIN Sys_Role ON Sys_Role.R_Code = AM_Base_User.F_Account
  WHERE AM_Base_User.F_EnCode LIKE '%{0}%' OR AM_Base_User.F_RealName LIKE '%{0}%'", key);

                SqlHelper db = new SqlHelper();
                var rows = db.ExecuteDataTable(sql); //db.ExecuteObjects<SysUser>(sql);

                return rows;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 通过账户名获取数据列表
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public SysUser getDetail(string key)
        {
            //( '[' + AM_Base_User.[R_Code] + ']' + Sys_Role.R_Name )
            try
            {
                string sql = string.Format(@"SELECT F_Account
      ,F_RealName
      ,F_Password
      ,F_Gender
      ,AM_Base_Department.F_FullName  D_Code 
,F_TeamCode
,F_TeamName
      ,Sys_Role.R_Name R_Code
      ,F_Mobile
      ,AM_Base_User.F_Email
      ,F_OICQ
      ,F_WeChat
      ,[U_Address]
      ,AM_Base_User.F_Description ,
        AM_Base_User.F_EnabledMark ,
        AM_Base_User.F_CreateUserName ,
        AM_Base_User.F_CreateDate ,
        AM_Base_User.F_ModifyUserName ,
        AM_Base_User.F_ModifyDate ,
		F_Kind,
        F_RFIDCode,
       AM_Base_User.R_CSCode,
        F_Group,
        F_Indate,
        F_Outdate,
        F_Cert,
        F_Nation,
        F_Record,
        F_Origin,
        AM_Base_User.F_EnCode,
        F_Picture1,
        F_Picture2,
        F_Picture3,
        F_Picture4,
        F_Picture5,
        AM_Base_User.F_DepartmentId
  FROM AM_Base_User
        left JOIN AM_Base_Department ON AM_Base_Department.F_DepartmentId = AM_Base_User.F_DepartmentId
        LEFT JOIN Sys_Role ON Sys_Role.R_Code = AM_Base_User.F_Account
WHERE F_Account='{0}'", key);


                SqlHelper db = new SqlHelper();
                var rows = db.ExecuteObject<SysUser>(sql);

                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 通过部门id获取数据列表
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public SysUser getDetail_F_DepartmentId(string F_DepartmentId)
        {
            try
            {
                string sql = string.Format(@"SELECT F_Account
      ,F_RealName
      ,F_Password
      ,F_Gender
      ,AM_Base_Department.F_FullName  D_Code 
      ,( '[' + AM_Base_User.[R_Code] + ']' + Sys_Role.R_Name ) R_Code
      ,F_Mobile
      ,AM_Base_User.F_Email
      ,F_OICQ
      ,F_WeChat
      ,[U_Address]
      ,AM_Base_User.F_Description ,
        AM_Base_User.F_EnabledMark ,
        AM_Base_User.F_CreateUserName ,
        AM_Base_User.F_CreateDate ,
        AM_Base_User.F_ModifyUserName ,
        AM_Base_User.F_ModifyDate ,
		F_Kind,
        F_RFIDCode,
        F_Group,
        F_Indate,
        F_Outdate,
        F_Cert,
        F_Nation,
        F_Record,
        F_Origin,
        AM_Base_User.F_EnCode,
        AM_Base_User.F_DepartmentId,
        F_Picture1,
        F_Picture2,
        F_Picture3,
        F_Picture4,
        F_Picture5
  FROM AM_Base_User
        LEFT JOIN AM_Base_Department ON AM_Base_Department.F_DepartmentId = AM_Base_User.F_DepartmentId
        LEFT JOIN Sys_Role ON Sys_Role.R_Code = AM_Base_User.F_Account
  WHERE AM_Base_User.F_DepartmentId='{0}'", F_DepartmentId);
                SqlHelper db = new SqlHelper();
                var rows = db.ExecuteObject<SysUser>(sql);

                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SysUser> getDetail_F_EnCode(string key)
        {
            try
            {
                string sql = string.Format(@"SELECT F_Account
      ,F_RealName
      ,F_Password
      ,F_Gender
      ,AM_Base_Department.F_FullName  D_Code 
      ,( '[' + AM_Base_User.[R_Code] + ']' + Sys_Role.R_Name ) R_Code
      ,F_Mobile
      ,AM_Base_User.F_Email
      ,F_OICQ
      ,F_WeChat
      ,[U_Address]
      ,AM_Base_User.F_Description ,
        AM_Base_User.F_EnabledMark ,
        AM_Base_User.F_CreateUserName ,
        AM_Base_User.F_CreateDate ,
        AM_Base_User.F_ModifyUserName ,
        AM_Base_User.F_ModifyDate ,
		F_Kind,
        F_RFIDCode,
        F_Group,
        F_Indate,
        F_Outdate,
        F_Cert,
        F_Nation,
        F_Record,
        F_Origin,
        AM_Base_User.F_EnCode,
        AM_Base_User.F_DepartmentId,
        F_Picture1,
        F_Picture2,
        F_Picture3,
        F_Picture4,
        F_Picture5
  FROM AM_Base_User
        left JOIN AM_Base_Department ON AM_Base_Department.F_DepartmentId = AM_Base_User.F_DepartmentId
        LEFT JOIN Sys_Role ON Sys_Role.R_Code = AM_Base_User.F_Account
WHERE F_Account='{0}'", key);


                SqlHelper db = new SqlHelper();
                var rows = db.ExecuteObjects<SysUser>(sql);
               // db.ExecuteObjects<MesDeviceEntity>(strSql.ToString(), paramList.ToArray());
                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Add(SysUser user)
        {
            try
            {
                string sql = @"INSERT INTO [Sys_User]
           ([U_Code]
           ,[U_Name]
           ,[U_Pwd]
           ,[U_Sex]
           ,[D_Code]
           ,[R_Code]
           ,[U_Phone]
           ,[U_Email]
           ,[U_QQ]
           ,[U_WeChat]
           ,[U_Address]
           ,[U_Remark]
           ,[U_Active]
           ,[U_CreateBy]
           ,[U_CreateDate]
           ,[U_UpdateBy]
           ,[U_UpdateDate])
     VALUES
           (@U_Code
           ,@U_Name
           ,@U_Pwd
           ,@U_Sex
           ,@D_Code
           ,@R_Code
           ,@U_Phone
           ,@U_Email
           ,@U_QQ
           ,@U_WeChat
           ,@U_Address
           ,@U_Remark
           ,@U_Active
           ,@U_CreateBy
           ,@U_CreateDate
           ,@U_UpdateBy
           ,@U_UpdateDate)
";

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@U_Code", user.F_Account));
                parameters.Add(new SqlParameter("@U_Name", user.F_RealName));
                parameters.Add(new SqlParameter("@U_Pwd", user.F_Password));
                parameters.Add(new SqlParameter("@U_Sex", user.F_Gender));
                parameters.Add(new SqlParameter("@D_Code", user.D_Code));
                parameters.Add(new SqlParameter("@R_CSCode", user.R_CSCode));
                parameters.Add(new SqlParameter("@U_Active", user.F_EnabledMark));

                if (!string.IsNullOrEmpty(user.F_Mobile))
                {
                    parameters.Add(new SqlParameter("@U_Phone", user.F_Mobile));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_Phone", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(user.F_Email))
                {
                    parameters.Add(new SqlParameter("@U_Email", user.F_Email));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_Email", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(user.F_OICQ))
                {
                    parameters.Add(new SqlParameter("@U_QQ", user.F_OICQ));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_QQ", DBNull.Value));
                }


                if (!string.IsNullOrEmpty(user.F_WeChat))
                {
                    parameters.Add(new SqlParameter("@U_WeChat", user.F_WeChat));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_WeChat", DBNull.Value));
                }


                if (!string.IsNullOrEmpty(user.U_Address))
                {
                    parameters.Add(new SqlParameter("@U_Address", user.U_Address));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_Address", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(user.F_Description))
                {
                    parameters.Add(new SqlParameter("@U_Remark", user.F_Description));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_Remark", DBNull.Value));
                }

               

                if (!string.IsNullOrEmpty(user.F_CreateUserName))
                {
                    parameters.Add(new SqlParameter("@U_CreateBy", user.F_CreateUserName));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_CreateBy", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(user.F_ModifyUserName))
                {
                    parameters.Add(new SqlParameter("@U_UpdateBy", user.F_ModifyUserName));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_UpdateBy", DBNull.Value));
                }


                if (user.F_CreateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@U_CreateDate", user.F_CreateDate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_CreateDate", DBNull.Value));
                }

                if (!user.F_ModifyDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@U_UpdateDate", user.F_ModifyDate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_UpdateDate", DBNull.Value));
                }


                SqlHelper db = new SqlHelper();
                var row = db.ExecuteNonQuery(sql, parameters.ToArray());

                return row;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Edit(SysUser user)
        {
            try
            {
                string sql = @"UPDATE [AM_Base_User]
   SET 
       [F_RealName] = @U_Name
      ,[F_Gender] = @U_Sex
      ,[D_Code] = @D_Code
,[F_TeamCode] = @F_TeamCode
,[F_TeamName] = @F_TeamName
      ,[R_CSCode] = @R_CSCode
      ,[F_Mobile] = @U_Phone
      ,[F_Email] = @U_Email
      ,[F_OICQ] = @U_QQ
      ,[F_WeChat] = @U_WeChat
      ,[U_Address] = @U_Address
      ,[F_Description] = @U_Remark
      ,[F_EnabledMark] = @U_Active
      ,[F_ModifyUserName] = @U_UpdateBy
      ,[F_ModifyDate] = @U_UpdateDate
      ,[F_Kind] = @F_Kind
      ,[F_RFIDCode] = @F_RFIDCode
      ,[F_Group] = @F_Group
      ,[F_Indate] = @F_Indate
      ,[F_Outdate] = @F_Outdate
      ,[F_Cert] = @F_Cert
      ,[F_Nation] = @F_Nation
      ,[F_Record] = @F_Record
      ,[F_Origin] = @F_Origin
      ,[F_Picture1] = @F_Picture1
,F_DepartmentId =@F_DepartmentId
,[F_Picture2] = @F_Picture2
,[F_Picture3] = @F_Picture3
,[F_Picture4] = @F_Picture4
,[F_Picture5] = @F_Picture5
 WHERE [F_Account] = @U_Code";

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@U_Code", user.F_Account));
                parameters.Add(new SqlParameter("@U_Name", user.F_RealName));
                //parameters.Add(new SqlParameter("@U_Pwd", user.F_Password));
                parameters.Add(new SqlParameter("@U_Sex", user.F_Gender));
                parameters.Add(new SqlParameter("@D_Code", user.D_Code));
                parameters.Add(new SqlParameter("@F_TeamCode", user.F_TeamCode));
                parameters.Add(new SqlParameter("@F_TeamName", user.F_TeamName));
                parameters.Add(new SqlParameter("@R_CSCode", user.R_CSCode));
                parameters.Add(new SqlParameter("@U_Active", user.F_EnabledMark));

                parameters.Add(new SqlParameter("@F_Kind", user.F_Kind));
                parameters.Add(new SqlParameter("@F_RFIDCode", user.F_RFIDCode));
                parameters.Add(new SqlParameter("@F_Group", user.F_Group));
                //parameters.Add(new SqlParameter("@F_Indate", user.F_Indate));
                //parameters.Add(new SqlParameter("@F_Outdate", user.F_Outdate));
                parameters.Add(new SqlParameter("@F_Cert", user.F_Cert));
                parameters.Add(new SqlParameter("@F_Nation", user.F_Nation));
                parameters.Add(new SqlParameter("@F_Record", user.F_Record));
                parameters.Add(new SqlParameter("@F_Origin", user.F_Origin));
                parameters.Add(new SqlParameter("@F_Picture1", user.F_Picture1));
                parameters.Add(new SqlParameter("@F_DepartmentId", user.F_DepartmentId));
                parameters.Add(new SqlParameter("@F_Picture2", user.F_Picture2));
                parameters.Add(new SqlParameter("@F_Picture3", user.F_Picture3));
                parameters.Add(new SqlParameter("@F_Picture4", user.F_Picture4));
                parameters.Add(new SqlParameter("@F_Picture5", user.F_Picture5));

                if (!user.F_Indate.HasValue)
                {
                    parameters.Add(new SqlParameter("@F_Indate", user.F_Indate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@F_Indate", DBNull.Value));
                }

                if (!user.F_Outdate.HasValue)
                {
                    parameters.Add(new SqlParameter("@F_Outdate", user.F_Outdate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@F_Outdate", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(user.F_Mobile))
                {
                    parameters.Add(new SqlParameter("@U_Phone", user.F_Mobile));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_Phone", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(user.F_Email))
                {
                    parameters.Add(new SqlParameter("@U_Email", user.F_Email));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_Email", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(user.F_OICQ))
                {
                    parameters.Add(new SqlParameter("@U_QQ", user.F_OICQ));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_QQ", DBNull.Value));
                }


                if (!string.IsNullOrEmpty(user.F_WeChat))
                {
                    parameters.Add(new SqlParameter("@U_WeChat", user.F_WeChat));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_WeChat", DBNull.Value));
                }


                if (!string.IsNullOrEmpty(user.U_Address))
                {
                    parameters.Add(new SqlParameter("@U_Address", user.U_Address));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_Address", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(user.F_Description))
                {
                    parameters.Add(new SqlParameter("@U_Remark", user.F_Description));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_Remark", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(user.F_ModifyUserName))
                {
                    parameters.Add(new SqlParameter("@U_UpdateBy", user.F_ModifyUserName));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_UpdateBy", DBNull.Value));
                }


                if (!user.F_ModifyDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@U_UpdateDate", user.F_ModifyDate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_UpdateDate", DBNull.Value));
                }

                SqlHelper db = new SqlHelper();
                var row = db.ExecuteNonQuery(sql, parameters.ToArray());

                return row;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Delete(string U_Code)
        {
            try
            {
                string sql = string.Format(@"DELETE FROM [Sys_User] WHERE [U_Code]='{0}'", U_Code);
                SqlHelper db = new SqlHelper();
                var row = db.ExecuteNonQuery(sql);

                return row;
            }
            catch (Exception)
            {
                throw;
            }
        }  

    }
}
