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
    /// 描述: 业务层 -- Mes_CollarDetail
    /// </summary>
    public partial class MesCollarDetailBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
		public List<MesCollarDetailEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_CollarDetail");
                var rows = db.ExecuteObjects<MesCollarDetailEntity>(strSql.ToString());
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
        /// <returns>MesCollarDetail</returns>
		public MesCollarDetailEntity GetEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_CollarDetail");
                strSql.Append(" WHERE ID=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var rowData = db.ExecuteObject<MesCollarDetailEntity>(strSql.ToString(),paramList.ToArray());
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
				strSql.Append("DELETE Mes_CollarDetail");
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
        public int SaveEntity(string keyValue,MesCollarDetailEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO Mes_CollarDetail(");
                     strSql.Append("ID,");
                     strSql.Append("C_CollarNo,");
                     strSql.Append("C_OrderNo,");
                     strSql.Append("C_GoodsCode,");
                     strSql.Append("C_GoodsName,");
                     strSql.Append("C_Unit,");
                     strSql.Append("C_Qty,");
                     strSql.Append("C_Batch,");
                     strSql.Append("C_Remark");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@ID,");
                     strSql.Append("@C_CollarNo,");
                     strSql.Append("@C_OrderNo,");
                     strSql.Append("@C_GoodsCode,");
                     strSql.Append("@C_GoodsName,");
                     strSql.Append("@C_Unit,");
                     strSql.Append("@C_Qty,");
                     strSql.Append("@C_Batch,");
                     strSql.Append("@C_Remark");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@ID",Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE Mes_CollarDetail SET ");
                     strSql.Append("C_CollarNo=@C_CollarNo,");
                     strSql.Append("C_OrderNo=@C_OrderNo,");
                     strSql.Append("C_GoodsCode=@C_GoodsCode,");
                     strSql.Append("C_GoodsName=@C_GoodsName,");
                     strSql.Append("C_Unit=@C_Unit,");
                     strSql.Append("C_Qty=@C_Qty,");
                     strSql.Append("C_Batch=@C_Batch,");
                     strSql.Append("C_Remark=@C_Remark ");
                     strSql.Append(" WHERE ID=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@C_CollarNo",entity.C_CollarNo));
                paramList.Add(new SqlParameter("@C_OrderNo",entity.C_OrderNo));
                paramList.Add(new SqlParameter("@C_GoodsCode",entity.C_GoodsCode));
                paramList.Add(new SqlParameter("@C_GoodsName",entity.C_GoodsName));
                paramList.Add(new SqlParameter("@C_Unit",entity.C_Unit));
                paramList.Add(new SqlParameter("@C_Qty",entity.C_Qty));
                paramList.Add(new SqlParameter("@C_Batch",entity.C_Batch));
                paramList.Add(new SqlParameter("@C_Remark",entity.C_Remark));
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
