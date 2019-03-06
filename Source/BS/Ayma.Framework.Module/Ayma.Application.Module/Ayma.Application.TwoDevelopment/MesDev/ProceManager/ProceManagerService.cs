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
    /// 日 期：2019-03-06 14:55
    /// 描 述：工序管理
    /// </summary>
    public partial class ProceManagerService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_ProceEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.P_Record,
                t.P_ProCode,
                t.P_ProName,
                t.P_WorkShop,
                t.P_Remark
                ");
                strSql.Append("  FROM Mes_Proce t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["P_Record"].IsEmpty())
                {
                    dp.Add("P_Record", "%" + queryParam["P_Record"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_Record Like @P_Record ");
                }
                if (!queryParam["P_ProCode"].IsEmpty())
                {
                    dp.Add("P_ProCode", "%" + queryParam["P_ProCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_ProCode Like @P_ProCode ");
                }
                if (!queryParam["P_ProName"].IsEmpty())
                {
                    dp.Add("P_ProName", "%" + queryParam["P_ProName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_ProName Like @P_ProName ");
                }
                if (!queryParam["P_WorkShop"].IsEmpty())
                {
                    dp.Add("P_WorkShop", "%" + queryParam["P_WorkShop"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_WorkShop Like @P_WorkShop ");
                }
                return this.BaseRepository().FindList<Mes_ProceEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_Proce表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_ProceEntity GetMes_ProceEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_ProceEntity>(keyValue);
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
                this.BaseRepository().Delete<Mes_ProceEntity>(t=>t.P_Record == keyValue);
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
        public void SaveEntity(string keyValue, Mes_ProceEntity entity)
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
