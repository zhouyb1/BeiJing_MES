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
    /// 日 期：2019-03-13 20:54
    /// 描 述：抽检记录
    /// </summary>
    public partial class Mes_InspectService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_InspectEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.I_InspectNo,
                t.I_Date,
                t.I_OrderNo,
                t.I_Kind,
                t.I_StockCode,
                t.I_StockName,
                t.I_Status,
                t.I_GoodsCode,
                t.I_GoodsName,
                t.I_GoodsQty,
                t.I_QualifiedQty,
                t.I_Batch,
                r.R_Name AS I_Reson,
                t.I_CreateBy,
                t.I_CreateDate
                ");
                strSql.Append("  FROM Mes_Inspect t LEFT JOIN Mes_Reson r ON t.I_Reson=r.R_Code");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.I_Date >= @startTime AND t.I_Date <= @endTime ) ");
                }
                if (!queryParam["I_Date"].IsEmpty())
                {
                    dp.Add("I_Date", "%" + queryParam["I_Date"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_Date Like @I_Date ");
                }
                if (!queryParam["I_InspectNo"].IsEmpty())
                {
                    dp.Add("I_InspectNo", "%" + queryParam["I_InspectNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_InspectNo Like @I_InspectNo ");
                }
                if (!queryParam["I_OrderNo"].IsEmpty())
                {
                    dp.Add("I_OrderNo", "%" + queryParam["I_OrderNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_OrderNo Like @I_OrderNo ");
                }
                return this.BaseRepository().FindList<Mes_InspectEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_Inspect表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_InspectEntity GetMes_InspectEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_InspectEntity>(keyValue);
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
                this.BaseRepository().Delete<Mes_InspectEntity>(t=>t.ID == keyValue);
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
        public void SaveEntity(string keyValue, Mes_InspectEntity entity)
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
                    var dp = new DynamicParameters(new { });
                    dp.Add("@BillType", "抽检单");
                    dp.Add("@Doucno", "", DbType.String, ParameterDirection.Output);
                    this.BaseRepository().ExecuteByProc("sp_GetDoucno", dp);
                    var billNo = dp.Get<string>("@Doucno");//存储过程返回单号
                    entity.I_InspectNo = billNo;
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
