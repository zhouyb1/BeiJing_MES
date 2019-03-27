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
    /// 描述: 业务层 -- Mes_OrgResDetail
    /// </summary>
    public partial class MesOrgResDetailBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
		public List<MesOrgResDetailEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_OrgResDetail");
                var rows = db.ExecuteObjects<MesOrgResDetailEntity>(strSql.ToString());
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
        /// <returns>MesOrgResDetail</returns>
		public MesOrgResDetailEntity GetEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_OrgResDetail");
                strSql.Append(" WHERE ID=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var rowData = db.ExecuteObject<MesOrgResDetailEntity>(strSql.ToString(),paramList.ToArray());
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
				strSql.Append("DELETE Mes_OrgResDetail");
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
        public int SaveEntity(string keyValue,MesOrgResDetailEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO Mes_OrgResDetail(");
                     strSql.Append("ID,");
                     strSql.Append("O_OrgResNo,");
                     strSql.Append("O_OrderNo,");
                     strSql.Append("O_GoodsCode,");
                     strSql.Append("O_GoodsName,");
                     strSql.Append("O_Unit,");
                     strSql.Append("O_Qty,");
                     strSql.Append("O_Batch,");
                     strSql.Append("O_Price,");
                     strSql.Append("O_SecGoodsCode,");
                     strSql.Append("O_SecGoodsName,");
                     strSql.Append("O_SecUnit,");
                     strSql.Append("O_SecQty,");
                     strSql.Append("O_SecBatch,");
                     strSql.Append("O_SecPrice");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@ID,");
                     strSql.Append("@O_OrgResNo,");
                     strSql.Append("@O_OrderNo,");
                     strSql.Append("@O_GoodsCode,");
                     strSql.Append("@O_GoodsName,");
                     strSql.Append("@O_Unit,");
                     strSql.Append("@O_Qty,");
                     strSql.Append("@O_Batch,");
                     strSql.Append("@O_Price,");
                     strSql.Append("@O_SecGoodsCode,");
                     strSql.Append("@O_SecGoodsName,");
                     strSql.Append("@O_SecUnit,");
                     strSql.Append("@O_SecQty,");
                     strSql.Append("@O_SecBatch,");
                     strSql.Append("@O_SecPrice");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@ID",Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE Mes_OrgResDetail SET ");
                     strSql.Append("O_OrgResNo=@O_OrgResNo,");
                     strSql.Append("O_OrderNo=@O_OrderNo,");
                     strSql.Append("O_GoodsCode=@O_GoodsCode,");
                     strSql.Append("O_GoodsName=@O_GoodsName,");
                     strSql.Append("O_Unit=@O_Unit,");
                     strSql.Append("O_Qty=@O_Qty,");
                     strSql.Append("O_Batch=@O_Batch,");
                     strSql.Append("O_Price=@O_Price,");
                     strSql.Append("O_SecGoodsCode=@O_SecGoodsCode,");
                     strSql.Append("O_SecGoodsName=@O_SecGoodsName,");
                     strSql.Append("O_SecUnit=@O_SecUnit,");
                     strSql.Append("O_SecQty=@O_SecQty,");
                     strSql.Append("O_SecBatch=@O_SecBatch,");
                     strSql.Append("O_SecPrice=@O_SecPrice ");
                     strSql.Append(" WHERE ID=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@O_OrgResNo",entity.O_OrgResNo));
                paramList.Add(new SqlParameter("@O_OrderNo",entity.O_OrderNo));
                paramList.Add(new SqlParameter("@O_GoodsCode",entity.O_GoodsCode));
                paramList.Add(new SqlParameter("@O_GoodsName",entity.O_GoodsName));
                paramList.Add(new SqlParameter("@O_Unit",entity.O_Unit));
                paramList.Add(new SqlParameter("@O_Qty",entity.O_Qty));
                paramList.Add(new SqlParameter("@O_Batch",entity.O_Batch));
                paramList.Add(new SqlParameter("@O_Price",entity.O_Price));
                paramList.Add(new SqlParameter("@O_SecGoodsCode",entity.O_SecGoodsCode));
                paramList.Add(new SqlParameter("@O_SecGoodsName",entity.O_SecGoodsName));
                paramList.Add(new SqlParameter("@O_SecUnit",entity.O_SecUnit));
                paramList.Add(new SqlParameter("@O_SecQty",entity.O_SecQty));
                paramList.Add(new SqlParameter("@O_SecBatch",entity.O_SecBatch));
                paramList.Add(new SqlParameter("@O_SecPrice",entity.O_SecPrice));
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
