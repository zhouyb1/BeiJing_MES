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
    /// 日 期：2019-01-09 10:20
    /// 描 述：调拨单制作
    /// </summary>
    public partial class RequistBillService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取商品列表数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="stockCode">仓库编码</param>
        /// <param name="keyword">商品编码/商品名称搜索</param>
        /// <returns></returns>
        public DataTable GetList(Pagination pagination, string queryJson, string stockCode, string keyword)
        {
            try
            {
                var strSql=new StringBuilder();
                strSql.Append(@"
                                 SELECT t.I_Qty,t.I_Batch,t1.* FROM dbo.Mes_Inventory t
                                 LEFT JOIN dbo.Mes_Goods t1 ON t.I_GoodsCode=t1.G_Code");
                strSql.Append("  where I_StockCode='"+stockCode+"'");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["GoodsType"].IsEmpty())
                {
                    dp.Add("G_Kind", queryParam["GoodsType"].ToString(),DbType.String);
                    strSql.Append(" And t1.G_Kind=@G_Kind");
                }
                if (!queryParam["SupplyCode"].IsEmpty())
                {
                    dp.Add("SupplyCode", "%" + queryParam["SupplyCode"].ToString() + "%", DbType.String);
                    strSql.Append(" And t1.G_SupplyCode like @SupplyCode");
                }
                if (!queryParam["SupplyName"].IsEmpty())
                {
                    dp.Add("SupplyName", "%" + queryParam["SupplyName"].ToString() + "%", DbType.String);
                    strSql.Append(" And t1.G_Supply like @SupplyName");
                }
                if (!queryParam["Batch"].IsEmpty())
                {
                    dp.Add("Batch", "%" + queryParam["Batch"].ToString() + "%", DbType.String);
                    strSql.Append(" And t.I_Batch like @Batch");
                }
                if (!keyword.IsEmpty())
                {
                    dp.Add("keyword", "%" + keyword + "%", DbType.String);
                    strSql.Append(" And t.I_GoodsCode+t.I_GoodsName like @keyword");
                }
                return this.BaseRepository().FindTable(strSql.ToString(),dp,pagination);
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
        /// 获取已提交单据列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_RequistHeadEntity> GetPostPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.R_RequistNo,
                t.R_StockCode,
                t.R_StockName,
                t.R_StockToCode,
                t.R_StockToName,
                t.P_OrderNo,
                t.P_OrderDate,
                t.R_Status,
                t.R_CreateBy,
                t.R_CreateDate,
                t.R_UpdateBy,
                t.R_UpdateDate,
                t.R_Remark,
                t.R_DeleteBy,
                t.R_DeleteDate,
                t.R_UploadBy,
                t.R_UploadDate
                ");
                strSql.Append("  FROM Mes_RequistHead t ");
                strSql.Append("  WHERE t.R_Status != -1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.P_OrderDate >= @startTime AND t.P_OrderDate <= @endTime ) ");
                }
                if (!queryParam["R_RequistNo"].IsEmpty())
                {
                    dp.Add("R_RequistNo", "%" + queryParam["R_RequistNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.R_RequistNo Like @R_RequistNo ");
                }
                if (!queryParam["R_StockCode"].IsEmpty())
                {
                    dp.Add("R_StockCode", "%" + queryParam["R_StockCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.R_StockCode Like @R_StockCode ");
                }
                if (!queryParam["R_StockName"].IsEmpty())
                {
                    dp.Add("R_StockName", "%" + queryParam["R_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.R_StockName Like @R_StockName ");
                }
                if (!queryParam["R_StockToCode"].IsEmpty())
                {
                    dp.Add("R_StockToCode", "%" + queryParam["R_StockToCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.R_StockToCode Like @R_StockToCode ");
                }
                if (!queryParam["R_StockToName"].IsEmpty())
                {
                    dp.Add("R_StockToName", "%" + queryParam["R_StockToName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.R_StockToName Like @R_StockToName ");
                }
                if (!queryParam["P_OrderNo"].IsEmpty())
                {
                    dp.Add("P_OrderNo", "%" + queryParam["P_OrderNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_OrderNo Like @P_OrderNo ");
                }
                return this.BaseRepository().FindList<Mes_RequistHeadEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_RequistHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.R_RequistNo,
                t.R_StockCode,
                t.R_StockName,
                t.R_StockToCode,
                t.R_StockToName,
                t.P_OrderNo,
                t.P_OrderDate,
                t.R_Status,
                t.R_CreateBy,
                t.R_CreateDate,
                t.R_UpdateBy,
                t.R_UpdateDate,
                t.R_Remark,
                t.R_DeleteBy,
                t.R_DeleteDate,
                t.R_UploadBy,
                t.R_UploadDate
                ");
                strSql.Append("  FROM Mes_RequistHead t ");
                strSql.Append("  WHERE t.R_Status in(1,2) ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.P_OrderDate >= @startTime AND t.P_OrderDate <= @endTime ) ");
                }
                if (!queryParam["R_RequistNo"].IsEmpty())
                {
                    dp.Add("R_RequistNo", "%" + queryParam["R_RequistNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.R_RequistNo Like @R_RequistNo ");
                }
                if (!queryParam["R_StockCode"].IsEmpty())
                {
                    dp.Add("R_StockCode", "%" + queryParam["R_StockCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.R_StockCode Like @R_StockCode ");
                }
                if (!queryParam["R_StockName"].IsEmpty())
                {
                    dp.Add("R_StockName", "%" + queryParam["R_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.R_StockName Like @R_StockName ");
                }
                if (!queryParam["R_StockToCode"].IsEmpty())
                {
                    dp.Add("R_StockToCode", "%" + queryParam["R_StockToCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.R_StockToCode Like @R_StockToCode ");
                }
                if (!queryParam["R_StockToName"].IsEmpty())
                {
                    dp.Add("R_StockToName", "%" + queryParam["R_StockToName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.R_StockToName Like @R_StockToName ");
                }
                if (!queryParam["P_OrderNo"].IsEmpty())
                {
                    dp.Add("P_OrderNo", "%" + queryParam["P_OrderNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_OrderNo Like @P_OrderNo ");
                }
                return this.BaseRepository().FindList<Mes_RequistHeadEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_RequistDetail表数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_RequistDetailEntity> GetMes_RequistDetailList(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_RequistDetailEntity>(t=>t.R_RequistNo == keyValue );
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
        /// 获取Mes_RequistHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_RequistHeadEntity GetMes_RequistHeadEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_RequistHeadEntity>(keyValue);
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
        /// 获取Mes_RequistDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_RequistDetailEntity GetMes_RequistDetailEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_RequistDetailEntity>(t=>t.R_RequistNo == keyValue);
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
                var mes_RequistHeadEntity = GetMes_RequistHeadEntity(keyValue); 
                db.Delete<Mes_RequistHeadEntity>(t=>t.ID == keyValue);
                db.Delete<Mes_RequistDetailEntity>(t=>t.R_RequistNo == mes_RequistHeadEntity.R_RequistNo);
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
        public void SaveEntity(string keyValue, Mes_RequistHeadEntity entity,List<Mes_RequistDetailEntity> mes_RequistDetailList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var mes_RequistHeadEntityTmp = GetMes_RequistHeadEntity(keyValue); 
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<Mes_RequistDetailEntity>(t=>t.R_RequistNo == mes_RequistHeadEntityTmp.R_RequistNo);
                    foreach (Mes_RequistDetailEntity item in mes_RequistDetailList)
                    {
                        item.Create();
                        item.R_RequistNo = mes_RequistHeadEntityTmp.R_RequistNo;
                        item.P_OrderNo = entity.P_OrderNo;
                        db.Insert(item);
                    }
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                    foreach (Mes_RequistDetailEntity item in mes_RequistDetailList)
                    {
                        item.Create();
                        item.R_RequistNo = entity.R_RequistNo;
                        item.P_OrderNo = entity.P_OrderNo;
                        db.Insert(item);
                    }
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