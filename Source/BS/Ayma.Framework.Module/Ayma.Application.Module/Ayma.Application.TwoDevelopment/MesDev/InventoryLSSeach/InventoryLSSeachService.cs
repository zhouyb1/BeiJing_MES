﻿using Dapper;
using Ayma.DataBase.Repository;
using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-08 17:27
    /// 描 述：历史库存查询
    /// </summary>
    public partial class InventoryLSSeachService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_InventoryLSEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.I_Date,
                t.I_StockCode,
                t.I_StockName,
                t.I_GoodsCode,
                t.I_GoodsName,
                t.I_Unit,
                t.I_OldQty,
                t.I_Qty,
                t.I_Batch
                ");
                strSql.Append("  FROM Mes_InventoryLS t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.I_Date >= @startTime AND t.I_Date <= @endTime ) ");
                }
                if (!queryParam["I_StockCode"].IsEmpty())
                {
                    dp.Add("I_StockCode", "%" + queryParam["I_StockCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_StockCode Like @I_StockCode ");
                }
                if (!queryParam["I_StockName"].IsEmpty())
                {
                    dp.Add("I_StockName", "%" + queryParam["I_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_StockName Like @I_StockName ");
                }
                if (!queryParam["I_GoodsCode"].IsEmpty())
                {
                    dp.Add("I_GoodsCode", "%" + queryParam["I_GoodsCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_GoodsCode Like @I_GoodsCode ");
                }
                if (!queryParam["I_GoodsName"].IsEmpty())
                {
                    dp.Add("I_GoodsName", "%" + queryParam["I_GoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_GoodsName Like @I_GoodsName ");
                }
                if (!queryParam["I_Batch"].IsEmpty())
                {
                    dp.Add("I_Batch", "%" + queryParam["I_Batch"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_Batch Like @I_Batch ");
                }
                return this.BaseRepository().FindList<Mes_InventoryLSEntity>(strSql.ToString(),dp, pagination);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取Mes_InventoryLS表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_InventoryLSEntity GetMes_InventoryLSEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_InventoryLSEntity>(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                this.BaseRepository().Delete<Mes_InventoryLSEntity>(t=>t.ID == keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, Mes_InventoryLSEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
                }
                else
                {
                    entity.Create();
                    this.BaseRepository().Insert(entity);
                }
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        #endregion

    }
}
