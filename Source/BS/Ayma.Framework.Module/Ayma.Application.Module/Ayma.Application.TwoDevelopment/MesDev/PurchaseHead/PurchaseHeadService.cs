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
    /// 日 期：2019-03-05 11:20
    /// 描 述：采购单制作及查询
    /// </summary>
    public partial class PurchaseHeadService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_PurchaseHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.P_PurchaseNo,
                t.P_StockCode,
                t.P_StockName,
                t.P_SupplyCode,
                t.P_SupplyName,
                t.P_OrderNo,
                t.P_OrderDate,
                t.P_Status,
                t.P_CreateBy,
                t.P_CreateDate,
                t.P_UpdateBy,
                t.P_UpdateDate,
                t.P_Remark,
                t.P_UploadBy,
                t.P_UploadDate
                ");
                strSql.Append("  FROM Mes_PurchaseHead t ");
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
                if (!queryParam["P_PurchaseNo"].IsEmpty())
                {
                    dp.Add("P_PurchaseNo", "%" + queryParam["P_PurchaseNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_PurchaseNo Like @P_PurchaseNo ");
                }
                if (!queryParam["P_Status"].IsEmpty())
                {
                    dp.Add("P_Status",queryParam["P_Status"].ToString(), DbType.String);
                    strSql.Append(" AND t.P_Status = @P_Status ");
                }
                if (!queryParam["P_OrderNo"].IsEmpty())
                {
                    dp.Add("P_OrderNo", "%" + queryParam["P_OrderNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_OrderNo Like @P_OrderNo ");
                }
                return this.BaseRepository().FindList<Mes_PurchaseHeadEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_PurchaseDetail表数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_PurchaseDetailEntity> GetMes_PurchaseDetailList(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_PurchaseDetailEntity>(t=>t.P_PurchaseNo == keyValue );
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
        /// 获取Mes_PurchaseHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_PurchaseHeadEntity GetMes_PurchaseHeadEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_PurchaseHeadEntity>(keyValue);
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
        /// 获取Mes_PurchaseDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_PurchaseDetailEntity GetMes_PurchaseDetailEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_PurchaseDetailEntity>(t=>t.P_PurchaseNo == keyValue);
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
                var mes_PurchaseHeadEntity = GetMes_PurchaseHeadEntity(keyValue); 
                db.Delete<Mes_PurchaseHeadEntity>(t=>t.ID == keyValue);
                db.Delete<Mes_PurchaseDetailEntity>(t=>t.P_PurchaseNo == mes_PurchaseHeadEntity.P_PurchaseNo);
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
        public void SaveEntity(string keyValue, Mes_PurchaseHeadEntity entity,List<Mes_PurchaseDetailEntity> mes_PurchaseDetailList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var mes_PurchaseHeadEntityTmp = GetMes_PurchaseHeadEntity(keyValue); 
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<Mes_PurchaseDetailEntity>(t=>t.P_PurchaseNo == mes_PurchaseHeadEntityTmp.P_PurchaseNo);
                    foreach (Mes_PurchaseDetailEntity item in mes_PurchaseDetailList)
                    {
                        item.Create();
                        item.P_PurchaseNo = mes_PurchaseHeadEntityTmp.P_PurchaseNo;
                        db.Insert(item);
                    }
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                    foreach (Mes_PurchaseDetailEntity item in mes_PurchaseDetailList)
                    {
                        item.Create();
                        item.P_PurchaseNo = entity.P_PurchaseNo;
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
