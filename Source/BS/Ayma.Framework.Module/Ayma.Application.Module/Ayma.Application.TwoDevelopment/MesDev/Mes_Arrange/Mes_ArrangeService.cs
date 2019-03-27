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
    /// 日 期：2019-03-12 17:32
    /// 描 述：排班记录
    /// </summary>
    public partial class Mes_ArrangeService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_ArrangeEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.A_Date,
                t.A_DateTime,
                t.A_OrderNo,
                t.A_WorkShopCode,
                t.A_WorkShopName,
                t.A_F_EnCode,
                t.A_Record,
                t.A_ProCode,
                t.A_ClassCode,
                t.A_Avail,
                t.A_CreateDate,
                t.A_CreateBy,
                t.A_UpdateBy,
                t.A_UpdateDate,
                t.A_Remark
                ");
                strSql.Append("  FROM Mes_Arrange t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.A_Date >= @startTime AND t.A_Date <= @endTime ) ");
                }
                if (!queryParam["A_OrderNo"].IsEmpty())
                {
                    dp.Add("A_OrderNo", "%" + queryParam["A_OrderNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.A_OrderNo Like @A_OrderNo ");
                }
                if (!queryParam["A_F_EnCode"].IsEmpty())
                {
                    dp.Add("A_F_EnCode", "%" + queryParam["A_F_EnCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.A_F_EnCode Like @A_F_EnCode ");
                }
                return this.BaseRepository().FindList<Mes_ArrangeEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取页面列表数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetDataList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.A_Date,
                t.A_DateTime,
                t.A_OrderNo,
                t.A_WorkShopCode,
                t.A_ClassCode
                ");
                strSql.Append("  FROM Mes_Arrange t WHERE 1=1");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["A_OrderNo"].IsEmpty())
                {
                    dp.Add("A_OrderNo", "%" + queryParam["A_OrderNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.A_OrderNo Like @A_OrderNo ");
                }
                strSql.Append("  GROUP BY t.A_Date,t.A_DateTime ,t.A_OrderNo,t.A_WorkShopCode, t.A_ClassCode");
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
        /// 获取页面子列表数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetSubDataList(string datetime, string time, string orderno, string workshopcode,
            string classcode)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.A_F_Encode,
                t.A_Record,
                t.A_ProCode,
                t.A_Avail,
                t.A_Remark
                ");
                strSql.Append(
                    " FROM Mes_Arrange t WHERE 1=1 AND t.A_Date=@A_Date AND t.A_DateTime=@A_DateTime AND t.A_OrderNo=@A_OrderNo");
                strSql.Append(" AND t.A_WorkShopCode=@A_WorkShopCode AND t.A_ClassCode=@A_ClassCode");
                var dp = new DynamicParameters(new {});
                dp.Add("A_Date", datetime, DbType.String);
                dp.Add("A_DateTime", time, DbType.String);
                dp.Add("A_OrderNo", orderno, DbType.String);
                dp.Add("A_WorkShopCode", workshopcode, DbType.String);
                dp.Add("A_ClassCode", classcode, DbType.String);
                return this.BaseRepository().FindTable(strSql.ToString(), dp);
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
        /// 获取Mes_Arrange表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_ArrangeEntity GetMes_ArrangeEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_ArrangeEntity>(keyValue);
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
                this.BaseRepository().Delete<Mes_ArrangeEntity>(t=>t.ID == keyValue);
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
        public void SaveEntity(string keyValue, Mes_ArrangeEntity entity)
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
