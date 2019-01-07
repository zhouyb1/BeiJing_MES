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
    /// 日 期：2019-01-07 17:54
    /// 描 述：查询生成清单
    /// </summary>
    public partial class Mes_ProductOrderHeadService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_ProductOrderHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.P_OrderNo,
                t.P_OrderStationID,
                t.P_OrderDate,
                t.P_OrderStationName,
                t.P_CreateBy,
                t.P_CreateDate,
                t.P_UpdateBy,
                t.P_UpdateDate,
                t1.P_GoodsCode,
                t1.P_GoodsName,
                t1.P_Unit,
                t1.P_Qty
                ");
                strSql.Append("  FROM Mes_ProductOrderHead t ");
                strSql.Append("  LEFT JOIN Mes_ProductOrderDetail t1 ON t1.P_OrderNo = t.P_OrderNo ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.P_OrderDate >= @startTime AND t.P_OrderDate <= @endTime ) ");
                }
                if (!queryParam["P_OrderDate"].IsEmpty())
                {
                    dp.Add("P_OrderDate", "%" + queryParam["P_OrderDate"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_OrderDate Like @P_OrderDate ");
                }
                return this.BaseRepository().FindList<Mes_ProductOrderHeadEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_ProductOrderHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_ProductOrderHeadEntity GetMes_ProductOrderHeadEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_ProductOrderHeadEntity>(keyValue);
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
        /// 获取Mes_ProductOrderDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_ProductOrderDetailEntity GetMes_ProductOrderDetailEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_ProductOrderDetailEntity>(t=>t.P_OrderNo == keyValue);
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
                var mes_ProductOrderHeadEntity = GetMes_ProductOrderHeadEntity(keyValue); 
                db.Delete<Mes_ProductOrderHeadEntity>(t=>t.ID == keyValue);
                db.Delete<Mes_ProductOrderDetailEntity>(t=>t.P_OrderNo == mes_ProductOrderHeadEntity.P_OrderNo);
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
        public void SaveEntity(string keyValue, Mes_ProductOrderHeadEntity entity,Mes_ProductOrderDetailEntity mes_ProductOrderDetailEntity)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var mes_ProductOrderHeadEntityTmp = GetMes_ProductOrderHeadEntity(keyValue); 
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<Mes_ProductOrderDetailEntity>(t=>t.P_OrderNo == mes_ProductOrderHeadEntityTmp.P_OrderNo);
                    mes_ProductOrderDetailEntity.Create();
                    mes_ProductOrderDetailEntity.P_OrderNo = mes_ProductOrderHeadEntityTmp.P_OrderNo;
                    db.Insert(mes_ProductOrderDetailEntity);
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                    mes_ProductOrderDetailEntity.Create();
                    mes_ProductOrderDetailEntity.P_OrderNo = entity.P_OrderNo;
                    db.Insert(mes_ProductOrderDetailEntity);
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
