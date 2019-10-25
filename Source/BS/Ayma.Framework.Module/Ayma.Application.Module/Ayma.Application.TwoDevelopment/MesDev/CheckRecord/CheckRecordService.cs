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
    /// 日 期：2019-04-25 15:41
    /// 描 述：考勤管理
    /// </summary>
    public partial class CheckRecordService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_CheckRecordEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.C_PersonId,
                t.C_ScanDate,
                t.C_ScanTime,
                t.C_Remark,
                A.D_Code,
				A.F_TeamName,
	            A.F_RealName
                ");
                strSql.Append("  FROM Mes_CheckRecord t left join AM_Base_User A on(A.F_EnCode=t.C_PersonId) ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.C_ScanDate >= @startTime AND t.C_ScanDate <= @endTime ) ");
                }
                if (!queryParam["C_ScanDate"].IsEmpty())
                {
                    dp.Add("C_ScanDate", "%" + queryParam["C_ScanDate"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_ScanDate Like @C_ScanDate ");
                }
                if (!queryParam["C_PersonId"].IsEmpty())
                {
                    dp.Add("C_PersonId", "%" + queryParam["C_PersonId"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_PersonId Like @C_PersonId ");
                }
                if (!queryParam["C_UserName"].IsEmpty())
                {
                    dp.Add("C_UserName", "%" + queryParam["C_UserName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND A.F_Account Like @C_UserName ");
                }
                if (!queryParam["D_Code"].IsEmpty())
                {
                    dp.Add("D_Code", "%" + queryParam["D_Code"].ToString() + "%", DbType.String);
                    strSql.Append(" AND A.D_Code Like @D_Code ");
                }
                if (!queryParam["F_TeamName"].IsEmpty())
                {
                    dp.Add("F_TeamName", "%" + queryParam["F_TeamName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND A.F_TeamName Like @F_TeamName ");
                }
                return this.BaseRepository().FindList<Mes_CheckRecordEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_CheckRecord表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_CheckRecordEntity GetMes_CheckRecordEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_CheckRecordEntity>(c=>c.ID==keyValue);
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
                this.BaseRepository().Delete<Mes_CheckRecordEntity>(t=>t.ID == keyValue);
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
        public void SaveEntity(string keyValue, Mes_CheckRecordEntity entity)
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
