using Ayma.Application.TwoDevelopment.MesDev.ScrapManager;
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
    /// 日 期：2019-03-15 16:11
    /// 描 述：线边仓退料到仓库
    /// </summary>
    public partial class BackStockManagerService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_BackStockHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.B_Status,
                t.B_BackStockNo,
                t.B_StockCode,
                t.B_StockName,
                t.B_StockToCode,
                t.B_StockToName,
                t.B_Remark,
                t.B_CreateBy,
                t.B_Kind,
                t.B_CreateDate
                ");
                strSql.Append("  FROM Mes_BackStockHead t ");
                strSql.Append("  LEFT JOIN Mes_BackStockDetail t1 ON t1.B_BackStockNo = t.B_BackStockNo ");
                strSql.Append("  WHERE 1=1 and t.B_Status=3");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (queryParam["type"].IsEmpty())
                {
                    strSql.Append(" AND B_Status IN (1,2)  ");
                }
                if (!queryParam["B_BackStockNo"].IsEmpty())
                {
                    dp.Add("B_BackStockNo", queryParam["B_BackStockNo"].ToString(), DbType.String);
                    strSql.Append(" AND t.B_BackStockNo Like @B_BackStockNo ");
                }
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.B_CreateDate >= @startTime AND t.B_CreateDate <= @endTime ) ");
                }
                if (!queryParam["B_StockName"].IsEmpty())
                {
                    dp.Add("B_StockName", "%" + queryParam["B_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.B_StockCode Like @B_StockName ");
                }
                if (!queryParam["S_ScrapNo"].IsEmpty())
                {
                    dp.Add("S_ScrapNo", "%" + queryParam["S_ScrapNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.B_BackStockNo Like @S_ScrapNo ");
                }
                if (!queryParam["B_StockToName"].IsEmpty())
                {
                    dp.Add("B_StockToName", "%" + queryParam["B_StockToName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.B_StockToCode Like @B_StockToName ");
                }
                if (!queryParam["B_Status"].IsEmpty())
                {
                    dp.Add("B_Status",  queryParam["B_Status"].ToString() , DbType.String);
                    strSql.Append(" AND t.B_Status = @B_Status ");
                }
                return this.BaseRepository().FindList<Mes_BackStockHeadEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取仓库物料
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        public IEnumerable<GoodsEntity> GetGoodsList(Pagination obj, string stockCode, string keyword)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT t.ID G_ID, t.I_StockCode G_StockCode ,
                                t.I_StockName G_StockName ,
                                t.I_Batch G_Batch ,
                                t.I_GoodsCode G_GoodsCode,
                                t.I_GoodsName G_GoodsName,
                                t.I_Unit G_Unit ,
                                g.G_Price ,
                                t.I_Qty G_Qty 
                        FROM    dbo.Mes_Inventory t
                        LEFT JOIN dbo.Mes_Goods g ON t.I_GoodsCode = g.G_Code WHERE t.I_StockCode =@I_StockCode ");
            // 虚拟参数
            var dp = new DynamicParameters(new { });
            if (!keyword.IsEmpty())
            {
                dp.Add("keyword", "%" + keyword + "%", DbType.String);
                sb.Append(" AND I_GoodsCode+I_GoodsName like @keyword ");
            }
            dp.Add("@I_StockCode", stockCode, DbType.String);
            return this.BaseRepository().FindList<GoodsEntity>(sb.ToString(), dp, obj);
        }

        /// <summary>
        /// 获取Mes_BackStockHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_BackStockHeadEntity GetMes_BackStockHeadEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_BackStockHeadEntity>(keyValue);
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
        /// 获取Mes_BackStockDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public IEnumerable<Mes_BackStockDetailEntity> GetMes_BackStockDetailList(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_BackStockDetailEntity>(t=>t.B_BackStockNo == keyValue);
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
            var db = this.BaseRepository().BeginTrans();
            try
            {
                var mes_BackStockHeadEntity = GetMes_BackStockHeadEntity(keyValue); 
                db.Delete<Mes_BackStockHeadEntity>(t=>t.ID == keyValue);
                db.Delete<Mes_BackStockDetailEntity>(t=>t.B_BackStockNo == mes_BackStockHeadEntity.B_BackStockNo);
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
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
        public void SaveEntity(string keyValue, Mes_BackStockHeadEntity entity,List<Mes_BackStockDetailEntity >mes_BackStockDetailList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var mes_BackStockHeadEntityTmp = GetMes_BackStockHeadEntity(keyValue); 
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<Mes_BackStockDetailEntity>(t=>t.B_BackStockNo == mes_BackStockHeadEntityTmp.B_BackStockNo);
                    foreach (var item in mes_BackStockDetailList)
                    {
                        item.Create();
                        item.B_BackStockNo = mes_BackStockHeadEntityTmp.B_BackStockNo;
                    }
                    db.Insert(mes_BackStockDetailList);
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                    foreach (var item in mes_BackStockDetailList)
                    {
                        item.Create();
                        item.B_BackStockNo = entity.B_BackStockNo;
                    }
                    db.Insert(mes_BackStockDetailList);
                }
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
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
