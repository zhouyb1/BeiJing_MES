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
    /// 描述: 业务层 -- Mes_IPToRFID
    /// </summary>
    public partial class MesIPToRFIDBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
		public List<MesIPToRFIDEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_IPToRFID");
                var rows = db.ExecuteObjects<MesIPToRFIDEntity>(strSql.ToString());
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
        /// <returns>MesIPToRFID</returns>
		public MesIPToRFIDEntity GetEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_IPToRFID");
                strSql.Append(" WHERE ID=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var rowData = db.ExecuteObject<MesIPToRFIDEntity>(strSql.ToString(),paramList.ToArray());
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
				strSql.Append("DELETE Mes_IPToRFID");
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
        public int SaveEntity(string keyValue,MesIPToRFIDEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO Mes_IPToRFID(");
                     strSql.Append("ID,");
                     strSql.Append("I_IP,");
                     strSql.Append("I_RFIDCode,");
                     strSql.Append("I_DoorCode,");
                     strSql.Append("I_DoorName,");
                     strSql.Append("I_Remark");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@ID,");
                     strSql.Append("@I_IP,");
                     strSql.Append("@I_RFIDCode,");
                     strSql.Append("@I_DoorCode,");
                     strSql.Append("@I_DoorName,");
                     strSql.Append("@I_Remark");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@ID",Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE Mes_IPToRFID SET ");
                     strSql.Append("I_IP=@I_IP,");
                     strSql.Append("I_RFIDCode=@I_RFIDCode,");
                     strSql.Append("I_DoorCode=@I_DoorCode,");
                     strSql.Append("I_DoorName=@I_DoorName,");
                     strSql.Append("I_Remark=@I_Remark ");
                     strSql.Append(" WHERE ID=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@I_IP",entity.I_IP));
                paramList.Add(new SqlParameter("@I_RFIDCode",entity.I_RFIDCode));
                paramList.Add(new SqlParameter("@I_DoorCode",entity.I_DoorCode));
                paramList.Add(new SqlParameter("@I_DoorName",entity.I_DoorName));
                paramList.Add(new SqlParameter("@I_Remark",entity.I_Remark));
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
