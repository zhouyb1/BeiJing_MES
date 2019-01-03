using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DataAccess.SqlServer;
using Model;

namespace Business
{
    public class EquipmentBLL
    {
        public List<Equipment> loadData( string E_City,string key)
        {
            try
            {
                StringBuilder sql = new StringBuilder(@"SELECT E_Code
      ,E_BoxCode
      ,('['+Base_Equipment.E_City+']' + Area1.A_Name) E_City
      ,('['+Base_Equipment.E_Village+']' + Area2.A_Name) E_Village
      ,E_Address
      ,E_IP
      ,E_MonitorNumber
      ,E_CameraType
      ,E_CameraQty
      ,E_Direction
      ,E_Range
      ,E_InstallType
      ,E_Height
      ,E_Width
      ,E_Longitude
      ,E_Latitude
      ,E_ElectricityType
      ,E_EquipmentBoxQty
      ,E_OpticalFiberQty1
      ,E_OpticalFiberQty2
      ,E_CreateBy
      ,E_CreateDate
      ,E_UpdateBy
      ,E_UpdateDate
      ,E_Active
  FROM Base_Equipment
  LEFT JOIN Base_Area Area1 ON Area1.A_Code=Base_Equipment.E_City
  LEFT JOIN Base_Area Area2 ON Area2.A_Code=Base_Equipment.E_Village
  WHERE 1=1 ");
                if (!string.IsNullOrEmpty(key))
                {
                    sql.Append(
                        string.Format(" AND (Base_Equipment.E_Code = '{0}' OR Base_Equipment.E_BoxCode = '{0}')",
                            key));
                }

                if (!string.IsNullOrEmpty(E_City))
                {
                    sql.Append(
                        string.Format(" AND (Base_Equipment.E_City = '{0}' )",
                            E_City));
                }

                SqlHelper db = new SqlHelper();
                var rows = db.ExecuteObjects<Equipment>(sql.ToString());

                return rows;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Equipment> getPagerData(int star, int end, string sqlwhere)
        {
            try
            {
                string sql = @"SELECT  *
FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY E_Code ) RowIndex ,
                    E_Code ,
                    E_BoxCode ,
                    ( '[' + Base_Equipment.E_City + ']' + Area1.A_Name ) E_City ,
                    ( '[' + Base_Equipment.E_Village + ']' + Area2.A_Name ) E_Village ,
                    E_Address ,
                    E_IP ,
                    E_MonitorNumber ,
                    E_CameraType ,
                    E_CameraQty ,
                    E_Direction ,
                    E_Range ,
                    E_InstallType ,
                    E_Height ,
                    E_Width ,
                    E_Longitude ,
                    E_Latitude ,
                    E_ElectricityType ,
                    E_EquipmentBoxQty ,
                    E_OpticalFiberQty1 ,
                    E_OpticalFiberQty2 ,
                    E_CreateBy ,
                    E_CreateDate ,
                    E_UpdateBy ,
                    E_UpdateDate ,
                    E_Active
          FROM      Base_Equipment
                    LEFT JOIN Base_Area Area1 ON Area1.A_Code = Base_Equipment.E_City
                    LEFT JOIN Base_Area Area2 ON Area2.A_Code = Base_Equipment.E_Village
          WHERE     1 = 1 {2}
        ) MyData
WHERE MyData.RowIndex >= {0}
      AND MyData.RowIndex < {1}  ";

                SqlHelper db = new SqlHelper();
                var rows = db.ExecuteObjects<Equipment>(string.Format(sql, star, end, sqlwhere));

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
                string sql = @"SELECT  COUNT(E_Code)
FROM    Base_Equipment
WHERE   1 = 1 {0}";

                SqlHelper db = new SqlHelper();
                var row = db.ExecuteScalar<int>(string.Format(sql, sqlwhere));
                return row;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Equipment getDetail(string key)
        {
            try
            {

                string sql = string.Format(@"SELECT [E_Code]
      ,[E_BoxCode]
      ,[E_City]
      ,[E_Village]
      ,[E_Address]
      ,[E_IP]
      ,[E_MonitorNumber]
      ,[E_CameraType]
      ,[E_CameraQty]
      ,[E_Direction]
      ,[E_Range]
      ,[E_InstallType]
      ,[E_Height]
      ,[E_Width]
      ,[E_Longitude]
      ,[E_Latitude]
      ,[E_ElectricityType]
      ,[E_EquipmentBoxQty]
      ,[E_OpticalFiberQty1]
      ,[E_OpticalFiberQty2]
      ,[E_CreateBy]
      ,[E_CreateDate]
      ,[E_UpdateBy]
      ,[E_UpdateDate]
      ,[E_Active]
  FROM [Base_Equipment]
WHERE [E_Code]='{0}'", key);


                SqlHelper db = new SqlHelper();
                var rows = db.ExecuteObject<Equipment>(sql);

                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Exists(string E_Code)
        {
            try
            {
                string sql = @"SELECT COUNT(*) FROM Base_Equipment WHERE E_Code='{0}'";
                SqlHelper db = new SqlHelper();
                var row = db.ExecuteScalar<int>(string.Format(sql, E_Code));

                return row > 0 ? true : false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Add(Equipment equipment)
        {
            try
            {
                string sql = @"INSERT INTO [Base_Equipment]
           ([E_Code]
           ,[E_BoxCode]
           ,[E_City]
           ,[E_Village]
           ,[E_Address]
           ,[E_IP]
           ,[E_MonitorNumber]
           ,[E_CameraType]
           ,[E_CameraQty]
           ,[E_Direction]
           ,[E_Range]
           ,[E_InstallType]
           ,[E_Height]
           ,[E_Width]
           ,[E_Longitude]
           ,[E_Latitude]
           ,[E_ElectricityType]
           ,[E_EquipmentBoxQty]
           ,[E_OpticalFiberQty1]
           ,[E_OpticalFiberQty2]
           ,[E_CreateBy]
           ,[E_CreateDate]
           ,[E_UpdateBy]
           ,[E_UpdateDate]
           ,[E_Active])
     VALUES
           (@E_Code
           ,@E_BoxCode
           ,@E_City
           ,@E_Village
           ,@E_Address
           ,@E_IP
           ,@E_MonitorNumber
           ,@E_CameraType
           ,@E_CameraQty
           ,@E_Direction
           ,@E_Range
           ,@E_InstallType
           ,@E_Height
           ,@E_Width
           ,@E_Longitude
           ,@E_Latitude
           ,@E_ElectricityType
           ,@E_EquipmentBoxQty
           ,@E_OpticalFiberQty1
           ,@E_OpticalFiberQty2
           ,@E_CreateBy
           ,@E_CreateDate
           ,@E_UpdateBy
           ,@E_UpdateDate
           ,@E_Active)";

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@E_Code", equipment.E_Code));
                parameters.Add(new SqlParameter("@E_BoxCode", equipment.E_BoxCode));
                parameters.Add(new SqlParameter("@E_City", equipment.E_City));
                parameters.Add(new SqlParameter("@E_Village", equipment.E_Village));

                parameters.Add(new SqlParameter("@E_Address", equipment.E_Address));
                parameters.Add(new SqlParameter("@E_IP", equipment.E_IP));
                parameters.Add(new SqlParameter("@E_MonitorNumber", equipment.E_MonitorNumber));
                parameters.Add(new SqlParameter("@E_CameraType", equipment.E_CameraType));
                parameters.Add(new SqlParameter("@E_CameraQty", equipment.E_CameraQty));

                parameters.Add(new SqlParameter("@E_ElectricityType", equipment.E_ElectricityType));
                parameters.Add(new SqlParameter("@E_Active", equipment.E_Active));

                if (!string.IsNullOrEmpty(equipment.E_Direction))
                {
                    parameters.Add(new SqlParameter("@E_Direction", equipment.E_Direction));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_Direction", DBNull.Value));
                }


                if (!string.IsNullOrEmpty(equipment.E_Range))
                {
                    parameters.Add(new SqlParameter("@E_Range", equipment.E_Range));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_Range", DBNull.Value));
                }



                if (!string.IsNullOrEmpty(equipment.E_InstallType))
                {
                    parameters.Add(new SqlParameter("@E_InstallType", equipment.E_InstallType));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_InstallType", DBNull.Value));
                }



                if (equipment.E_Height.HasValue)
                {
                    parameters.Add(new SqlParameter("@E_Height", equipment.E_Height));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_Height", DBNull.Value));
                }

                if (equipment.E_Width.HasValue)
                {
                    parameters.Add(new SqlParameter("@E_Width", equipment.E_Width));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_Width", DBNull.Value));
                }


                if (!string.IsNullOrEmpty(equipment.E_Longitude))
                {
                    parameters.Add(new SqlParameter("@E_Longitude", equipment.E_Longitude));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_Longitude", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(equipment.E_Latitude))
                {
                    parameters.Add(new SqlParameter("@E_Latitude", equipment.E_Latitude));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_Latitude", DBNull.Value));
                }


                if (equipment.E_EquipmentBoxQty.HasValue)
                {
                    parameters.Add(new SqlParameter("@E_EquipmentBoxQty", equipment.E_EquipmentBoxQty));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_EquipmentBoxQty", DBNull.Value));
                }


                if (equipment.E_OpticalFiberQty1.HasValue)
                {
                    parameters.Add(new SqlParameter("@E_OpticalFiberQty1", equipment.E_OpticalFiberQty1));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_OpticalFiberQty1", DBNull.Value));
                }

                if (equipment.E_OpticalFiberQty2.HasValue)
                {
                    parameters.Add(new SqlParameter("@E_OpticalFiberQty2", equipment.E_OpticalFiberQty2));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_OpticalFiberQty2", DBNull.Value));
                }



                if (!string.IsNullOrEmpty(equipment.E_CreateBy))
                {
                    parameters.Add(new SqlParameter("@E_CreateBy", equipment.E_CreateBy));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_CreateBy", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(equipment.E_UpdateBy))
                {
                    parameters.Add(new SqlParameter("@E_UpdateBy", equipment.E_UpdateBy));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_UpdateBy", DBNull.Value));
                }


                if (equipment.E_CreateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@E_CreateDate", equipment.E_CreateDate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_CreateDate", DBNull.Value));
                }

                if (!equipment.E_UpdateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@E_UpdateDate", equipment.E_UpdateDate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_UpdateDate", DBNull.Value));
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

        public int Edit(Equipment equipment)
        {
            try
            {
                string sql = @"UPDATE  [Base_Equipment]
   SET 
       [E_BoxCode] = @E_BoxCode
      ,[E_City] = @E_City
      ,[E_Village] = @E_Village
      ,[E_Address] = @E_Address
      ,[E_IP] = @E_IP
      ,[E_MonitorNumber] = @E_MonitorNumber
      ,[E_CameraType] = @E_CameraType
      ,[E_CameraQty] = @E_CameraQty
      ,[E_Direction] = @E_Direction
      ,[E_Range] = @E_Range
      ,[E_InstallType] = @E_InstallType
      ,[E_Height] = @E_Height
      ,[E_Width] = @E_Width
      ,[E_Longitude] = @E_Longitude
      ,[E_Latitude] = @E_Latitude
      ,[E_ElectricityType] = @E_ElectricityType
      ,[E_EquipmentBoxQty] = @E_EquipmentBoxQty
      ,[E_OpticalFiberQty1] = @E_OpticalFiberQty1
      ,[E_OpticalFiberQty2] = @E_OpticalFiberQty2
      ,[E_CreateBy] = @E_CreateBy
      ,[E_CreateDate] = @E_CreateDate
      ,[E_UpdateBy] = @E_UpdateBy
      ,[E_UpdateDate] = @E_UpdateDate
      ,[E_Active] = @E_Active
 WHERE [E_Code] = @E_Code";


                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@E_Code", equipment.E_Code));
                parameters.Add(new SqlParameter("@E_BoxCode", equipment.E_BoxCode));
                parameters.Add(new SqlParameter("@E_City", equipment.E_City));
                parameters.Add(new SqlParameter("@E_Village", equipment.E_Village));

                parameters.Add(new SqlParameter("@E_Address", equipment.E_Address));
                parameters.Add(new SqlParameter("@E_IP", equipment.E_IP));
                parameters.Add(new SqlParameter("@E_MonitorNumber", equipment.E_MonitorNumber));
                parameters.Add(new SqlParameter("@E_CameraType", equipment.E_CameraType));
                parameters.Add(new SqlParameter("@E_CameraQty", equipment.E_CameraQty));

                parameters.Add(new SqlParameter("@E_ElectricityType", equipment.E_ElectricityType));
                parameters.Add(new SqlParameter("@E_Active", equipment.E_Active));

                if (!string.IsNullOrEmpty(equipment.E_Direction))
                {
                    parameters.Add(new SqlParameter("@E_Direction", equipment.E_Direction));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_Direction", DBNull.Value));
                }


                if (!string.IsNullOrEmpty(equipment.E_Range))
                {
                    parameters.Add(new SqlParameter("@E_Range", equipment.E_Range));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_Range", DBNull.Value));
                }



                if (!string.IsNullOrEmpty(equipment.E_InstallType))
                {
                    parameters.Add(new SqlParameter("@E_InstallType", equipment.E_InstallType));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_InstallType", DBNull.Value));
                }



                if (equipment.E_Height.HasValue)
                {
                    parameters.Add(new SqlParameter("@E_Height", equipment.E_Height));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_Height", DBNull.Value));
                }

                if (equipment.E_Width.HasValue)
                {
                    parameters.Add(new SqlParameter("@E_Width", equipment.E_Width));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_Width", DBNull.Value));
                }


                if (!string.IsNullOrEmpty(equipment.E_Longitude))
                {
                    parameters.Add(new SqlParameter("@E_Longitude", equipment.E_Longitude));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_Longitude", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(equipment.E_Latitude))
                {
                    parameters.Add(new SqlParameter("@E_Latitude", equipment.E_Latitude));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_Latitude", DBNull.Value));
                }


                if (equipment.E_EquipmentBoxQty.HasValue)
                {
                    parameters.Add(new SqlParameter("@E_EquipmentBoxQty", equipment.E_EquipmentBoxQty));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_EquipmentBoxQty", DBNull.Value));
                }


                if (equipment.E_OpticalFiberQty1.HasValue)
                {
                    parameters.Add(new SqlParameter("@E_OpticalFiberQty1", equipment.E_OpticalFiberQty1));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_OpticalFiberQty1", DBNull.Value));
                }

                if (equipment.E_OpticalFiberQty2.HasValue)
                {
                    parameters.Add(new SqlParameter("@E_OpticalFiberQty2", equipment.E_OpticalFiberQty2));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_OpticalFiberQty2", DBNull.Value));
                }



                if (!string.IsNullOrEmpty(equipment.E_CreateBy))
                {
                    parameters.Add(new SqlParameter("@E_CreateBy", equipment.E_CreateBy));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_CreateBy", DBNull.Value));
                }

                if (!string.IsNullOrEmpty(equipment.E_UpdateBy))
                {
                    parameters.Add(new SqlParameter("@E_UpdateBy", equipment.E_UpdateBy));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_UpdateBy", DBNull.Value));
                }


                if (equipment.E_CreateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@E_CreateDate", equipment.E_CreateDate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_CreateDate", DBNull.Value));
                }

                if (!equipment.E_UpdateDate.HasValue)
                {
                    parameters.Add(new SqlParameter("@E_UpdateDate", equipment.E_UpdateDate));
                }
                else
                {
                    parameters.Add(new SqlParameter("@E_UpdateDate", DBNull.Value));
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

        public int Delete(string E_Code)
        {
            try
            {
                string sql = string.Format(@"DELETE FROM [Base_Equipment] WHERE E_Code='{0}'", E_Code);
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