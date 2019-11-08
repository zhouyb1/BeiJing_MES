using System.Security.Cryptography;
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
    /// 日 期：2019-11-07 10:38
    /// 描 述：原物料(半成品)销售
    /// </summary>
    public partial class Mes_SaleManagerService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_SaleHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                                t.ID,
                                t.S_Status,
                                t.S_SaleNo,
                                t.S_StockName,
                                t.S_StockCode,
                                t.S_CostomName,
                                t.S_CostomCode,
                                t.S_Remark,
                                t.S_CreateBy,
                                t.S_CreateDate,
                                t.S_UpdateBy,
                                t.S_UpdateDate,
                                t.MonthBalance
                                ");
                strSql.Append("  FROM Mes_SaleHead t ");
                strSql.Append("  WHERE 1=1 AND S_Status in (1,2) ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.S_CreateDate >= @startTime AND t.S_CreateDate <= @endTime ) ");
                }
                if (!queryParam["S_SaleNo"].IsEmpty())
                {
                    dp.Add("S_SaleNo", "%" + queryParam["S_SaleNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_SaleNo Like @S_SaleNo ");
                }
                if (!queryParam["S_Status"].IsEmpty())
                {
                    dp.Add("S_Status", "%" + queryParam["S_Status"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_Status Like @S_Status ");
                }
                if (!queryParam["S_StockCode"].IsEmpty())
                {
                    dp.Add("S_StockCode", queryParam["S_StockCode"].ToString(), DbType.String);
                    strSql.Append(" AND t.S_StockCode = @S_StockCode ");
                }
                if (!queryParam["S_StockCode"].IsEmpty())
                {
                    dp.Add("S_StockCode", "%" + queryParam["S_StockCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_StockCode Like @S_StockCode ");
                }
                if (!queryParam["MonthBalance"].IsEmpty())
                {
                    dp.Add("MonthBalance", "%" + queryParam["MonthBalance"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.MonthBalance Like @MonthBalance ");
                }
                return this.BaseRepository().FindList<Mes_SaleHeadEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_SaleHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_SaleHeadEntity GetMes_SaleHeadEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_SaleHeadEntity>(keyValue);
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
        /// 获取Mes_SaleDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public IEnumerable<Mes_SaleDetailEntity> GetMes_SaleDetail(string S_SaleNo)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_SaleDetailEntity>(t => t.S_SaleNo == S_SaleNo);
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
        /// 获取原物料
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        public IEnumerable<Mes_InventoryEntity> GetGoodsList(Pagination pagination, string queryJson, string keyword)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT s.ID,
                                   s.I_GoodsCode ,
                                   s.I_GoodsName ,
                                   s.I_Batch ,
                                   s.I_Qty ,
                                   s.I_Kind,
                                   g.G_Price I_Price,
                                   s.I_Unit,
								   g.G_SupplyCode I_SupplyCode,
								   g.G_SupplyName I_SupplyName
                          FROM    dbo.Mes_Inventory s
                                LEFT JOIN dbo.Mes_Goods g ON g.G_Code = s.I_GoodsCode
                                WHERE s.I_Qty <> 0 AND S.I_Kind IS NOT NULL");
            var queryParam = queryJson.ToJObject();
            // 虚拟参数
            var dp = new DynamicParameters(new { });
            if (!keyword.IsEmpty())
            {
                dp.Add("keyword", "%" + keyword + "%", DbType.String);
                strSql.Append(" AND  S.I_GoodsCode + S.I_GoodsName like @keyword ");
            }
            if (!queryParam.IsEmpty())
            {
                dp.Add("stockCode", queryParam["stockCode"].ToString(), DbType.String);
                strSql.Append(" AND S.I_StockCode =@stockCode ");
            }
            return this.BaseRepository().FindList<Mes_InventoryEntity>(strSql.ToString(), dp, pagination);
        }


        /// <summary>
        /// 获取已提交的原物料销售单据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<Mes_SaleHeadEntity> GetPostList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                                t.ID,
                                t.S_Status,
                                t.S_SaleNo,
                                t.S_StockName,
                                t.S_StockCode,
                                t.S_CostomName,
                                t.S_CostomCode,
                                t.S_Remark,
                                t.S_CreateBy,
                                t.S_CreateDate,
                                t.S_UpdateBy,
                                t.S_UpdateDate,
                                t.MonthBalance
                                ");
                strSql.Append("  FROM Mes_SaleHead t ");
                strSql.Append("  WHERE 1=1 AND S_Status = 3 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.S_CreateDate >= @startTime AND t.S_CreateDate <= @endTime ) ");
                }
                if (!queryParam["S_SaleNo"].IsEmpty())
                {
                    dp.Add("S_SaleNo", "%" + queryParam["S_SaleNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_SaleNo Like @S_SaleNo ");
                }
                if (!queryParam["S_StockCode"].IsEmpty())
                {
                    dp.Add("S_StockCode", queryParam["S_StockCode"].ToString(), DbType.String);
                    strSql.Append(" AND t.S_StockCode = @S_StockCode ");
                }
                if (!queryParam["S_CostomCode"].IsEmpty())
                {
                    dp.Add("S_CostomCode", "%" + queryParam["S_CostomCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_CostomCode Like @S_CostomCode ");
                }
                if (!queryParam["MonthBalance"].IsEmpty())
                {
                    dp.Add("MonthBalance", "%" + queryParam["MonthBalance"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.MonthBalance Like @MonthBalance ");
                }
                return this.BaseRepository().FindList<Mes_SaleHeadEntity>(strSql.ToString(), dp, pagination);
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
        /// 报表：原物料销售单据详情
        /// </summary>
        /// <param name="saleNo"></param>
        /// <returns></returns>
        public IEnumerable<Mes_SaleDetailEntity> GetDetailList(string saleNo)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT  b.S_GoodsName ,
                                b.S_GoodsCode ,
                                b.S_Batch ,
                                S_Unit ,
                                b.S_Qty ,
                                b.S_Price
                        FROM    dbo.Mes_SaleHead h
                                LEFT JOIN dbo.Mes_SaleDetail b ON b.S_SaleNo = h.S_SaleNo WHERE  h.S_SaleNo =@saleNo");
            var dp = new DynamicParameters(new {});
            dp.Add("@saleNo",saleNo,DbType.String);
            return this.BaseRepository().FindList<Mes_SaleDetailEntity>(sb.ToString(), dp);
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
                var mes_SaleHeadEntity = GetMes_SaleHeadEntity(keyValue); 
                db.Delete<Mes_SaleHeadEntity>(t=>t.ID == keyValue);
                db.Delete<Mes_SaleDetailEntity>(t => t.S_SaleNo == mes_SaleHeadEntity.S_SaleNo);
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
        public void SaveEntity(string keyValue, Mes_SaleHeadEntity entity, List<Mes_SaleDetailEntity> detail)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var mes_SaleHeadEntityTmp = GetMes_SaleHeadEntity(keyValue); 
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<Mes_SaleDetailEntity>(t=>t.S_SaleNo== mes_SaleHeadEntityTmp.S_SaleNo);
                    foreach (var item in detail)
                    {
                        item.Create();
                        item.S_SaleNo = mes_SaleHeadEntityTmp.S_SaleNo;
                    }
                    db.Insert(detail);
                }
                else
                {
                    var dp = new DynamicParameters(new { });
                    dp.Add("@BillType", "原物料销售单");
                    dp.Add("@Doucno", "", DbType.String, ParameterDirection.Output);
                    db.ExecuteByProc("sp_GetDoucno", dp);
                    var billNo = dp.Get<string>("@Doucno");//存储过程返回单号
                    entity.S_SaleNo = billNo;
                    entity.Create();
                    db.Insert(entity);
                    foreach (var item in detail)
                    {
                        item.Create();
                        item.S_SaleNo = entity.S_SaleNo;
                    }
                    db.Insert(detail);
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
