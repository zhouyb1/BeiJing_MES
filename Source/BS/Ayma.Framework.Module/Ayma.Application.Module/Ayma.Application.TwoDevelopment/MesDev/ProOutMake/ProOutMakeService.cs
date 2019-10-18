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
    /// 日 期：2019-03-02 17:05
    /// 描 述：成品出库单制作
    /// </summary>
    public partial class ProOutMakeService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_ProOutHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                               t.[ID]
                              ,t.[P_ProOutNo]
                              ,t.[P_StockCode]
                              ,t.[P_StockName]
                              ,t.[P_OrderNo]
                              ,t.[P_OrderDate]
                              ,t.[P_Status]
                              ,t.[P_CreateBy]
                              ,t.[P_CreateDate]
                              ,t.[P_UpdateBy]
                              ,t.[P_UpdateDate]
                              ,t.[P_Remark]
                              ,t.[P_DeleteBy]
                              ,t.[P_DeleteDate]
                              ,t.[P_UploadBy]
                              ,t.[P_UploadDate]");
                strSql.Append("  FROM Mes_ProOutHead t ");
                strSql.Append("  WHERE (t.P_Status=1 or t.P_Status=2) ");
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
                    dp.Add("P_OrderDate",queryParam["P_OrderDate"].ToString(), DbType.String);
                    strSql.Append(" AND t.P_OrderDate = @P_OrderDate ");
                }
                if (!queryParam["P_ProOutNo"].IsEmpty())
                {
                    dp.Add("P_ProOutNo", "%" + queryParam["P_ProOutNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_ProOutNo Like @P_ProOutNo ");
                }
                if (!queryParam["P_Status"].IsEmpty())
                {
                    dp.Add("P_Status",queryParam["P_Status"].ToString(), DbType.String);
                    strSql.Append(" AND t.P_Status = @P_Status ");
                }
                if (!queryParam["P_StockName"].IsEmpty())
                {
                    dp.Add("P_StockName", queryParam["P_StockName"].ToString(), DbType.String);
                    strSql.Append(" AND t.P_StockName = @P_StockName ");
                }
                return this.BaseRepository().FindList<Mes_ProOutHeadEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取页面显示列表数据 单据完成
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_ProOutHeadEntity> GetSearchPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                               t.[ID]
                              ,t.[P_ProOutNo]
                              ,t.[P_StockCode]
                              ,t.[P_StockName]
                              ,t.[P_OrderNo]
                              ,t.[P_OrderDate]
                              ,t.[P_Status]
                              ,t.[P_CreateBy]
                              ,t.[P_CreateDate]
                              ,t.[P_UpdateBy]
                              ,t.[P_UpdateDate]
                              ,t.[P_Remark]
                              ,t.[P_DeleteBy]
                              ,t.[P_DeleteDate]
                              ,t.[P_UploadBy]
                              ,t.[P_UploadDate]");
                strSql.Append("  FROM Mes_ProOutHead t ");
                strSql.Append("  WHERE t.P_Status=3 ");
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
                    dp.Add("P_OrderDate",queryParam["P_OrderDate"].ToString(), DbType.String);
                    strSql.Append(" AND t.P_OrderDate = @P_OrderDate ");
                }
                if (!queryParam["P_ProOutNo"].IsEmpty())
                {
                    dp.Add("P_ProOutNo", "%" + queryParam["P_ProOutNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_ProOutNo Like @P_ProOutNo ");
                }
                if (!queryParam["P_Status"].IsEmpty())
                {
                    dp.Add("P_Status",queryParam["P_Status"].ToString(), DbType.String);
                    strSql.Append(" AND t.P_Status = @P_Status ");
                }
                if (!queryParam["P_StockName"].IsEmpty())
                {
                    dp.Add("P_StockName", queryParam["P_StockName"].ToString(), DbType.String);
                    strSql.Append(" AND t.P_StockName = @P_StockName ");
                }
                return this.BaseRepository().FindList<Mes_ProOutHeadEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_ProOutDetail表数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_ProOutDetailEntity> GetMes_ProOutDetailList(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_ProOutDetailEntity>(t=>t.P_ProOutNo == keyValue );
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
        /// 获取Mes_ProOutHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_ProOutHeadEntity GetMes_ProOutHeadEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_ProOutHeadEntity>(keyValue);
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
        /// 获取Mes_ProOutDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_ProOutDetailEntity GetMes_ProOutDetailEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_ProOutDetailEntity>(t=>t.P_ProOutNo == keyValue);
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
        /// 获取仓库成品物料列表
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        public IEnumerable<Mes_InventoryEntity> GetInventoryProMaterList(Pagination paginationobj, string stockCode)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"select m.*,g.G_Price as I_Price from Mes_Inventory m left join Mes_Goods g on m.I_GoodsCode = g.G_Code where m.I_StockCode =@stockCode and g.G_Kind=3");
                var dp = new DynamicParameters(new { });
                dp.Add("@stockCode", stockCode, DbType.String);

                return this.BaseRepository().FindList<Mes_InventoryEntity>(strSql.ToString(), dp, paginationobj);
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
                var mes_ProOutHeadEntity = GetMes_ProOutHeadEntity(keyValue); 
                db.Delete<Mes_ProOutHeadEntity>(t=>t.ID == keyValue);
                db.Delete<Mes_ProOutDetailEntity>(t=>t.P_ProOutNo == mes_ProOutHeadEntity.P_ProOutNo);
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
        public void SaveEntity(string keyValue, Mes_ProOutHeadEntity entity,List<Mes_ProOutDetailEntity> mes_ProOutDetailList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var mes_ProOutHeadEntityTmp = GetMes_ProOutHeadEntity(keyValue); 
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<Mes_ProOutDetailEntity>(t=>t.P_ProOutNo == mes_ProOutHeadEntityTmp.P_ProOutNo);
                    foreach (Mes_ProOutDetailEntity item in mes_ProOutDetailList)
                    {
                        item.Create();
                        item.P_ProOutNo = mes_ProOutHeadEntityTmp.P_ProOutNo;
                        db.Insert(item);
                    }
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                    foreach (Mes_ProOutDetailEntity item in mes_ProOutDetailList)
                    {
                        item.Create();
                        item.P_ProOutNo = entity.P_ProOutNo;
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
