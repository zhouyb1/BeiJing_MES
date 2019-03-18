﻿using Ayma.Application.TwoDevelopment.MesDev.ScrapManager;
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
    /// 日 期：2019-03-18 15:14
    /// 描 述：组装与拆分单据制作
    /// </summary>
    public partial class OrgResMangerService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_OrgResHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.O_Status,
                t.O_OrgResNo,
                t.O_OrderNo,
                t.O_OrderDate,
                t.O_Record,
                t.O_ProCode,
                t.O_WorkShopCode,
                t.O_WorkShopName,
                t.O_Remark,
                t.O_CreateBy,
                t.O_CreateDate,
                t1.O_OrgResNo
                ");
                strSql.Append("  FROM Mes_OrgResHead t ");
                strSql.Append("  LEFT JOIN Mes_OrgResDetail t1 ON t1.O_OrgResNo = t.O_OrgResNo ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.O_OrderDate >= @startTime AND t.O_OrderDate <= @endTime ) ");
                }
                if (!queryParam["O_OrderNo"].IsEmpty())
                {
                    dp.Add("O_OrderNo", "%" + queryParam["O_OrderNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_OrderNo Like @O_OrderNo ");
                }
                if (!queryParam["O_OrgResNo"].IsEmpty())
                {
                    dp.Add("O_OrgResNo", "%" + queryParam["O_OrgResNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t1.O_OrgResNo Like @O_OrgResNo ");
                }
                if (!queryParam["O_WorkShopName"].IsEmpty())
                {
                    dp.Add("O_WorkShopName", "%" + queryParam["O_WorkShopName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_WorkShopName Like @O_WorkShopName ");
                }
                return this.BaseRepository().FindList<Mes_OrgResHeadEntity>(strSql.ToString(),dp, pagination);
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
        public IEnumerable<GoodsEntity> GetGoodsList(Pagination obj, string keyword)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT t.ID G_ID, t.I_StockCode G_StockCode ,
                                t.I_StockName G_StockName ,
                                t.I_Batch G_Batch ,
                                I_GoodsCode G_GoodsCode,
                                I_GoodsName G_GoodsName,
                                t.I_Unit G_Unit ,
                                g.G_Price ,
                                t.I_Qty G_Qty 
                        FROM    dbo.Mes_Inventory t
                        LEFT JOIN dbo.Mes_Goods g ON t.I_GoodsCode = g.G_Code WHERE 1=1 ");
            // 虚拟参数
            var dp = new DynamicParameters(new { });
            if (!keyword.IsEmpty())
            {
                dp.Add("keyword", "%" + keyword + "%", DbType.String);
                sb.Append(" AND P_GoodsCode+P_GoodsName like @keyword ");
            }
            return this.BaseRepository().FindList<GoodsEntity>(sb.ToString(), dp, obj);
        }
        /// <summary>
        /// 获取Mes_OrgResHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_OrgResHeadEntity GetMes_OrgResHeadEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_OrgResHeadEntity>(keyValue);
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
        /// 获取Mes_OrgResDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public IEnumerable<Mes_OrgResDetailEntity> GetMes_OrgResDetailList(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_OrgResDetailEntity>(t=>t.O_OrgResNo == keyValue);
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
                var mes_OrgResHeadEntity = GetMes_OrgResHeadEntity(keyValue); 
                db.Delete<Mes_OrgResHeadEntity>(t=>t.ID == keyValue);
                db.Delete<Mes_OrgResDetailEntity>(t=>t.O_OrgResNo == mes_OrgResHeadEntity.O_OrgResNo);
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
        public void SaveEntity(string keyValue, Mes_OrgResHeadEntity entity,List<Mes_OrgResDetailEntity> mes_OrgResDetailList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var mes_OrgResHeadEntityTmp = GetMes_OrgResHeadEntity(keyValue); 
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<Mes_OrgResDetailEntity>(t=>t.O_OrgResNo == mes_OrgResHeadEntityTmp.O_OrgResNo);
                    foreach (var item in mes_OrgResDetailList)
                    {
                        item.Create();
                        item.O_OrgResNo = mes_OrgResHeadEntityTmp.O_OrgResNo;
                    }
                    db.Insert(mes_OrgResDetailList);
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                    foreach (var item in mes_OrgResDetailList)
                    {
                        item.Create();
                        item.O_OrgResNo = item.O_OrgResNo;
                    }
                    db.Insert(mes_OrgResDetailList);
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