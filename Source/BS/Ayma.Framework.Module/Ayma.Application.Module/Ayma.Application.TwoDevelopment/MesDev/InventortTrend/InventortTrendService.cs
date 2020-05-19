using Dapper;
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
    /// 日 期：2019-03-18 11:16
    /// 描 述：库存动态表查询
    /// </summary>
    public partial class InventortTrendService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_InventoryTrendEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.I_OrderKind,
                t.I_StockCode,
                t.I_StockName,
                t.I_GoodsCode,
                t.I_GoodsName,
                t.I_Unit,
                t.I_Batch,
                (select G_Period from Mes_Goods t where G_Code=I_GoodsCode) as I_Period,
                t.I_OrderNo,
                t.I_QtyOld,
                t.I_QtyNew,
                t.I_QtyTrend,
                t.I_CreateDate,
                t.I_Remark
                ");
                strSql.Append("  FROM Mes_InventoryTrend t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["I_OrderKind"].IsEmpty())
                {
                    dp.Add("I_OrderKind", "%" + queryParam["I_OrderKind"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_OrderKind Like @I_OrderKind ");
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
                strSql.Append(" order by I_CreateDate desc ");

                return this.BaseRepository().FindList<Mes_InventoryTrendEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_InventoryTrend表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_InventoryTrendEntity GetMes_InventoryTrendEntity(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.I_OrderKind,
                t.I_StockCode,
                t.I_StockName,
                t.I_GoodsCode,
                t.I_GoodsName,
                t.I_Unit,
                t.I_Batch,
                (select G_Period from Mes_Goods t where G_Code=I_GoodsCode) as I_Period,
                t.I_OrderNo,
                t.I_QtyOld,
                t.I_QtyNew,
                t.I_QtyTrend,
                t.I_Remark
                ");
                strSql.Append("  FROM Mes_InventoryTrend t ");
                strSql.Append("  WHERE 1=1 ");
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!keyValue.IsEmpty())
                {
                    dp.Add("keyValue", "%" + keyValue.ToString() + "%", DbType.String);
                    strSql.Append(" AND t.ID Like @keyValue ");
                }
                return this.BaseRepository().FindEntity<Mes_InventoryTrendEntity>(strSql.ToString(), dp);
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
                this.BaseRepository().Delete<Mes_InventoryTrendEntity>(t=>t.ID == keyValue);
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
        public void SaveEntity(string keyValue, Mes_InventoryTrendEntity entity)
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
