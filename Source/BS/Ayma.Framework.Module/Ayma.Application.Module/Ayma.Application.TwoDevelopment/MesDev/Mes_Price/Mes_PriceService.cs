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
    /// 日 期：2019-12-17 12:37
    /// 描 述：变价记录表
    /// </summary>
    public partial class Mes_PriceService : RepositoryFactory
    {
        #region 构造函数和属性

        private string fieldSql;
        public Mes_PriceService()
        {
            fieldSql=@"
                t.ID,
                t.P_SupplyCode,
                t.P_SupplyName,
                t.P_GoodsCode,
                t.P_GoodsName,
                t.P_InPrice,
                t.P_CreateBy,
                t.P_CreateDate,
                t.P_Itax,
                t.P_StartDate,
                t.P_EndDate
            ";
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_PriceEntity> GetList( string queryJson )
        {
            try
            {
                //参考写法
                //var queryParam = queryJson.ToJObject();
                // 虚拟参数
                //var dp = new DynamicParameters(new { });
                //dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM Mes_Price t ");
                return this.BaseRepository().FindList<Mes_PriceEntity>(strSql.ToString());
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
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_PriceEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT 
                    t.P_SupplyCode,
                    t.P_SupplyName,
                    t.P_GoodsCode,
                    t.P_GoodsName
                    FROM Mes_Price t where 1=1");
                var queryParam = queryJson.ToJObject();
                var dp = new DynamicParameters(new { });
                // 虚拟参数
                if (!queryParam["P_SupplyName"].IsEmpty())
                {
                    dp.Add("P_SupplyName", "%" + queryParam["P_SupplyName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_SupplyName Like @P_SupplyName ");
                }
                if (!queryParam["P_GoodsName"].IsEmpty())
                {
                    dp.Add("P_GoodsName", "%" + queryParam["P_GoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_GoodsName Like @P_GoodsName");
                }
                strSql.Append(" group by t.P_SupplyCode, t.P_SupplyName,t.P_GoodsCode,t.P_GoodsName");
                return this.BaseRepository().FindList<Mes_PriceEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取供应商供应的物料变价数据
        /// <param name="pagination">分页参数</param>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_PriceEntity> GetPriceBySupply(Pagination pagination, string P_SupplyCode, string P_GoodsCode)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@" SELECT
                t.ID,
                t.P_SupplyCode,
                t.P_SupplyName,
                t.P_GoodsCode,
                t.P_GoodsName,
                t.P_InPrice,
                t.P_CreateBy,
                t.P_CreateDate,
                t.P_Itax,
                t.P_StartDate,
                t.P_EndDate
                FROM Mes_Price t where 1=1 and t.P_SupplyCode=" + P_SupplyCode + "and t.P_GoodsCode="+P_GoodsCode);
                return this.BaseRepository().FindList<Mes_PriceEntity>(strSql.ToString(), pagination);
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
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_PriceEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_PriceEntity>(keyValue);
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
                this.BaseRepository().Delete<Mes_PriceEntity>(t=>t.ID == keyValue);
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
        public void SaveEntity(string keyValue, Mes_PriceEntity entity)
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
