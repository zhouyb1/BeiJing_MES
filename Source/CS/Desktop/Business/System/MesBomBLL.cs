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
    /// 描述: 业务层 -- Mes_Bom
    /// </summary>
    public partial class MesBomBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
		public List<MesBomEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_Bom");
                var rows = db.ExecuteObjects<MesBomEntity>(strSql.ToString());
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
        /// <returns>MesBom</returns>
		public MesBomEntity GetEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_Bom");
                strSql.Append(" WHERE ID=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var rowData = db.ExecuteObject<MesBomEntity>(strSql.ToString(),paramList.ToArray());
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
				strSql.Append("DELETE Mes_Bom");
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
        public int SaveEntity(string keyValue,MesBomEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO Mes_Bom(");
                     strSql.Append("ID,");
                     strSql.Append("B_Date,");
                     strSql.Append("B_OrderNo,");
                     strSql.Append("B_GoodsCode,");
                     strSql.Append("B_GoodsName,");
                     strSql.Append("B_Unit,");
                     strSql.Append("B_Grade,");
                     strSql.Append("B_SecGoodsCode,");
                     strSql.Append("B_SecGoodsName,");
                     strSql.Append("B_SecUnit,");
                     strSql.Append("B_SecQty,");
                     strSql.Append("B_Conversion,");
                     strSql.Append("B_CreateBy,");
                     strSql.Append("B_CreateDate,");
                     strSql.Append("B_UpdateBy,");
                     strSql.Append("B_UpdateDate,");
                     strSql.Append("B_Remark");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@ID,");
                     strSql.Append("@B_Date,");
                     strSql.Append("@B_OrderNo,");
                     strSql.Append("@B_GoodsCode,");
                     strSql.Append("@B_GoodsName,");
                     strSql.Append("@B_Unit,");
                     strSql.Append("@B_Grade,");
                     strSql.Append("@B_SecGoodsCode,");
                     strSql.Append("@B_SecGoodsName,");
                     strSql.Append("@B_SecUnit,");
                     strSql.Append("@B_SecQty,");
                     strSql.Append("@B_Conversion,");
                     strSql.Append("@B_CreateBy,");
                     strSql.Append("@B_CreateDate,");
                     strSql.Append("@B_UpdateBy,");
                     strSql.Append("@B_UpdateDate,");
                     strSql.Append("@B_Remark");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@ID",Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE Mes_Bom SET ");
                     strSql.Append("B_Date=@B_Date,");
                     strSql.Append("B_OrderNo=@B_OrderNo,");
                     strSql.Append("B_GoodsCode=@B_GoodsCode,");
                     strSql.Append("B_GoodsName=@B_GoodsName,");
                     strSql.Append("B_Unit=@B_Unit,");
                     strSql.Append("B_Grade=@B_Grade,");
                     strSql.Append("B_SecGoodsCode=@B_SecGoodsCode,");
                     strSql.Append("B_SecGoodsName=@B_SecGoodsName,");
                     strSql.Append("B_SecUnit=@B_SecUnit,");
                     strSql.Append("B_SecQty=@B_SecQty,");
                     strSql.Append("B_Conversion=@B_Conversion,");
                     strSql.Append("B_CreateBy=@B_CreateBy,");
                     strSql.Append("B_CreateDate=@B_CreateDate,");
                     strSql.Append("B_UpdateBy=@B_UpdateBy,");
                     strSql.Append("B_UpdateDate=@B_UpdateDate,");
                     strSql.Append("B_Remark=@B_Remark ");
                     strSql.Append(" WHERE ID=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@B_Date",entity.B_Date));
                paramList.Add(new SqlParameter("@B_OrderNo",entity.B_OrderNo));
                paramList.Add(new SqlParameter("@B_GoodsCode",entity.B_GoodsCode));
                paramList.Add(new SqlParameter("@B_GoodsName",entity.B_GoodsName));
                paramList.Add(new SqlParameter("@B_Unit",entity.B_Unit));
                paramList.Add(new SqlParameter("@B_Grade",entity.B_Grade));
                paramList.Add(new SqlParameter("@B_SecGoodsCode",entity.B_SecGoodsCode));
                paramList.Add(new SqlParameter("@B_SecGoodsName",entity.B_SecGoodsName));
                paramList.Add(new SqlParameter("@B_SecUnit",entity.B_SecUnit));
                paramList.Add(new SqlParameter("@B_SecQty",entity.B_SecQty));
                paramList.Add(new SqlParameter("@B_Conversion",entity.B_Conversion));
                paramList.Add(new SqlParameter("@B_CreateBy",entity.B_CreateBy));
                paramList.Add(new SqlParameter("@B_CreateDate",entity.B_CreateDate));
                paramList.Add(new SqlParameter("@B_UpdateBy",entity.B_UpdateBy));
                paramList.Add(new SqlParameter("@B_UpdateDate",entity.B_UpdateDate));
                paramList.Add(new SqlParameter("@B_Remark",entity.B_Remark));
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
