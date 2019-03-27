using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DataAccess.SqlServer;
using Model;

namespace Business
{
    public class SysDepartmentBLL
    {
        public List<SysDepartment> loadData(string key)
        {
            try
            {
                string sql = "";
                if (string.IsNullOrEmpty(key))
                {
                    sql = @"SELECT [D_Code],
    [D_Name],
    ('[' + Sys_Department.[C_Code] + ']' + Sys_Company.C_Name) C_Code,
    [D_Remark],
    [D_CreateBy],
    [D_CreateDate],
    [D_UpdateBy],
    [D_UpdateDate]
FROM [Sys_Department]
    LEFT JOIN Sys_Company
        ON Sys_Company.C_Code = Sys_Department.C_Code";
                }
                else
                {
                    sql = string.Format(@"SELECT [D_Code],
    [D_Name],
    ('[' + Sys_Department.[C_Code] + ']' + Sys_Company.C_Name) C_Code,
    [D_Remark],
    [D_CreateBy],
    [D_CreateDate],
    [D_UpdateBy],
    [D_UpdateDate]
FROM [Sys_Department]
    LEFT JOIN Sys_Company
        ON Sys_Company.C_Code = Sys_Department.C_Code
  WHERE Sys_Department.D_Code LIKE '%{0}%' OR Sys_Department.D_Name LIKE '%{0}%'", key);
                }

                SqlHelper db = new SqlHelper();
                var rows = db.ExecuteObjects<SysDepartment>(sql);

                return rows;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SysDepartment> getDepartment()
        {
            try
            {
                string sql = "";
                    sql = @"SELECT 
    [D_Name]
FROM [Sys_Department]";

                SqlHelper db = new SqlHelper();
                var rows = db.ExecuteObjects<SysDepartment>(sql);

                return rows;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public SysDepartment getDetail(string key)
        {
            try
            {
                string sql = string.Format(@"SELECT [D_Code]
      ,[D_Name]
      ,[C_Code]
      ,[D_Remark]
      ,[D_CreateBy]
      ,[D_CreateDate]
      ,[D_UpdateBy]
      ,[D_UpdateDate]
  FROM [Sys_Department]
WHERE D_Code='{0}'", key);


                SqlHelper db = new SqlHelper();
                var rows = db.ExecuteObject<SysDepartment>(sql);

                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Exists(string D_Code)
        {
            try
            {
                string sql = @"SELECT COUNT(*) FROM Sys_Department WHERE D_Code='{0}'";
                SqlHelper db = new SqlHelper();
                var row = db.ExecuteScalar<int>(string.Format(sql, D_Code));

                return row > 0 ? true : false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Add(SysDepartment department)
        {
            try
            {
                string sql = @"INSERT INTO [Sys_Department]
           ([D_Code]
           ,[D_Name]
           ,[C_Code]
           ,[D_Remark]
           ,[D_CreateBy]
           ,[D_CreateDate]
           ,[D_UpdateBy]
           ,[D_UpdateDate])
     VALUES
           (@D_Code
           ,@D_Name
           ,@C_Code
           ,@D_Remark
           ,@D_CreateBy
           ,@D_CreateDate
           ,@D_UpdateBy
           ,@D_UpdateDate)";

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@D_Code", department.D_Code));
                parameters.Add(new SqlParameter("@D_Name", department.D_Name));
                parameters.Add(new SqlParameter("@C_Code", department.C_Code));

                if (!string.IsNullOrEmpty(department.D_Remark))
                {
                    parameters.Add(new SqlParameter("@D_Remark", department.D_Remark));
                }
                else
                {
                    parameters.Add(new SqlParameter("@D_Remark", DBNull.Value));
                }



                if (!string.IsNullOrEmpty(department.D_CreateBy))
                {
                    parameters.Add(new SqlParameter("@D_CreateBy", department.D_CreateBy));
                }
                else
                {
                    parameters.Add(new SqlParameter("@D_CreateBy", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(department.D_UpdateBy))
                {
                    parameters.Add(new SqlParameter("@D_UpdateBy", department.D_UpdateBy));
                }
                else
                {
                    parameters.Add(new SqlParameter("@D_UpdateBy", DBNull.Value));
                }


                if (department.D_CreateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@D_CreateDate", department.D_CreateDate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@D_CreateDate", DBNull.Value));
                }

                if (!department.D_UpdateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@D_UpdateDate", department.D_UpdateDate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@D_UpdateDate", DBNull.Value));
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

        public int Edit(SysDepartment department)
        {
            try
            {
                string sql = @"UPDATE [Sys_Department]
   SET 
       [D_Name] = @D_Name
      ,[C_Code] = @C_Code
      ,[D_Remark] = @D_Remark
      ,[D_CreateBy] = @D_CreateBy
      ,[D_CreateDate] = @D_CreateDate
      ,[D_UpdateBy] = @D_UpdateBy
      ,[D_UpdateDate] = @D_UpdateDate
 WHERE [D_Code] = @D_Code";

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@D_Code", department.D_Code));
                parameters.Add(new SqlParameter("@D_Name", department.D_Name));
                parameters.Add(new SqlParameter("@C_Code", department.C_Code));

                if (!string.IsNullOrEmpty(department.D_Remark))
                {
                    parameters.Add(new SqlParameter("@D_Remark", department.D_Remark));
                }
                else
                {
                    parameters.Add(new SqlParameter("@D_Remark", DBNull.Value));
                }



                if (!string.IsNullOrEmpty(department.D_CreateBy))
                {
                    parameters.Add(new SqlParameter("@D_CreateBy", department.D_CreateBy));
                }
                else
                {
                    parameters.Add(new SqlParameter("@D_CreateBy", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(department.D_UpdateBy))
                {
                    parameters.Add(new SqlParameter("@D_UpdateBy", department.D_UpdateBy));
                }
                else
                {
                    parameters.Add(new SqlParameter("@D_UpdateBy", DBNull.Value));
                }


                if (department.D_CreateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@D_CreateDate", department.D_CreateDate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@D_CreateDate", DBNull.Value));
                }

                if (department.D_UpdateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@D_UpdateDate", department.D_UpdateDate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@D_UpdateDate", DBNull.Value));
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

        public int Delete(string D_Code)
        {
            try
            {
                string sql = string.Format(@"DELETE FROM [Sys_Department] WHERE [D_Code]='{0}'", D_Code);
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