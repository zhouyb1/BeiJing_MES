﻿using System;
using System.Collections.Generic;
using System.Linq;
using Model.Dto;
using Model;
using DataAccess.SqlServer;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Business.System
{
    /// <summary>
    /// 描述: 业务层 -- Mes_Inventory
    /// </summary>
    public partial class MesInventoryBLL
    {
        SqlHelper db = new SqlHelper();
        
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
		public List<MesInventoryEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_Inventory");
                var rows = db.ExecuteObjects<MesInventoryEntity>(strSql.ToString());
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
        /// <returns>MesInventory</returns>
        public MesInventoryEntity GetValues(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT * FROM Mes_Inventory");
                strSql.Append(" WHERE I_StockCode=@StockCode");
                var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@StockCode", keyValue));
                var rowData = db.ExecuteObject<MesInventoryEntity>(strSql.ToString(), paramList.ToArray());
                return rowData;
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
        /// <returns>MesInventory</returns>
        public List<MesInventoryEntity> GetData(string Condit)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT * FROM Mes_Inventory ");
                strSql.Append(Condit);
                var rows = db.ExecuteObjects<MesInventoryEntity>(strSql.ToString());
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
        /// <returns>MesInventory</returns>
        public DataSet GetData2(string Condit)
        {
            try
            {
                var strSql = new StringBuilder();
                //strSql.Append("SELECT * FROM Mes_Inventory ");
                strSql.Append(Condit);
                DataSet ds = db.ExecuteDataSet(Condit);
                return ds;
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
        /// <returns>MesInventory</returns>
		public MesInventoryEntity GetEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
				strSql.Append("SELECT * FROM Mes_Inventory");
                strSql.Append(" WHERE ID=@ID");
				var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@ID",keyValue));
                var rowData = db.ExecuteObject<MesInventoryEntity>(strSql.ToString(),paramList.ToArray());
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
				strSql.Append("DELETE Mes_Inventory");
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
        public int SaveEntity(string keyValue,MesInventoryEntity entity)
        {
            try
            {
			    var strSql = new StringBuilder();
                var paramList = new List<SqlParameter>();
                if (string.IsNullOrEmpty(keyValue))
                {
					 strSql.Append("INSERT INTO Mes_Inventory(");
                     strSql.Append("ID,");
                     strSql.Append("I_StockCode,");
                     strSql.Append("I_StockName,");
                     strSql.Append("I_GoodsCode,");
                     strSql.Append("I_GoodsName,");
                     strSql.Append("I_Unit,");
                     strSql.Append("I_Qty,");
                     strSql.Append("I_Batch,");
                     strSql.Append("I_Period,");
                     strSql.Append("I_Remark");
                     strSql.Append(")");
                     strSql.Append("VALUES(");
                     strSql.Append("@ID,");
                     strSql.Append("@I_StockCode,");
                     strSql.Append("@I_StockName,");
                     strSql.Append("@I_GoodsCode,");
                     strSql.Append("@I_GoodsName,");
                     strSql.Append("@I_Unit,");
                     strSql.Append("@I_Qty,");
                     strSql.Append("@I_Batch,");
                     strSql.Append("@I_Period,");
                     strSql.Append("@I_Remark");
                     strSql.Append(")");
                     paramList.Add(new SqlParameter("@ID",Guid.NewGuid().ToString()));
                }
                else
                {
					 strSql.Append("UPDATE Mes_Inventory SET ");
                     strSql.Append("I_StockCode=@I_StockCode,");
                     strSql.Append("I_StockName=@I_StockName,");
                     strSql.Append("I_GoodsCode=@I_GoodsCode,");
                     strSql.Append("I_GoodsName=@I_GoodsName,");
                     strSql.Append("I_Unit=@I_Unit,");
                     strSql.Append("I_Qty=@I_Qty,");
                     strSql.Append("I_Batch=@I_Batch,");
                     strSql.Append("I_Period=@I_Period,");
                     strSql.Append("I_Remark=@I_Remark ");
                     strSql.Append(" WHERE ID=@ID");
                     paramList.Add(new SqlParameter("@ID",keyValue));
                }
                paramList.Add(new SqlParameter("@I_StockCode",entity.I_StockCode));
                paramList.Add(new SqlParameter("@I_StockName",entity.I_StockName));
                paramList.Add(new SqlParameter("@I_GoodsCode",entity.I_GoodsCode));
                paramList.Add(new SqlParameter("@I_GoodsName",entity.I_GoodsName));
                paramList.Add(new SqlParameter("@I_Unit",entity.I_Unit));
                paramList.Add(new SqlParameter("@I_Qty",entity.I_Qty));
                paramList.Add(new SqlParameter("@I_Batch",entity.I_Batch));
                paramList.Add(new SqlParameter("@I_Period",entity.I_Period));
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
