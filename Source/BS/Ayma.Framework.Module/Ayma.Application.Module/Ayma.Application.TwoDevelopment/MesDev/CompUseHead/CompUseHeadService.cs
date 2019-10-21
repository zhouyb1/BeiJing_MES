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
    /// 日 期：2019-07-04 16:14
    /// 描 述：强制使用记录单据
    /// </summary>
    public partial class CompUseHeadService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_CompUseHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.C_No,
                t.C_WorkShop,
                t.C_OrderNo,
                t.C_OrderDate,
                t.C_Status,
                t.C_CreateBy,
                t.C_Remark,
                t.C_StockName,
                t.C_StockCode,
                t.C_CreateDate,
                s.W_Name as C_WorkShopName
                ");
                strSql.Append("  FROM Mes_CompUseHead t left join Mes_WorkShop s on(t.C_WorkShop=s.W_Code) ");
                strSql.Append("  WHERE 1=1 AND t.C_Status in(1,2) ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.C_CreateDate >= @startTime AND t.C_CreateDate <= @endTime ) ");
                }
                if (!queryParam["C_OrderDate"].IsEmpty())
                {
                    dp.Add("C_OrderDate", "%" + queryParam["C_OrderDate"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_OrderDate Like @C_OrderDate ");
                }
                if (!queryParam["C_Status"].IsEmpty())
                {
                    dp.Add("C_Status",queryParam["C_Status"].ToString(), DbType.String);
                    strSql.Append(" AND t.C_Status = @C_Status ");
                }
                if (!queryParam["C_No"].IsEmpty())
                {
                    dp.Add("C_No", "%" + queryParam["C_No"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_No Like @C_No ");
                }
                if (!queryParam["C_WorkShopName"].IsEmpty())
                {
                    dp.Add("C_WorkShopName", "%" + queryParam["C_WorkShopName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND s.W_Name Like @C_WorkShopName ");
                }
                if (!queryParam["C_StockName"].IsEmpty())
                {
                    dp.Add("C_StockName", "%" + queryParam["C_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND s.C_StockName Like @C_StockName ");
                }
                return this.BaseRepository().FindList<Mes_CompUseHeadEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取强制使用记录单据查询页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_CompUseHeadEntity> CompUseHeadList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.C_No,
                t.C_WorkShop,
                t.C_OrderNo,
                t.C_OrderDate,
                t.C_Status,
                t.C_CreateBy,
                t.C_Remark,
                t.C_StockName,
                t.C_StockCode,
                t.C_CreateDate,
                s.W_Name as C_WorkShopName
                ");
                strSql.Append("  FROM Mes_CompUseHead t left join Mes_WorkShop s on(t.C_WorkShop=s.W_Code) ");
                strSql.Append("  WHERE 1=1 AND t.C_Status in(1,2)");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.C_CreateDate >= @startTime AND t.C_CreateDate <= @endTime ) ");
                }
                if (!queryParam["C_OrderDate"].IsEmpty())
                {
                    dp.Add("C_OrderDate", "%" + queryParam["C_OrderDate"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_OrderDate Like @C_OrderDate ");
                }
                if (!queryParam["C_Status"].IsEmpty())
                {
                    dp.Add("C_Status", queryParam["C_Status"].ToString(), DbType.String);
                    strSql.Append(" AND t.C_Status = @C_Status ");
                }
                if (!queryParam["C_No"].IsEmpty())
                {
                    dp.Add("C_No", "%" + queryParam["C_No"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_No Like @C_No ");
                }
                if (!queryParam["C_WorkShopName"].IsEmpty())
                {
                    dp.Add("C_WorkShopName", "%" + queryParam["C_WorkShopName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND s.W_Name Like @C_WorkShopName ");
                }
                if (!queryParam["C_StockName"].IsEmpty())
                {
                    dp.Add("C_StockName", "%" + queryParam["C_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND s.C_StockName Like @C_StockName ");
                }
                return this.BaseRepository().FindList<Mes_CompUseHeadEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取Mes_CompUseDetail表数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_CompUseDetailEntity> GetMes_CompUseDetailList(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_CompUseDetailEntity>(t=>t.C_No == keyValue );
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
        /// 获取Mes_CompUseHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_CompUseHeadEntity GetMes_CompUseHeadEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_CompUseHeadEntity>(keyValue);
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
        /// 获取Mes_CompUseDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_CompUseDetailEntity GetMes_CompUseDetailEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_CompUseDetailEntity>(t=>t.C_No == keyValue);
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
        /// 获取退供应商物料数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetGoodsList(Pagination pagination, string queryJson, string keyword, string stockCode)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.I_GoodsCode G_Code,
                t.I_GoodsName G_Name,
                t.I_Unit g_unit,
                t.I_Qty Qty,
                t.I_Batch Batch,
                b.G_Price
                ");
                strSql.Append("  FROM Mes_Inventory t LEFT JOIN Mes_Goods b ON t.I_GoodsCode=b.G_Code");
                strSql.Append("  WHERE 1=1 And t.I_StockCode=@I_StockCode And b.G_Kind=1");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters();
                dp.Add("I_StockCode", stockCode);
                if (!keyword.IsEmpty())
                {
                    dp.Add("keyword", "%" + keyword + "%", DbType.String);
                    strSql.Append(" AND I_GoodsCode+I_GoodsName like @keyword ");
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
                var mes_CompUseHeadEntity = GetMes_CompUseHeadEntity(keyValue); 
                db.Delete<Mes_CompUseHeadEntity>(t=>t.ID == keyValue);
                db.Delete<Mes_CompUseDetailEntity>(t=>t.C_No == mes_CompUseHeadEntity.C_No);
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
        public void SaveEntity(string keyValue, Mes_CompUseHeadEntity entity,List<Mes_CompUseDetailEntity> mes_CompUseDetailList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var mes_CompUseHeadEntityTmp = GetMes_CompUseHeadEntity(keyValue); 
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<Mes_CompUseDetailEntity>(t=>t.C_No == mes_CompUseHeadEntityTmp.C_No);
                    foreach (Mes_CompUseDetailEntity item in mes_CompUseDetailList)
                    {
                        item.Create();
                        item.C_No = mes_CompUseHeadEntityTmp.C_No;
                        db.Insert(item);
                    }
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                    foreach (Mes_CompUseDetailEntity item in mes_CompUseDetailList)
                    {
                        item.Create();
                        item.C_No = entity.C_No;
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
