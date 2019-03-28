using System;
using System.Collections.Generic;
using System.Linq;
using Model.Dto;
using Model;
using DataAccess.SqlServer;
using System.Text;
using System.Data.SqlClient;

namespace Business.System
{
    /// <summary>
    /// 描述: 业务层 -- Mes_Device
    /// </summary>
    public partial class MesDeviceBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<MesDeviceDto> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_Device");
                var rows = db.ExecuteObjects<MesDeviceDto>(strSql.ToString());
                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 根据设备名获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<MesDeviceDto> GetList_Name(string name)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT * FROM Mes_Device");
                strSql.Append(" WHERE D_Name LIKE @D_Name");
                var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@D_Name", string.Format("%{0}%", name)));
                var rows = db.ExecuteObjects<MesDeviceDto>(strSql.ToString(), paramList.ToArray());
                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 根据部门获取设备数据列表
        /// </summary>
        /// <returns></returns>
        public List<MesDeviceEntity> GetList_Deparemaent(string D_Department)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT * FROM Mes_Device");
                strSql.Append(" WHERE D_Department LIKE @D_Department");
                var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@D_Department", string.Format("%{0}%", D_Department)));
                var rows = db.ExecuteObjects<MesDeviceEntity>(strSql.ToString(), paramList.ToArray());
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
		/// <param name="keyValue">主键</param>
        /// <returns>MesDevice</returns>
		public MesDeviceEntity GetEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_Device");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var rowData = db.ExecuteObject<MesDeviceEntity>(strSql.ToString(),paramList.ToArray());
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
				strSql.Append("DELETE Mes_Device");
                strSql.Append(" WHERE ID=@ID");
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
        public int SaveEntity(string keyValue,MesDeviceEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO Mes_Device(");
                     strSql.Append("ID,");
                     strSql.Append("D_Department,");
                     strSql.Append("D_Name,");
                     strSql.Append("D_IP,");
                     strSql.Append("D_Remark");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@ID,");
                     strSql.Append("@D_Department,");
                     strSql.Append("@D_Name,");
                     strSql.Append("@D_IP,");
                     strSql.Append("@D_Remark");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@ID", Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE Mes_Device SET ");
                     strSql.Append("ID=@ID,");
                     strSql.Append("D_Department=@D_Department,");
                     strSql.Append("D_Name=@D_Name,");
                     strSql.Append("D_IP=@D_IP,");
                     strSql.Append("D_Remark=@D_Remark ");
                     strSql.Append(" WHERE ID=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@D_Department",entity.D_Department));
                paramList.Add(new SqlParameter("@D_Name",entity.D_Name));
                paramList.Add(new SqlParameter("@D_IP",entity.D_IP));
                paramList.Add(new SqlParameter("@D_Remark", entity.D_Remark));
				var result = db.ExecuteNonQuery(strSql.ToString(),paramList.ToArray());
                return result;
            }
            catch (Exception)
            {
                throw;
            }			
		}
        
    }
}
