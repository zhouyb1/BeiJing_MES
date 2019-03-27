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
    /// 描述: 业务层 -- Mes_ProductGoods
    /// </summary>
    public partial class MesProductGoodsBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
		public List<MesProductGoodsEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_ProductGoods");
                var rows = db.ExecuteObjects<MesProductGoodsEntity>(strSql.ToString());
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
        /// <returns>MesProductGoods</returns>
		public MesProductGoodsEntity GetEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_ProductGoods");
                strSql.Append(" WHERE ID=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var rowData = db.ExecuteObject<MesProductGoodsEntity>(strSql.ToString(),paramList.ToArray());
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
				strSql.Append("DELETE Mes_ProductGoods");
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
        public int SaveEntity(string keyValue,MesProductGoodsEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO Mes_ProductGoods(");
                     strSql.Append("ID,");
                     strSql.Append("G_Code,");
                     strSql.Append("G_Name,");
                     strSql.Append("G_Period,");
                     strSql.Append("G_Price,");
                     strSql.Append("G_Unit,");
                     strSql.Append("G_CreateBy,");
                     strSql.Append("G_CreateDate,");
                     strSql.Append("G_UpdateBy,");
                     strSql.Append("G_UpdateDate,");
                     strSql.Append("G_Remark");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@ID,");
                     strSql.Append("@G_Code,");
                     strSql.Append("@G_Name,");
                     strSql.Append("@G_Period,");
                     strSql.Append("@G_Price,");
                     strSql.Append("@G_Unit,");
                     strSql.Append("@G_CreateBy,");
                     strSql.Append("@G_CreateDate,");
                     strSql.Append("@G_UpdateBy,");
                     strSql.Append("@G_UpdateDate,");
                     strSql.Append("@G_Remark");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@ID",Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE Mes_ProductGoods SET ");
                     strSql.Append("G_Code=@G_Code,");
                     strSql.Append("G_Name=@G_Name,");
                     strSql.Append("G_Period=@G_Period,");
                     strSql.Append("G_Price=@G_Price,");
                     strSql.Append("G_Unit=@G_Unit,");
                     strSql.Append("G_CreateBy=@G_CreateBy,");
                     strSql.Append("G_CreateDate=@G_CreateDate,");
                     strSql.Append("G_UpdateBy=@G_UpdateBy,");
                     strSql.Append("G_UpdateDate=@G_UpdateDate,");
                     strSql.Append("G_Remark=@G_Remark ");
                     strSql.Append(" WHERE ID=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@G_Code",entity.G_Code));
                paramList.Add(new SqlParameter("@G_Name",entity.G_Name));
                paramList.Add(new SqlParameter("@G_Period",entity.G_Period));
                paramList.Add(new SqlParameter("@G_Price",entity.G_Price));
                paramList.Add(new SqlParameter("@G_Unit",entity.G_Unit));
                paramList.Add(new SqlParameter("@G_CreateBy",entity.G_CreateBy));
                paramList.Add(new SqlParameter("@G_CreateDate",entity.G_CreateDate));
                paramList.Add(new SqlParameter("@G_UpdateBy",entity.G_UpdateBy));
                paramList.Add(new SqlParameter("@G_UpdateDate",entity.G_UpdateDate));
                paramList.Add(new SqlParameter("@G_Remark",entity.G_Remark));
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
