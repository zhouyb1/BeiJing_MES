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
    /// 日 期：2019-03-14 11:20
    /// 描 述：报废单据管理
    /// </summary>
    public partial class ScrapManagerService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_ScrapHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.S_Status,
                t.S_ScrapNo,
                t.S_StockCode,
                t.S_StockName,
                t.S_OrderDate,
                t.S_Remark,
                dbo.GetUserNameById(t.S_CreateBy) S_CreateBy,
                t.S_CreateDate
                ");
                strSql.Append("  FROM Mes_ScrapHead t ");
                strSql.Append("  WHERE 1=1 AND t.S_Status in (1,2)");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.S_CreateDate >= @startTime AND t.S_CreateDate <= @endTime ) ");
                }
                if (!queryParam["S_ScrapNo"].IsEmpty())
                {
                    dp.Add("S_ScrapNo", "%" + queryParam["S_ScrapNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_ScrapNo Like @S_ScrapNo ");
                }
                if (!queryParam["S_StockName"].IsEmpty())
                {
                    dp.Add("S_StockName", "%" + queryParam["S_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_StockCode Like @S_StockName ");
                }
                if (!queryParam["S_Status"].IsEmpty())
                {
                    dp.Add("S_Status", "%" + queryParam["S_Status"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_Status Like @S_Status ");
                }
                return this.BaseRepository().FindList<Mes_ScrapHeadEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取报废单查询页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_ScrapHeadEntity> ScrapManagerList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.S_Status,
                t.S_ScrapNo,
                t.S_StockCode,
                t.S_StockName,
                t.S_OrderDate,
                t.S_Remark,
                dbo.GetUserNameById(t.S_CreateBy) S_CreateBy ,
                t.S_CreateDate
                ");
                strSql.Append("  FROM Mes_ScrapHead t ");
                strSql.Append("  WHERE 1=1 AND t.S_Status =3");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.S_CreateDate >= @startTime AND t.S_CreateDate <= @endTime ) ");
                }
                if (!queryParam["S_ScrapNo"].IsEmpty())
                {
                    dp.Add("S_ScrapNo", "%" + queryParam["S_ScrapNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_ScrapNo Like @S_ScrapNo ");
                }
                if (!queryParam["S_StockName"].IsEmpty())
                {
                    dp.Add("S_StockName", "%" + queryParam["S_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_StockCode Like @S_StockName ");
                }
                if (!queryParam["S_Status"].IsEmpty())
                {
                    dp.Add("S_Status", "%" + queryParam["S_Status"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_Status Like @S_Status ");
                }
                return this.BaseRepository().FindList<Mes_ScrapHeadEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取Mes_ScrapHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_ScrapHeadEntity GetMes_ScrapHeadEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_ScrapHeadEntity>(keyValue);
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
        /// 获取明细List
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IEnumerable<Mes_ScrapDetailEntity> GetMes_ScrapDeail(string orderNo)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_ScrapDetailEntity>(c=>c.S_ScrapNo==orderNo);
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
        public IEnumerable<GoodsEntity> GetGoodsList(Pagination obj,string stockCode,string keyword)
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
                        LEFT JOIN dbo.Mes_Goods g ON t.I_GoodsCode = g.G_Code WHERE t.I_Qty <> 0 and t.I_StockCode =@I_StockCode ");
            // 虚拟参数
            var dp = new DynamicParameters(new { });
            if (!keyword.IsEmpty())
            {
                dp.Add("keyword", "%" + keyword + "%", DbType.String);
                sb.Append(" AND I_GoodsCode+I_GoodsName like @keyword ");
            }
            dp.Add("@I_StockCode",stockCode,DbType.String);
            return this.BaseRepository().FindList<GoodsEntity>(sb.ToString(), dp,obj);
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
                this.BaseRepository().Delete<Mes_ScrapHeadEntity>(t=>t.ID == keyValue);
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
        public void SaveEntity(string keyValue, Mes_ScrapHeadEntity entity, List<Mes_ScrapDetailEntity> detailList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var headEntity = GetMes_ScrapHeadEntity(keyValue);
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<Mes_ScrapDetailEntity>(t => t.S_ScrapNo == headEntity.S_ScrapNo);
                    foreach (var item in detailList)
                    {
                        item.Create();
                        item.S_ScrapNo = headEntity.S_ScrapNo;
                    }
                    db.Insert(detailList);
                }
                else
                {
                    var dp = new DynamicParameters(new { });
                    dp.Add("@BillType", "报废单");
                    dp.Add("@Doucno", "", DbType.String, ParameterDirection.Output);
                    db.ExecuteByProc("sp_GetDoucno", dp);
                    var billNo = dp.Get<string>("@Doucno");//存储过程返回单号
                    entity.S_ScrapNo = billNo;
                    entity.Create();
                    db.Insert(entity);
                    foreach (var item in detailList)
                    {
                        item.Create();
                        item.S_ScrapNo = entity.S_ScrapNo;
                    }
                    db.Insert(detailList);
                }
                db.Commit();
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
