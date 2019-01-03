using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.SqlServer;
using Model;

namespace Business.System
{
    public class AreaBLL
    {
        public List<Area> loadData()
        {
            try
            {
                string sql = @"SELECT [A_Code]
      ,('['+[A_Code]+']'+[A_Name])[A_Name]
      ,[A_Parent]
  FROM [Base_Area]";
              
                SqlHelper db = new SqlHelper();
                var rows = db.ExecuteObjects<Area>(sql);

                return rows;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Area getDetail(string key)
        {
            try
            {
                string sql = @"SELECT [A_Code]
      ,[A_Name]
      ,[A_Parent]
  FROM [Base_Area]
WHERE [A_Code]='{0}'";

                SqlHelper db = new SqlHelper();
                var row = db.ExecuteObject<Area>(string.Format(sql,key));

                return row;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Exists(string A_Code)
        {
            try
            {
                string sql = @"SELECT COUNT(*) FROM Base_Area WHERE A_Code='{0}'";
                SqlHelper db = new SqlHelper();
                var row = db.ExecuteScalar<int>(string.Format(sql, A_Code));

                return row > 0 ? true : false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Add(Area area)
        {
            try
            {
                string sql = @"INSERT INTO [Base_Area]
           ([A_Code]
           ,[A_Name]
           ,[A_Parent])
     VALUES
           (@A_Code
           ,@A_Name
           ,@A_Parent)";

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@A_Code", area.A_Code));
                parameters.Add(new SqlParameter("@A_Name", area.A_Name));

                if (!string.IsNullOrEmpty(area.A_Parent))
                {
                    parameters.Add(new SqlParameter("@A_Parent", area.A_Parent));
                }
                else
                {
                    parameters.Add(new SqlParameter("@A_Parent", DBNull.Value));
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

        public int Edit(Area area)
        {
            try
            {
                string sql = @"UPDATE [Base_Area]
   SET 
       [A_Name] = @A_Name
      ,[A_Parent] = @A_Parent
 WHERE [A_Code] = @A_Code";

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@A_Code", area.A_Code));
                parameters.Add(new SqlParameter("@A_Name", area.A_Name));

                if (!string.IsNullOrEmpty(area.A_Parent))
                {
                    parameters.Add(new SqlParameter("@A_Parent", area.A_Parent));
                }
                else
                {
                    parameters.Add(new SqlParameter("@A_Parent", DBNull.Value));
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

        public int Del(string A_Code)
        {
            try
            {
                string sql = @"DELETE [Base_Area]
WHERE A_Code = '{0}'
      AND
    (
        SELECT COUNT(A_Parent) FROM [Base_Area] WHERE A_Parent = '{0}'
    ) = 0";


                SqlHelper db = new SqlHelper();
                var row = db.ExecuteNonQuery(string.Format(sql,A_Code));

                return row;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
