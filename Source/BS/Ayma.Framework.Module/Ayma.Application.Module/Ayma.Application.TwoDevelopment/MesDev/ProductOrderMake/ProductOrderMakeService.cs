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
    /// 日 期：2019-03-02 15:05
    /// 描 述：生成订单制作
    /// </summary>
    public partial class ProductOrderMakeService : RepositoryFactory
    {
        #region 获取数据
        /// <summary>
        /// 获取商品列表数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="keyword">商品编码/商品名称搜索</param>
        /// <returns></returns>
        public DataTable GetGoodsList(Pagination pagination, string queryJson, string keyword)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"
                                 SELECT t.I_Qty,t.I_Batch,t1.* FROM dbo.Mes_Inventory t
                                 LEFT JOIN dbo.Mes_Goods t1 ON t.I_GoodsCode=t1.G_Code");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["GoodsType"].IsEmpty())
                {
                    dp.Add("G_Kind", queryParam["GoodsType"].ToString(), DbType.String);
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
                return this.BaseRepository().FindTable(strSql.ToString(), dp, pagination);
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
        public IEnumerable<Mes_ProductOrderHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                       t.[ID]
                      ,t.[P_OrderNo]
                      ,t.[P_OrderDate]
                      ,t.[P_OrderStationID]
                      ,t.[P_OrderStationName]
                      ,t.[P_CreateBy]
                      ,t.[P_CreateDate]
                      ,t.[P_UpdateBy]
                      ,t.[P_UpdateDate]
                      ,t.[P_UseDate]
                      ,t.[P_Status]
               ");
                strSql.Append("  FROM Mes_ProductOrderHead t ");
                strSql.Append("  WHERE 1=1 and t.P_Status=3");
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
                if (!queryParam["P_OrderNo"].IsEmpty())
                {
                    dp.Add("P_OrderNo", "%" + queryParam["P_OrderNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_OrderNo Like @P_OrderNo ");
                }
                if (!queryParam["P_Status"].IsEmpty())
                {
                    dp.Add("P_Status", queryParam["P_Status"].ToString(), DbType.Int32);
                    strSql.Append(" AND t.P_Status = @P_Status ");
                }
                if (!queryParam["P_OrderStationName"].IsEmpty())
                {
                    dp.Add("P_OrderStationName", "%" + queryParam["P_OrderStationName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_OrderStationName like @P_OrderStationName ");
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
        /// 获取Mes_ProductOrderDetail表数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_ProductOrderDetailEntity> GetMes_ProductOrderDetailList(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_ProductOrderDetailEntity>(t=>t.P_OrderNo == keyValue );
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
        public void SaveEntity(string keyValue, Mes_ProductOrderHeadEntity entity,List<Mes_ProductOrderDetailEntity> mes_ProductOrderDetailList)
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
                    foreach (Mes_ProductOrderDetailEntity item in mes_ProductOrderDetailList)
                    {
                        item.Create();
                        item.P_OrderNo = mes_ProductOrderHeadEntityTmp.P_OrderNo;
                        db.Insert(item);
                    }
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                    foreach (Mes_ProductOrderDetailEntity item in mes_ProductOrderDetailList)
                    {
                        item.Create();
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

        /// <summary>
        /// 根据(订单号,keyValue)获取订单实体
        /// </summary>
        /// <returns></returns>
        public Mes_ProductOrderHeadEntity GetEntityByParam(string param)
        {
            return this.BaseRepository().FindEntity<Mes_ProductOrderHeadEntity>(c => c.P_OrderNo == param || c.ID == param);
        }

        #endregion

    }
}
