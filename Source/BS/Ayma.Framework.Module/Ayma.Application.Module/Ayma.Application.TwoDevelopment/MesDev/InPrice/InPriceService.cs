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
    /// 日 期：2019-08-06 10:54
    /// 描 述：原物料入库价格表
    /// </summary>
    public partial class InPriceService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_InPriceEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.P_SupplyCode,
                t.P_SupplyName,
                (select S_EffectTime from Mes_SuppLy where S_Code=t.P_SupplyCode) as S_EffectTime
                ");
                strSql.Append("  FROM Mes_InPrice t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                //if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                //{
                //    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                //    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                //    strSql.Append(" AND ( t.P_CreateDate >= @startTime AND t.P_CreateDate <= @endTime ) ");
                //}
                //if (!queryParam["P_CreateDate"].IsEmpty())
                //{
                //    dp.Add("P_CreateDate", "%" + queryParam["P_CreateDate"].ToString() + "%", DbType.String);
                //    strSql.Append(" AND t.P_CreateDate Like @P_CreateDate ");
                //}
                if (!queryParam["P_SupplyName"].IsEmpty())
                {
                    dp.Add("P_SupplyName", "%" + queryParam["P_SupplyName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_SupplyName Like @P_SupplyName ");
                }
                if (!queryParam["P_GoodsName"].IsEmpty())
                {
                    dp.Add("P_GoodsName", "%" + queryParam["P_GoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_GoodsName Like @P_GoodsName ");
                }
                strSql.Append(" group by t.P_SupplyCode,t.P_SupplyName ");
                return this.BaseRepository().FindList<Mes_InPriceEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取供应商供应的物料列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_InPriceEntity> GetPriceBySupply(Pagination pagination, string P_SupplyCode, string P_GoodsName)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.P_SupplyCode,
                t.P_SupplyName,
                t.P_GoodsCode,
                t.P_GoodsName,
                t.P_InPrice,
                t.P_Itax,
                t.P_StartDate,
                t.P_EndDate,
                t.P_TaxPrice
                ");
                strSql.Append("  FROM Mes_InPrice t ");
                strSql.Append("  WHERE 1=1 ");
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!P_SupplyCode.IsEmpty())
                {
                    dp.Add("P_SupplyCode", "%" + P_SupplyCode.ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_SupplyCode Like @P_SupplyCode ");
                }
                if (!P_GoodsName.IsEmpty())
                {
                    dp.Add("P_GoodsName", "%" + P_GoodsName.ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_GoodsName Like @P_GoodsName ");
                }
                return this.BaseRepository().FindList<Mes_InPriceEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取Mes_InPrice表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_InPriceEntity GetMes_InPriceEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_InPriceEntity>(keyValue);
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
        public void DeleteEntity(List<Mes_InPriceEntity>list )
        {
            try
            {
                this.BaseRepository().Delete(list);
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
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void DeleteEntity(string keyValue)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                db.Delete<Mes_InPriceEntity>(t => t.P_SupplyCode == keyValue||t.ID==keyValue);
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
        public void SaveEntity(string keyValue, Mes_InPriceEntity entity, Mes_PriceEntity entity2)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    entity2.Create();
                    this.BaseRepository().Update(entity);
                    this.BaseRepository().Insert(entity2);
                }
                else
                {
                    entity.Create();
                    entity2.Create();
                    this.BaseRepository().Insert(entity);
                    this.BaseRepository().Insert(entity2);
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
        /// <summary>
        /// 批量保存
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entityList"></param>
        /// <param name="entityList2"></param>
        public void SaveEntity( List<Mes_InPriceEntity> entityList, List<Mes_PriceEntity> entityList2)
        {
            try
            {
                foreach (var item in entityList)
                {
                    item.Modify(item.ID);
                }
                foreach (var item1 in entityList2)
                {
                    item1.Create();
                }
                this.BaseRepository().Update(entityList);
                this.BaseRepository().Insert(entityList2);
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
