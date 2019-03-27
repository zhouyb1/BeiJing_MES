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
    /// 描述: 业务层 -- Mes_MoveRecord
    /// </summary>
    public partial class MesMoveRecordBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
		public List<MesMoveRecordEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_MoveRecord");
                var rows = db.ExecuteObjects<MesMoveRecordEntity>(strSql.ToString());
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
        /// <returns>MesMoveRecord</returns>
		public MesMoveRecordEntity GetEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_MoveRecord");
                strSql.Append(" WHERE ID=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var rowData = db.ExecuteObject<MesMoveRecordEntity>(strSql.ToString(),paramList.ToArray());
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
				strSql.Append("DELETE Mes_MoveRecord");
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
        public int SaveEntity(string keyValue,MesMoveRecordEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO Mes_MoveRecord(");
                     strSql.Append("ID,");
                     strSql.Append("M_UserCode,");
                     strSql.Append("M_UserName,");
                     strSql.Append("M_IP,");
                     strSql.Append("M_RFIDCode,");
                     strSql.Append("M_DoorCode,");
                     strSql.Append("M_DoorName,");
                     strSql.Append("M_Date,");
                     strSql.Append("M_Remark");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@ID,");
                     strSql.Append("@M_UserCode,");
                     strSql.Append("@M_UserName,");
                     strSql.Append("@M_IP,");
                     strSql.Append("@M_RFIDCode,");
                     strSql.Append("@M_DoorCode,");
                     strSql.Append("@M_DoorName,");
                     strSql.Append("@M_Date,");
                     strSql.Append("@M_Remark");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@ID",Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE Mes_MoveRecord SET ");
                     strSql.Append("M_UserCode=@M_UserCode,");
                     strSql.Append("M_UserName=@M_UserName,");
                     strSql.Append("M_IP=@M_IP,");
                     strSql.Append("M_RFIDCode=@M_RFIDCode,");
                     strSql.Append("M_DoorCode=@M_DoorCode,");
                     strSql.Append("M_DoorName=@M_DoorName,");
                     strSql.Append("M_Date=@M_Date,");
                     strSql.Append("M_Remark=@M_Remark ");
                     strSql.Append(" WHERE ID=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@M_UserCode",entity.M_UserCode));
                paramList.Add(new SqlParameter("@M_UserName",entity.M_UserName));
                paramList.Add(new SqlParameter("@M_IP",entity.M_IP));
                paramList.Add(new SqlParameter("@M_RFIDCode",entity.M_RFIDCode));
                paramList.Add(new SqlParameter("@M_DoorCode",entity.M_DoorCode));
                paramList.Add(new SqlParameter("@M_DoorName",entity.M_DoorName));
                paramList.Add(new SqlParameter("@M_Date",entity.M_Date));
                paramList.Add(new SqlParameter("@M_Remark",entity.M_Remark));
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
