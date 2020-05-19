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
    /// 日 期：2019-01-08 17:51
    /// 描 述：操作记录查询
    /// </summary>
    public partial class DocumentsMakeLogSeachService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<T_DocumentsMakeLogEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.F_Id,
                t.F_StockCode,
                t.F_StockName,
                t.F_BillType,
                t.F_OperationType,
                t.F_OrderNo,
                t.F_Remark,
                t.F_CreateDate,
                 dbo.GetUserNameById(t.F_CreateUserName) F_CreateUserName
                ");
                strSql.Append("  FROM T_DocumentsMakeLog t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.F_CreateDate >= @startTime AND t.F_CreateDate <= @endTime ) ");
                }
                if (!queryParam["F_StockCode"].IsEmpty())
                {
                    dp.Add("F_StockCode", "%" + queryParam["F_StockCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_StockCode Like @F_StockCode ");
                }
                if (!queryParam["F_StockName"].IsEmpty())
                {
                    dp.Add("F_StockName", "%" + queryParam["F_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_StockName Like @F_StockName ");
                }
                if (!queryParam["F_BillType"].IsEmpty())
                {
                    dp.Add("F_BillType", "%" + queryParam["F_BillType"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_BillType Like @F_BillType ");
                }
                if (!queryParam["F_OperationType"].IsEmpty())
                {
                    dp.Add("F_OperationType", "%" + queryParam["F_OperationType"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_OperationType Like @F_OperationType ");
                }
                if (!queryParam["F_OrderNo"].IsEmpty())
                {
                    dp.Add("F_OrderNo", "%" + queryParam["F_OrderNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_OrderNo Like @F_OrderNo ");
                }
                if (!queryParam["F_CreateUserName"].IsEmpty())
                {
                    dp.Add("F_CreateUserName", "%" + queryParam["F_CreateUserName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_CreateUserName Like @F_CreateUserName ");
                }
                return this.BaseRepository().FindList<T_DocumentsMakeLogEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取T_DocumentsMakeLog表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public T_DocumentsMakeLogEntity GetT_DocumentsMakeLogEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<T_DocumentsMakeLogEntity>(keyValue);
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
                this.BaseRepository().Delete<T_DocumentsMakeLogEntity>(t=>t.F_Id == keyValue);
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
        public void SaveEntity(string keyValue, T_DocumentsMakeLogEntity entity)
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
