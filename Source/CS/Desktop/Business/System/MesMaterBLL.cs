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
    /// 描述: 业务层 -- Mes_Mater
    /// </summary>
    public partial class MesMaterBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
		public List<MesMaterEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_Mater");
                var rows = db.ExecuteObjects<MesMaterEntity>(strSql.ToString());
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
        /// <returns>MesMater</returns>
		public MesMaterEntity GetEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_Mater");
                strSql.Append(" WHERE ID=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var rowData = db.ExecuteObject<MesMaterEntity>(strSql.ToString(),paramList.ToArray());
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
				strSql.Append("DELETE Mes_Mater");
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
        public int SaveEntity(string keyValue,MesMaterEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO Mes_Mater(");
                     strSql.Append("ID,");
                     strSql.Append("P_OrderNo,");
                     strSql.Append("P_OrderDate,");
                     strSql.Append("P_GoodsCode,");
                     strSql.Append("P_GoodsName,");
                     strSql.Append("P_Unit,");
                     strSql.Append("P_Qty");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@ID,");
                     strSql.Append("@P_OrderNo,");
                     strSql.Append("@P_OrderDate,");
                     strSql.Append("@P_GoodsCode,");
                     strSql.Append("@P_GoodsName,");
                     strSql.Append("@P_Unit,");
                     strSql.Append("@P_Qty");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@ID",Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE Mes_Mater SET ");
                     strSql.Append("P_OrderNo=@P_OrderNo,");
                     strSql.Append("P_OrderDate=@P_OrderDate,");
                     strSql.Append("P_GoodsCode=@P_GoodsCode,");
                     strSql.Append("P_GoodsName=@P_GoodsName,");
                     strSql.Append("P_Unit=@P_Unit,");
                     strSql.Append("P_Qty=@P_Qty ");
                     strSql.Append(" WHERE ID=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@P_OrderNo",entity.P_OrderNo));
                paramList.Add(new SqlParameter("@P_OrderDate",entity.P_OrderDate));
                paramList.Add(new SqlParameter("@P_GoodsCode",entity.P_GoodsCode));
                paramList.Add(new SqlParameter("@P_GoodsName",entity.P_GoodsName));
                paramList.Add(new SqlParameter("@P_Unit",entity.P_Unit));
                paramList.Add(new SqlParameter("@P_Qty",entity.P_Qty));
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
