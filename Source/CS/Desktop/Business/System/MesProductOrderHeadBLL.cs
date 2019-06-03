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
    /// 描述: 业务层 -- Mes_ProductOrderHead
    /// </summary>
    public partial class MesProductOrderHeadBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<MesProductOrderHeadEntity> GetList(string OrderNo)
        {
            try
            {
                var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
				strSql.Append("SELECT * FROM Mes_ProductOrderHead");
                strSql.Append(" where P_OrderNo = @P_OrderNo");
                paramList.Add(new SqlParameter("@P_OrderNo", string.Format("{0}", OrderNo)));
                var rows = db.ExecuteObjects<MesProductOrderHeadEntity>(strSql.ToString(), paramList.ToArray());
                return rows;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<MesProductOrderHeadEntity> GetListAll()
        {
            try
            {
                var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                strSql.Append("SELECT * FROM Mes_ProductOrderHead");
                //strSql.Append(" where P_OrderNo = @P_OrderNo");
                //paramList.Add(new SqlParameter("@P_OrderNo", string.Format("{0}", OrderNo)));
                var rows = db.ExecuteObjects<MesProductOrderHeadEntity>(strSql.ToString(), paramList.ToArray());
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
        /// <returns>MesProductOrderHead</returns>
		public MesProductOrderHeadEntity GetEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_ProductOrderHead");
                strSql.Append(" WHERE ID=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var rowData = db.ExecuteObject<MesProductOrderHeadEntity>(strSql.ToString(),paramList.ToArray());
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
				strSql.Append("DELETE Mes_ProductOrderHead");
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
        public int SaveEntity(string keyValue,MesProductOrderHeadEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO Mes_ProductOrderHead(");
                     strSql.Append("ID,");
                     strSql.Append("P_OrderNo,");
                     strSql.Append("P_OrderDate,");
                     strSql.Append("P_OrderStationID,");
                     strSql.Append("P_OrderStationName,");
                     strSql.Append("P_CreateBy,");
                     strSql.Append("P_CreateDate,");
                     strSql.Append("P_UpdateBy,");
                     strSql.Append("P_UpdateDate");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@ID,");
                     strSql.Append("@P_OrderNo,");
                     strSql.Append("@P_OrderDate,");
                     strSql.Append("@P_OrderStationID,");
                     strSql.Append("@P_OrderStationName,");
                     strSql.Append("@P_CreateBy,");
                     strSql.Append("@P_CreateDate,");
                     strSql.Append("@P_UpdateBy,");
                     strSql.Append("@P_UpdateDate");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@ID",Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE Mes_ProductOrderHead SET ");
                     strSql.Append("P_OrderNo=@P_OrderNo,");
                     strSql.Append("P_OrderDate=@P_OrderDate,");
                     strSql.Append("P_OrderStationID=@P_OrderStationID,");
                     strSql.Append("P_OrderStationName=@P_OrderStationName,");
                     strSql.Append("P_CreateBy=@P_CreateBy,");
                     strSql.Append("P_CreateDate=@P_CreateDate,");
                     strSql.Append("P_UpdateBy=@P_UpdateBy,");
                     strSql.Append("P_UpdateDate=@P_UpdateDate ");
                     strSql.Append(" WHERE ID=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@P_OrderNo",entity.P_OrderNo));
                paramList.Add(new SqlParameter("@P_OrderDate",entity.P_OrderDate));
                paramList.Add(new SqlParameter("@P_OrderStationID",entity.P_OrderStationID));
                paramList.Add(new SqlParameter("@P_OrderStationName",entity.P_OrderStationName));
                paramList.Add(new SqlParameter("@P_CreateBy",entity.P_CreateBy));
                paramList.Add(new SqlParameter("@P_CreateDate",entity.P_CreateDate));
                paramList.Add(new SqlParameter("@P_UpdateBy",entity.P_UpdateBy));
                paramList.Add(new SqlParameter("@P_UpdateDate",entity.P_UpdateDate));
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
