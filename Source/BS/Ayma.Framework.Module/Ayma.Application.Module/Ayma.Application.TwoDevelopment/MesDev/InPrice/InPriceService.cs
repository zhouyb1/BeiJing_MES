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
                t.ID,
                t.P_SupplyCode,
                t.P_SupplyName,
                t.P_GoodsCode,
                t.P_GoodsName,
                t.P_InPrice,
                t.P_Itax,
                t.P_StartBatch,
                t.P_EndBatch,
                t.P_CreateBy,
                t.P_CreateDate
                ");
                strSql.Append("  FROM Mes_InPrice t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.P_CreateDate >= @startTime AND t.P_CreateDate <= @endTime ) ");
                }
                if (!queryParam["P_CreateDate"].IsEmpty())
                {
                    dp.Add("P_CreateDate", "%" + queryParam["P_CreateDate"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_CreateDate Like @P_CreateDate ");
                }
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
        public void DeleteEntity(string keyValue)
        {
            try
            {
                this.BaseRepository().Delete<Mes_InPriceEntity>(t=>t.ID == keyValue);
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
        public void SaveEntity(string keyValue, Mes_InPriceEntity entity)
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
