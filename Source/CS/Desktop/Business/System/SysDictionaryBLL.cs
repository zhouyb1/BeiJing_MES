using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DataAccess.SqlServer;
using Model;

namespace Business
{
    public class SysDictionaryBLL
    {
        public List<SysDictionary> loadData(string type,string key)
        {
            try
            {
                StringBuilder sql=new StringBuilder(@"SELECT [D_Code]
      ,[D_Name]
      ,[D_Type]
      ,[D_Seq]
      ,[D_CreateBy]
      ,[D_CreateDate]
      ,[D_UpdateBy]
      ,[D_UpdateDate]
  FROM [Sys_Dictionary]
WHERE 1=1 ");

                if (!string.IsNullOrEmpty(type))
                {
                    sql.Append(string.Format(" AND [D_Type]='{0}'", type));
                }

                if (!string.IsNullOrEmpty(key))
                {
                    sql.Append(string.Format(" AND ([D_Code] LIKE '%{0}%' OR [D_Name] LIKE '%{0}%')", key));
                }

                sql.Append(" ORDER BY [D_Type],[D_Seq]");

                SqlHelper db = new SqlHelper();
                var rows = db.ExecuteObjects<SysDictionary>(sql.ToString());

                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SysDictionary getDetail(string key)
        {
            try
            {
                string sql = string.Format(@"SELECT [D_Code]
      ,[D_Name]
      ,[D_Type]
      ,[D_Seq]
      ,[D_CreateBy]
      ,[D_CreateDate]
      ,[D_UpdateBy]
      ,[D_UpdateDate]
  FROM [Sys_Dictionary]
WHERE  [D_Code]='{0}'", key);


                SqlHelper db = new SqlHelper();
                var rows = db.ExecuteObject<SysDictionary>(sql);

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
                string sql = @"SELECT COUNT(*) FROM Sys_Dictionary WHERE D_Code='{0}'";
                SqlHelper db = new SqlHelper();
                var row = db.ExecuteScalar<int>(string.Format(sql, D_Code));

                return row > 0 ? true : false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Add(SysDictionary Dictionary)
        {
            try
            {
                string sql = @"INSERT INTO [Sys_Dictionary]
           ([D_Code]
           ,[D_Name]
           ,[D_Type]
           ,[D_Seq]
           ,[D_CreateBy]
           ,[D_CreateDate]
           ,[D_UpdateBy]
           ,[D_UpdateDate])
     VALUES
           (@D_Code
           ,@D_Name
           ,@D_Type
           ,@D_Seq
           ,@D_CreateBy
           ,@D_CreateDate
           ,@D_UpdateBy
           ,@D_UpdateDate)";

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@D_Code", Dictionary.D_Code));
                parameters.Add(new SqlParameter("@D_Name", Dictionary.D_Name));
                parameters.Add(new SqlParameter("@D_Type", Dictionary.D_Type));
                parameters.Add(new SqlParameter("@D_Seq", Dictionary.D_Seq));



                if (!string.IsNullOrEmpty(Dictionary.D_CreateBy))
                {
                    parameters.Add(new SqlParameter("@D_CreateBy", Dictionary.D_CreateBy));
                }
                else
                {
                    parameters.Add(new SqlParameter("@D_CreateBy", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(Dictionary.D_UpdateBy))
                {
                    parameters.Add(new SqlParameter("@D_UpdateBy", Dictionary.D_UpdateBy));
                }
                else
                {
                    parameters.Add(new SqlParameter("@D_UpdateBy", DBNull.Value));
                }


                if (Dictionary.D_CreateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@D_CreateDate", Dictionary.D_CreateDate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@D_CreateDate", DBNull.Value));
                }

                if (!Dictionary.D_UpdateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@D_UpdateDate", Dictionary.D_UpdateDate));
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

        public int Edit(SysDictionary Dictionary)
        {
            try
            {
                string sql = @"UPDATE [Sys_Dictionary]
   SET 
       [D_Name] = @D_Name
      ,[D_Type] = @D_Type
      ,[D_Seq] = @D_Seq
      ,[D_CreateBy] = @D_CreateBy
      ,[D_CreateDate] = @D_CreateDate
      ,[D_UpdateBy] = @D_UpdateBy
      ,[D_UpdateDate] = @D_UpdateDate
 WHERE [D_Code] = @D_Code";

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@D_Code", Dictionary.D_Code));
                parameters.Add(new SqlParameter("@D_Name", Dictionary.D_Name));
                parameters.Add(new SqlParameter("@D_Type", Dictionary.D_Type));
                parameters.Add(new SqlParameter("@D_Seq", Dictionary.D_Seq));



                if (!string.IsNullOrEmpty(Dictionary.D_CreateBy))
                {
                    parameters.Add(new SqlParameter("@D_CreateBy", Dictionary.D_CreateBy));
                }
                else
                {
                    parameters.Add(new SqlParameter("@D_CreateBy", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(Dictionary.D_UpdateBy))
                {
                    parameters.Add(new SqlParameter("@D_UpdateBy", Dictionary.D_UpdateBy));
                }
                else
                {
                    parameters.Add(new SqlParameter("@D_UpdateBy", DBNull.Value));
                }


                if (Dictionary.D_CreateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@D_CreateDate", Dictionary.D_CreateDate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@D_CreateDate", DBNull.Value));
                }

                if (!Dictionary.D_UpdateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@D_UpdateDate", Dictionary.D_UpdateDate));
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
                string sql = string.Format(@"DELETE FROM [Sys_Dictionary] WHERE [D_Code]='{0}'", D_Code);
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