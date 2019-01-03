using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DataAccess.SqlServer;
using Model;

namespace Business
{
    public class SysRoleBLL
    {
        public List<SysRole> loadData(string key)
        {
            try
            {
                string sql = "";
                if (string.IsNullOrEmpty(key))
                {
                    sql = @"SELECT [R_Code]
      ,[R_Name]
      ,[R_Remark]
      ,[R_CreateBy]
      ,[R_CreateDate]
      ,[R_UpdateBy]
      ,[R_UpdateDate]
  FROM [Sys_Role]";
                }
                else
                {
                    sql = string.Format(@"SELECT [R_Code]
      ,[R_Name]
      ,[R_Remark]
      ,[R_CreateBy]
      ,[R_CreateDate]
      ,[R_UpdateBy]
      ,[R_UpdateDate]
  FROM [Sys_Role]
  WHERE R_Code LIKE '%{0}%' OR R_Name LIKE '%{0}%'", key);
                }

                SqlHelper db = new SqlHelper();
                var rows = db.ExecuteObjects<SysRole>(sql);

                return rows;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public SysRole getDetail(string key)
        {
            try
            {
                string sql = string.Format(@"SELECT [R_Code]
      ,[R_Name]
      ,[R_Remark]
      ,[R_CreateBy]
      ,[R_CreateDate]
      ,[R_UpdateBy]
      ,[R_UpdateDate]
  FROM [Sys_Role]
WHERE R_Code='{0}'", key);


                SqlHelper db = new SqlHelper();
                var rows = db.ExecuteObject<SysRole>(sql);

                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Exists(string R_Code)
        {
            try
            {
                string sql = @"SELECT COUNT(*) FROM Sys_Role WHERE R_Code='{0}'";
                SqlHelper db = new SqlHelper();
                var row = db.ExecuteScalar<int>(string.Format(sql, R_Code));

                return row > 0 ? true : false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Add(SysRole role)
        {
            try
            {
                string sql = @"INSERT INTO [Sys_Role]
           ([R_Code]
           ,[R_Name]
           ,[R_Remark]
           ,[R_CreateBy]
           ,[R_CreateDate]
           ,[R_UpdateBy]
           ,[R_UpdateDate])
     VALUES
           (@R_Code
           ,@R_Name
           ,@R_Remark
           ,@R_CreateBy
           ,@R_CreateDate
           ,@R_UpdateBy
           ,@R_UpdateDate)";

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@R_Code", role.R_Code));
                parameters.Add(new SqlParameter("@R_Name", role.R_Name));

                if (!string.IsNullOrEmpty(role.R_Remark))
                {
                    parameters.Add(new SqlParameter("@R_Remark", role.R_Remark));
                }
                else
                {
                    parameters.Add(new SqlParameter("@R_Remark", DBNull.Value));
                }



                if (!string.IsNullOrEmpty(role.R_CreateBy))
                {
                    parameters.Add(new SqlParameter("@R_CreateBy", role.R_CreateBy));
                }
                else
                {
                    parameters.Add(new SqlParameter("@R_CreateBy", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(role.R_UpdateBy))
                {
                    parameters.Add(new SqlParameter("@R_UpdateBy", role.R_UpdateBy));
                }
                else
                {
                    parameters.Add(new SqlParameter("@R_UpdateBy", DBNull.Value));
                }


                if (role.R_CreateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@R_CreateDate", role.R_CreateDate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@R_CreateDate", DBNull.Value));
                }

                if (!role.R_UpdateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@R_UpdateDate", role.R_UpdateDate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@R_UpdateDate", DBNull.Value));
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

        public int Edit(SysRole role)
        {
            try
            {
                string sql = @"UPDATE [Sys_Role]
   SET 
       [R_Name] = @R_Name
      ,[R_Remark] = @R_Remark
      ,[R_CreateBy] = @R_CreateBy
      ,[R_CreateDate] = @R_CreateDate
      ,[R_UpdateBy] = @R_UpdateBy
      ,[R_UpdateDate] = @R_UpdateDate
 WHERE [R_Code] = @R_Code";

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@R_Code", role.R_Code));
                parameters.Add(new SqlParameter("@R_Name", role.R_Name));

                if (!string.IsNullOrEmpty(role.R_Remark))
                {
                    parameters.Add(new SqlParameter("@R_Remark", role.R_Remark));
                }
                else
                {
                    parameters.Add(new SqlParameter("@R_Remark", DBNull.Value));
                }



                if (!string.IsNullOrEmpty(role.R_CreateBy))
                {
                    parameters.Add(new SqlParameter("@R_CreateBy", role.R_CreateBy));
                }
                else
                {
                    parameters.Add(new SqlParameter("@R_CreateBy", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(role.R_UpdateBy))
                {
                    parameters.Add(new SqlParameter("@R_UpdateBy", role.R_UpdateBy));
                }
                else
                {
                    parameters.Add(new SqlParameter("@R_UpdateBy", DBNull.Value));
                }


                if (role.R_CreateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@R_CreateDate", role.R_CreateDate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@R_CreateDate", DBNull.Value));
                }

                if (role.R_UpdateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@R_UpdateDate", role.R_UpdateDate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@R_UpdateDate", DBNull.Value));
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

        public int Delete(string C_Code)
        {
            try
            {
                string sql = string.Format(@"DELETE FROM [Sys_Role] WHERE [R_Code]='{0}'", C_Code);
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