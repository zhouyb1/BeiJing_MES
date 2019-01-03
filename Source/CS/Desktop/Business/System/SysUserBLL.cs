using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.SqlServer;
using Model;

namespace Business
{
    public class SysUserBLL
    {
        /// <summary
        /// 用户登录
        /// </summary>
        /// <param name="U_Code"></param>
        /// <param name="U_Pwd"></param>
        /// <returns></returns>
        public SysUser Login(string U_Code, string U_Pwd)
        {
            try
            {
                SqlHelper db = new SqlHelper();
                string sql = @"SELECT [U_Code],
    [U_Name],
    [U_Pwd],
    [U_Sex],
    [U_Code],
    [R_Code],
    [U_Phone],
    [U_Email],
    [U_QQ],
    [U_WeChat],
    [U_Address],
    [U_Remark],
    [U_Active],
    [U_CreateBy],
    [U_CreateDate],
    [U_UpdateBy],
    [U_UpdateDate]
FROM [Sys_User]
WHERE [U_Code] = '{0}'
      AND [U_Pwd] = '{1}';";

                SysUser user = db.ExecuteObject<SysUser>(string.Format(sql, U_Code, U_Pwd));
                
                return user;
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
                string sql = @"UPDATE [Sys_User]
   SET [U_Pwd] = '{0}'
 WHERE [U_Code]='{1}'";

                SqlHelper db = new SqlHelper();
                return db.ExecuteNonQuery(string.Format(sql, user.U_Pwd, user.U_Code));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SysUser> loadData(string key)
        {
            try
            {
                string sql = "";
                if (string.IsNullOrEmpty(key))
                {
                    sql = @"SELECT [U_Code]
      ,[U_Name]
      ,[U_Pwd]
      ,[U_Sex]
      ,('['+Sys_User.[D_Code]+']'+Sys_Department.D_Name) D_Code
      ,('['+Sys_User.[R_Code]+']'+Sys_Role.R_Name) R_Code
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
      ,[U_UpdateDate]
  FROM [Sys_User]
  LEFT JOIN Sys_Department ON Sys_Department.D_Code = Sys_User.D_Code
  LEFT JOIN Sys_Role ON Sys_Role.R_Code = Sys_User.R_Code";
                }
                else
                {
                    sql = string.Format(@"SELECT [U_Code]
      ,[U_Name]
      ,[U_Pwd]
      ,[U_Sex]
      ,('['+Sys_User.[D_Code]+']'+Sys_Department.D_Name) D_Code
      ,('['+Sys_User.[R_Code]+']'+Sys_Role.R_Name) R_Code
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
      ,[U_UpdateDate]
  FROM [Sys_User]
  LEFT JOIN Sys_Department ON Sys_Department.D_Code = Sys_User.D_Code
  LEFT JOIN Sys_Role ON Sys_Role.R_Code = Sys_User.R_Code
  WHERE Sys_User.U_Code LIKE '%{0}%' OR Sys_User.U_Name LIKE '%{0}%'", key);
                }

                SqlHelper db = new SqlHelper();
                var rows = db.ExecuteObjects<SysUser>(sql);

                return rows;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public SysUser getDetail(string key)
        {
            try
            {
                string sql = string.Format(@"SELECT [U_Code]
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
      ,[U_UpdateDate]
  FROM [Sys_User]
WHERE U_Code='{0}'", key);


                SqlHelper db = new SqlHelper();
                var rows = db.ExecuteObject<SysUser>(sql);

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
                parameters.Add(new SqlParameter("@U_Code", user.U_Code));
                parameters.Add(new SqlParameter("@U_Name", user.U_Name));
                parameters.Add(new SqlParameter("@U_Pwd", user.U_Pwd));
                parameters.Add(new SqlParameter("@U_Sex", user.U_Sex));
                parameters.Add(new SqlParameter("@D_Code", user.D_Code));
                parameters.Add(new SqlParameter("@R_Code", user.R_Code));
                parameters.Add(new SqlParameter("@U_Active", user.U_Active));

                if (!string.IsNullOrEmpty(user.U_Phone))
                {
                    parameters.Add(new SqlParameter("@U_Phone", user.U_Phone));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_Phone", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(user.U_Email))
                {
                    parameters.Add(new SqlParameter("@U_Email", user.U_Email));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_Email", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(user.U_QQ))
                {
                    parameters.Add(new SqlParameter("@U_QQ", user.U_QQ));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_QQ", DBNull.Value));
                }


                if (!string.IsNullOrEmpty(user.U_WeChat))
                {
                    parameters.Add(new SqlParameter("@U_WeChat", user.U_WeChat));
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

                if (!string.IsNullOrEmpty(user.U_Remark))
                {
                    parameters.Add(new SqlParameter("@U_Remark", user.U_Remark));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_Remark", DBNull.Value));
                }

               

                if (!string.IsNullOrEmpty(user.U_CreateBy))
                {
                    parameters.Add(new SqlParameter("@U_CreateBy", user.U_CreateBy));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_CreateBy", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(user.U_UpdateBy))
                {
                    parameters.Add(new SqlParameter("@U_UpdateBy", user.U_UpdateBy));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_UpdateBy", DBNull.Value));
                }


                if (user.U_CreateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@U_CreateDate", user.U_CreateDate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_CreateDate", DBNull.Value));
                }

                if (!user.U_UpdateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@U_UpdateDate", user.U_UpdateDate));
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
                string sql = @"UPDATE [Sys_User]
   SET 
       [U_Name] = @U_Name
      ,[U_Pwd] = @U_Pwd
      ,[U_Sex] = @U_Sex
      ,[D_Code] = @D_Code
      ,[R_Code] = @R_Code
      ,[U_Phone] = @U_Phone
      ,[U_Email] = @U_Email
      ,[U_QQ] = @U_QQ
      ,[U_WeChat] = @U_WeChat
      ,[U_Address] = @U_Address
      ,[U_Remark] = @U_Remark
      ,[U_Active] = @U_Active
      ,[U_CreateBy] = @U_CreateBy
      ,[U_CreateDate] = @U_CreateDate
      ,[U_UpdateBy] = @U_UpdateBy
      ,[U_UpdateDate] = @U_UpdateDate
 WHERE [U_Code] = @U_Code";

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@U_Code", user.U_Code));
                parameters.Add(new SqlParameter("@U_Name", user.U_Name));
                parameters.Add(new SqlParameter("@U_Pwd", user.U_Pwd));
                parameters.Add(new SqlParameter("@U_Sex", user.U_Sex));
                parameters.Add(new SqlParameter("@D_Code", user.D_Code));
                parameters.Add(new SqlParameter("@R_Code", user.R_Code));
                parameters.Add(new SqlParameter("@U_Active", user.U_Active));

                if (!string.IsNullOrEmpty(user.U_Phone))
                {
                    parameters.Add(new SqlParameter("@U_Phone", user.U_Phone));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_Phone", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(user.U_Email))
                {
                    parameters.Add(new SqlParameter("@U_Email", user.U_Email));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_Email", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(user.U_QQ))
                {
                    parameters.Add(new SqlParameter("@U_QQ", user.U_QQ));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_QQ", DBNull.Value));
                }


                if (!string.IsNullOrEmpty(user.U_WeChat))
                {
                    parameters.Add(new SqlParameter("@U_WeChat", user.U_WeChat));
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

                if (!string.IsNullOrEmpty(user.U_Remark))
                {
                    parameters.Add(new SqlParameter("@U_Remark", user.U_Remark));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_Remark", DBNull.Value));
                }



                if (!string.IsNullOrEmpty(user.U_CreateBy))
                {
                    parameters.Add(new SqlParameter("@U_CreateBy", user.U_CreateBy));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_CreateBy", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(user.U_UpdateBy))
                {
                    parameters.Add(new SqlParameter("@U_UpdateBy", user.U_UpdateBy));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_UpdateBy", DBNull.Value));
                }


                if (user.U_CreateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@U_CreateDate", user.U_CreateDate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@U_CreateDate", DBNull.Value));
                }

                if (!user.U_UpdateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@U_UpdateDate", user.U_UpdateDate));
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
