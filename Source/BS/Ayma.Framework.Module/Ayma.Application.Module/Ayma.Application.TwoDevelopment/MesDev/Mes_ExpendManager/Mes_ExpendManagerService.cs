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
    /// 日 期：2019-11-08 13:59
    /// 描 述：消耗物料
    /// </summary>
    public partial class Mes_ExpendManagerService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_ExpendHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                distinct
                t.ID,
                t.E_Status,
                t.E_ExpendNo,
                t.E_StockName,
                t.E_StockCode,
                t.MonthBalance,
                t.E_Remark,
                t.E_CreateDate,
                t.E_OrderDate,
                dbo.GetUserNameById(t.E_CreateBy) E_CreateBy,
                dbo.GetUserNameById(t.E_UpdateBy) E_UpdateBy,
                t.E_UpdateDate
                ");
                strSql.Append("  FROM Mes_ExpendHead t left join Mes_ExpendDetail s on(t.E_ExpendNo=s.E_ExpendNo)");
                strSql.Append("  WHERE 1=1 and E_Status in (1,2) ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.E_CreateDate >= @startTime AND t.E_CreateDate <= @endTime ) ");
                }
                if (!queryParam["OrderDate_S"].IsEmpty() && !queryParam["OrderDate_E"].IsEmpty())//新增单据时间
                {
                    dp.Add("OrderDate_S", queryParam["OrderDate_S"].ToDate(), DbType.DateTime);
                    dp.Add("OrderDate_E", queryParam["OrderDate_E"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.E_OrderDate >= @OrderDate_S AND t.E_OrderDate <= @OrderDate_E ) ");
                }
                if (!queryParam["M_GoodsName"].IsEmpty())
                {
                    dp.Add("M_GoodsName", "%" + queryParam["M_GoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND s.E_GoodsName Like @M_GoodsName ");
                }
               
                if (!queryParam["E_StockCode"].IsEmpty())
                {
                    dp.Add("E_StockCode", queryParam["E_StockCode"].ToString(), DbType.String);
                    strSql.Append(" AND t.E_StockCode = @E_StockCode ");
                }
                if (!queryParam["E_Status"].IsEmpty())
                {
                    dp.Add("E_Status", "%" + queryParam["E_Status"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.E_Status Like @E_Status ");
                }
                return this.BaseRepository().FindList<Mes_ExpendHeadEntity>(strSql.ToString(),dp, pagination);
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
        /// 报表：获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_ExpendHeadEntity> GetPostGoodsList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                distinct
                t.ID,
                t.E_Status,
                t.E_ExpendNo,
                t.E_StockName,
                t.E_StockCode,
                t.MonthBalance,
                t.E_Remark,
                t.E_CreateDate,
                dbo.GetUserNameById(t.E_CreateBy) E_CreateBy,
                dbo.GetUserNameById(t.E_UpdateBy) E_UpdateBy,
                t.E_UpdateDate,
                t.E_OrderDate,
                 dbo.GetUserNameById(t.E_UploadBy)  E_UploadBy,
                t.E_UploadDate
                ");
                strSql.Append("  FROM Mes_ExpendHead t left join Mes_ExpendDetail s on(t.E_ExpendNo=s.E_ExpendNo)");
                strSql.Append("  WHERE 1=1 and E_Status = 3 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.E_CreateDate >= @startTime AND t.E_CreateDate <= @endTime ) ");
                }
                if (!queryParam["OrderDate_S"].IsEmpty() && !queryParam["OrderDate_E"].IsEmpty())//新增单据时间
                {
                    dp.Add("OrderDate_S", queryParam["OrderDate_S"].ToDate(), DbType.DateTime);
                    dp.Add("OrderDate_E", queryParam["OrderDate_E"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.E_OrderDate >= @OrderDate_S AND t.E_OrderDate <= @OrderDate_E ) ");
                }
                if (!queryParam["M_GoodsName"].IsEmpty())
                {
                    dp.Add("M_GoodsName", "%" + queryParam["M_GoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND s.E_GoodsName Like @M_GoodsName ");
                }
                if (!queryParam["MonthBalance"].IsEmpty())
                {
                    dp.Add("MonthBalance", "%" + queryParam["MonthBalance"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.MonthBalance Like @MonthBalance ");
                }
                if (!queryParam["E_StockCode"].IsEmpty())
                {
                    dp.Add("E_StockCode", queryParam["E_StockCode"].ToString(), DbType.String);
                    strSql.Append(" AND t.E_StockCode = @E_StockCode ");
                }
                if (!queryParam["E_Status"].IsEmpty())
                {
                    dp.Add("E_Status", "%" + queryParam["E_Status"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.E_Status Like @E_Status ");
                }
                return this.BaseRepository().FindList<Mes_ExpendHeadEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取Mes_ExpendHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_ExpendHeadEntity GetMes_ExpendHeadEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_ExpendHeadEntity>(keyValue);
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
        /// 获取Mes_ExpendDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public IEnumerable<Mes_ExpendDetailEntity> GetMes_ExpendDetailEntity(string expendNo, string state)
        {
            try
            {
                var dp = new DynamicParameters(new { });
                var strSql = new StringBuilder();
                if (state.IsEmpty())
                {
                    strSql.Append(@"SELECT
                                   d.ID
                                  ,d.E_ExpendNo
                                  ,d.E_GoodsCode
                                  ,d.E_GoodsName
                                  ,d.E_Unit
                                  ,d.E_Qty
                                  ,d.E_Batch
                                  ,d.E_Remark
                                  ,dbo.GetPrice(d.E_GoodsCode,CONVERT(VARCHAR(6),h.E_UploadDate,112)) E_Price
                              FROM  dbo.Mes_ExpendHead h INNER JOIN dbo.Mes_ExpendDetail d ON d.E_ExpendNo =h.E_ExpendNo where h.E_ExpendNo =@E_ExpendNo");
                }
                else
                {
                    DateTime now = DateTime.Now;
                    //获取拼接形式的，精确到毫秒
                    string time = now.ToString("yyyyMM");
                    dp.Add("time", time, DbType.String);
                    strSql.Append(@"SELECT
                                   d.ID
                                  ,d.E_ExpendNo
                                  ,d.E_GoodsCode
                                  ,d.E_GoodsName
                                  ,d.E_Unit
                                  ,d.E_Qty
                                  ,d.E_Batch
                                  ,d.E_Remark
                                  ,dbo.GetPrice(d.E_GoodsCode,@time) E_Price
                              FROM  dbo.Mes_ExpendHead h INNER JOIN dbo.Mes_ExpendDetail d ON d.E_ExpendNo =h.E_ExpendNo where h.E_ExpendNo =@E_ExpendNo");
                }
                dp.Add("@E_ExpendNo", expendNo, DbType.String);
                var entity = this.BaseRepository().FindList<Mes_ExpendDetailEntity>(strSql.ToString(), dp);
                return entity;
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
                                   dbo.GetPrice(s.I_GoodsCode,@time) I_Price,
                                   s.I_Unit,
								   g.G_SupplyCode I_SupplyCode,
								   g.G_SupplyName I_SupplyName
                          FROM    dbo.Mes_Inventory s
                                LEFT JOIN dbo.Mes_Goods g ON g.G_Code = s.I_GoodsCode
                                WHERE s.I_Qty <> 0 AND S.I_Kind IS NOT NULL");
            var queryParam = queryJson.ToJObject();
            // 虚拟参数
            var dp = new DynamicParameters(new { });
            DateTime now = DateTime.Now;
            //获取拼接形式的，精确到毫秒
            string time = now.ToString("yyyyMM");
            dp.Add("time", time, DbType.String);
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
        /// 报表：单据详情
        /// </summary>
        /// <param name="expendNo"></param>
        /// <returns></returns>
        public IEnumerable<Mes_ExpendDetailEntity> GetDetail(string expendNo)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select d.E_GoodsCode,
                                d.E_GoodsName,
                                h.E_ExpendNo,
                                d.E_Batch,
                                d.E_Unit,
                                d.E_Qty,
                                d.E_Price,
                                h.MonthBalance 
                         from Mes_ExpendHead h 
                         left join Mes_ExpendDetail d on d.E_ExpendNo = h.E_ExpendNo
                         where h.E_ExpendNo = @E_ExpendNo ");
            var dp = new DynamicParameters(new {});
            dp.Add("@E_ExpendNo", expendNo, DbType.String);
            return this.BaseRepository().FindList<Mes_ExpendDetailEntity>(sql.ToString(), dp);
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
                var mes_ExpendHeadEntity = GetMes_ExpendHeadEntity(keyValue); 
                db.Delete<Mes_ExpendHeadEntity>(t=>t.ID == keyValue);
                db.Delete<Mes_ExpendDetailEntity>(t=>t.E_ExpendNo== mes_ExpendHeadEntity.E_ExpendNo);
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
        public void SaveEntity(string keyValue, Mes_ExpendHeadEntity entity,List<Mes_ExpendDetailEntity> detail)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var mes_ExpendHeadEntityTmp = GetMes_ExpendHeadEntity(keyValue); 
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<Mes_ExpendDetailEntity>(t=>t. E_ExpendNo== mes_ExpendHeadEntityTmp.E_ExpendNo);
                    foreach (var item in detail)
                    {
                        item.Create();
                        item.E_ExpendNo = mes_ExpendHeadEntityTmp.E_ExpendNo;
                    }
                    db.Insert(detail);
                }
                else
                {
                    var dp = new DynamicParameters(new { });
                    dp.Add("@BillType", "消耗单");
                    dp.Add("@Doucno", "", DbType.String, ParameterDirection.Output);
                    db.ExecuteByProc("sp_GetDoucno", dp);
                    var billNo = dp.Get<string>("@Doucno");//存储过程返回单号
                    entity.E_ExpendNo = billNo;
                    entity.Create();
                    db.Insert(entity);
                    foreach (var item in detail)
                    {
                        item.Create();
                        item.E_ExpendNo = entity.E_ExpendNo;
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
