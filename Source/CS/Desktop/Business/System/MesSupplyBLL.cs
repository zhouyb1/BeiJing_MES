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
    /// 描述: 业务层 -- Mes_Supply
    /// </summary>
    public partial class MesSupplyBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
		public List<MesSupplyEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_Supply");
                var rows = db.ExecuteObjects<MesSupplyEntity>(strSql.ToString());
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
        /// <returns>MesSupply</returns>
		public MesSupplyEntity GetEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_Supply");
                strSql.Append(" WHERE ID=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var rowData = db.ExecuteObject<MesSupplyEntity>(strSql.ToString(),paramList.ToArray());
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
				strSql.Append("DELETE Mes_Supply");
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
        public int SaveEntity(string keyValue,MesSupplyEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO Mes_Supply(");
                     strSql.Append("ID,");
                     strSql.Append("S_Code,");
                     strSql.Append("S_Name,");
                     strSql.Append("S_EffectTime,");
                     strSql.Append("S_CreateBy,");
                     strSql.Append("S_CreateDate,");
                     strSql.Append("S_UpdateBy,");
                     strSql.Append("S_UpdateDate,");
                     strSql.Append("S_Remark");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@ID,");
                     strSql.Append("@S_Code,");
                     strSql.Append("@S_Name,");
                     strSql.Append("@S_EffectTime,");
                     strSql.Append("@S_CreateBy,");
                     strSql.Append("@S_CreateDate,");
                     strSql.Append("@S_UpdateBy,");
                     strSql.Append("@S_UpdateDate,");
                     strSql.Append("@S_Remark");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@ID",Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE Mes_Supply SET ");
                     strSql.Append("S_Code=@S_Code,");
                     strSql.Append("S_Name=@S_Name,");
                     strSql.Append("S_EffectTime=@S_EffectTime,");
                     strSql.Append("S_CreateBy=@S_CreateBy,");
                     strSql.Append("S_CreateDate=@S_CreateDate,");
                     strSql.Append("S_UpdateBy=@S_UpdateBy,");
                     strSql.Append("S_UpdateDate=@S_UpdateDate,");
                     strSql.Append("S_Remark=@S_Remark ");
                     strSql.Append(" WHERE ID=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@S_Code",entity.S_Code));
                paramList.Add(new SqlParameter("@S_Name",entity.S_Name));
                paramList.Add(new SqlParameter("@S_EffectTime",entity.S_EffectTime));
                paramList.Add(new SqlParameter("@S_CreateBy",entity.S_CreateBy));
                paramList.Add(new SqlParameter("@S_CreateDate",entity.S_CreateDate));
                paramList.Add(new SqlParameter("@S_UpdateBy",entity.S_UpdateBy));
                paramList.Add(new SqlParameter("@S_UpdateDate",entity.S_UpdateDate));
                paramList.Add(new SqlParameter("@S_Remark",entity.S_Remark));
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
