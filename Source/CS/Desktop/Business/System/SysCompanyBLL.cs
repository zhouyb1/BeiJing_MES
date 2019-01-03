using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DataAccess.SqlServer;
using Model;

namespace Business
{
    public class SysCompanyBLL
    {
        public List<SysCompany> loadData(string key)
        {
            try
            {
                string sql = "";
                if (string.IsNullOrEmpty(key))
                {
                    sql = @"SELECT [C_Code]
      ,[C_Name]
      ,[C_Phone]
      ,[C_Fax]
      ,[C_Email]
      ,[C_Address]
      ,[C_Remark]
      ,[C_CreateBy]
      ,[C_CreateDate]
      ,[C_UpdateBy]
      ,[C_UpdateDate]
  FROM [Sys_Company]";
                }
                else
                {
                    sql = string.Format(@"SELECT [C_Code]
      ,[C_Name]
      ,[C_Phone]
      ,[C_Fax]
      ,[C_Email]
      ,[C_Address]
      ,[C_Remark]
      ,[C_CreateBy]
      ,[C_CreateDate]
      ,[C_UpdateBy]
      ,[C_UpdateDate]
  FROM [Sys_Company]
  WHERE C_Code LIKE '%{0}%' OR C_Name LIKE '%{0}%'", key);
                }

                SqlHelper db = new SqlHelper();
                var rows = db.ExecuteObjects<SysCompany>(sql);

                return rows;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public SysCompany getDetail(string key)
        {
            try
            {
                string sql = string.Format(@"SELECT [C_Code]
      ,[C_Name]
      ,[C_Phone]
      ,[C_Fax]
      ,[C_Email]
      ,[C_Address]
      ,[C_Remark]
      ,[C_CreateBy]
      ,[C_CreateDate]
      ,[C_UpdateBy]
      ,[C_UpdateDate]
  FROM [Sys_Company]
WHERE C_Code='{0}'", key);
                

                SqlHelper db = new SqlHelper();
                var rows = db.ExecuteObject<SysCompany>(sql);

                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Exists(string C_Code)
        {
            try
            {
                string sql = @"SELECT COUNT(*) FROM Sys_Company WHERE C_Code='{0}'";
                SqlHelper db = new SqlHelper();
                var row = db.ExecuteScalar<int>(string.Format(sql, C_Code));

                return row > 0 ? true : false;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public int Add(SysCompany company)
        {
            try
            {
                string sql = @"INSERT INTO [Sys_Company]
           ([C_Code]
           ,[C_Name]
           ,[C_Phone]
           ,[C_Fax]
           ,[C_Email]
           ,[C_Address]
           ,[C_Remark]
           ,[C_CreateBy]
           ,[C_CreateDate]
           ,[C_UpdateBy]
           ,[C_UpdateDate])
     VALUES
           (@C_Code
           ,@C_Name
           ,@C_Phone
           ,@C_Fax
           ,@C_Email
           ,@C_Address
           ,@C_Remark
           ,@C_CreateBy
           ,@C_CreateDate
           ,@C_UpdateBy
           ,@C_UpdateDate)";

                List<SqlParameter> parameters=new List<SqlParameter>();
                parameters.Add(new SqlParameter("@C_Code", company.C_Code));
                parameters.Add(new SqlParameter("@C_Name", company.C_Name));

                if (!string.IsNullOrEmpty(company.C_Phone))
                {
                    parameters.Add(new SqlParameter("@C_Phone", company.C_Phone));
                }
                else
                {
                    parameters.Add(new SqlParameter("@C_Phone", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(company.C_Fax))
                {
                    parameters.Add(new SqlParameter("@C_Fax", company.C_Fax));
                }
                else
                {
                    parameters.Add(new SqlParameter("@C_Fax", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(company.C_Email))
                {
                    parameters.Add(new SqlParameter("@C_Email", company.C_Email));
                }
                else
                {
                    parameters.Add(new SqlParameter("@C_Email", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(company.C_Address))
                {
                    parameters.Add(new SqlParameter("@C_Address", company.C_Address));
                }
                else
                {
                    parameters.Add(new SqlParameter("@C_Address", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(company.C_Remark))
                {
                    parameters.Add(new SqlParameter("@C_Remark", company.C_Remark));
                }
                else
                {
                    parameters.Add(new SqlParameter("@C_Remark", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(company.C_CreateBy))
                {
                    parameters.Add(new SqlParameter("@C_CreateBy", company.C_CreateBy));
                }
                else
                {
                    parameters.Add(new SqlParameter("@C_CreateBy", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(company.C_UpdateBy))
                {
                    parameters.Add(new SqlParameter("@C_UpdateBy", company.C_UpdateBy));
                }
                else
                {
                    parameters.Add(new SqlParameter("@C_UpdateBy", DBNull.Value));
                }


                if (company.C_CreateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@C_CreateDate", company.C_CreateDate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@C_CreateDate", DBNull.Value));
                }

                if (!company.C_UpdateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@C_UpdateDate", company.C_UpdateDate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@C_UpdateDate", DBNull.Value));
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

        public int Edit(SysCompany company)
        {
            try
            {
                string sql = @"UPDATE [Sys_Company]
   SET [C_Name] = @C_Name
      ,[C_Phone] = @C_Phone
      ,[C_Fax] = @C_Fax
      ,[C_Email] = @C_Email
      ,[C_Address] = @C_Address
      ,[C_Remark] = @C_Remark
      ,[C_CreateBy] = @C_CreateBy
      ,[C_CreateDate] = @C_CreateDate
      ,[C_UpdateBy] = @C_UpdateBy
      ,[C_UpdateDate] = @C_UpdateDate
 WHERE [C_Code] = @C_Code";

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@C_Code", company.C_Code));
                parameters.Add(new SqlParameter("@C_Name", company.C_Name));

                if (!string.IsNullOrEmpty(company.C_Phone))
                {
                    parameters.Add(new SqlParameter("@C_Phone", company.C_Phone));
                }
                else
                {
                    parameters.Add(new SqlParameter("@C_Phone", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(company.C_Fax))
                {
                    parameters.Add(new SqlParameter("@C_Fax", company.C_Fax));
                }
                else
                {
                    parameters.Add(new SqlParameter("@C_Fax", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(company.C_Email))
                {
                    parameters.Add(new SqlParameter("@C_Email", company.C_Email));
                }
                else
                {
                    parameters.Add(new SqlParameter("@C_Email", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(company.C_Address))
                {
                    parameters.Add(new SqlParameter("@C_Address", company.C_Address));
                }
                else
                {
                    parameters.Add(new SqlParameter("@C_Address", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(company.C_Remark))
                {
                    parameters.Add(new SqlParameter("@C_Remark", company.C_Remark));
                }
                else
                {
                    parameters.Add(new SqlParameter("@C_Remark", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(company.C_CreateBy))
                {
                    parameters.Add(new SqlParameter("@C_CreateBy", company.C_CreateBy));
                }
                else
                {
                    parameters.Add(new SqlParameter("@C_CreateBy", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(company.C_UpdateBy))
                {
                    parameters.Add(new SqlParameter("@C_UpdateBy", company.C_UpdateBy));
                }
                else
                {
                    parameters.Add(new SqlParameter("@C_UpdateBy", DBNull.Value));
                }


                if (company.C_CreateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@C_CreateDate", company.C_CreateDate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@C_CreateDate", DBNull.Value));
                }

                if (company.C_UpdateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@C_UpdateDate", company.C_UpdateDate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@C_UpdateDate", DBNull.Value));
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
                string sql = string.Format(@"DELETE FROM [Sys_Company] WHERE C_Code='{0}'", C_Code);
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