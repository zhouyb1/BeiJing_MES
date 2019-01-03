using System;
using System.Collections.Generic;
using DataAccess.SqlServer;
using Model;

namespace Business.System
{
    public class InspectBLL
    {
        public List<Inspect> getPagerData(int star, int end,string sqlwhere)
        {
            try
            {
                string sql = @"SELECT RowIndex,
    E_ID,
    E_BoxCode,
    E_Code,
    I_Type,
    I_Result,
    I_Photo,
    I_User,
    I_Date,
    E_City,
    E_Village,
    E_Address,
    E_IP,
    E_MonitorNumber,
    E_CameraType
FROM
(
  SELECT  ROW_NUMBER() OVER ( ORDER BY Base_Inspect.I_Date DESC) RowIndex ,
        Base_Inspect.E_ID ,
        Base_Inspect.E_BoxCode ,
        Base_Inspect.E_Code ,
        I_Type ,
        I_Result ,
        I_Photo ,
        I_User ,
        I_Date ,
        ( '[' + Base_Equipment.E_City + ']' + Area1.A_Name ) E_City ,
        ( '[' + Base_Equipment.E_Village + ']' + Area2.A_Name ) E_Village ,
        E_Address ,
        E_IP ,
        E_MonitorNumber ,
        E_CameraType
FROM    Base_Inspect
        LEFT JOIN Base_Equipment ON Base_Equipment.E_Code = Base_Inspect.E_Code
        LEFT JOIN Base_Area Area1 ON Area1.A_Code = Base_Equipment.E_City
        LEFT JOIN Base_Area Area2 ON Area2.A_Code = Base_Equipment.E_Village
WHERE   1 = 1 {2}
) MyData
WHERE MyData.RowIndex >= {0}
      AND MyData.RowIndex <{1}";

                SqlHelper db = new SqlHelper();
                var rows = db.ExecuteObjects<Inspect>(string.Format(sql, star, end, sqlwhere));

                return rows;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// 获得总行数
        /// </summary>
        /// <returns></returns>
        public int getRowCount(string sqlwhere)
        {
            try
            {
                string sql = @"SELECT  COUNT(Base_Inspect.E_Code)
FROM    Base_Inspect
        LEFT JOIN Base_Equipment ON Base_Equipment.E_Code = Base_Inspect.E_Code
        LEFT JOIN Base_Area Area1 ON Area1.A_Code = Base_Equipment.E_City
        LEFT JOIN Base_Area Area2 ON Area2.A_Code = Base_Equipment.E_Village
WHERE   1 = 1  {0}";

                SqlHelper db = new SqlHelper();
                var row = db.ExecuteScalar<int>(string.Format(sql, sqlwhere));
                return row;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="E_ID"></param>
        /// <returns></returns>
        public int Delete(string E_ID)
        {
            try
            {
                string sql = string.Format(@"DELETE FROM [Base_Inspect] WHERE E_ID='{0}'", E_ID);
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