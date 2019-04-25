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
    /// 日 期：2019-04-25 15:20
    /// 描 述：社保设置
    /// </summary>
    public partial class SocialSetService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_SocialSetEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.S_UserCode,
                t.S_UserName,
                t.S_Wagebase,
                t.S_PensionUnitRatio,
                t.S_PensionPersonRatio,
                t.S_OutWorkUnitRatio,
                t.S_OutWorkPersonRatio,
                t.S_MedicalUnitRatio,
                t.S_MedicalPresonRatio,
                t.S_InJuryUnitRatio,
                t.S_BearUnitRatio
                ");
                strSql.Append("  FROM Mes_SocialSet t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["S_UserCode"].IsEmpty())
                {
                    dp.Add("S_UserCode", "%" + queryParam["S_UserCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_UserCode Like @S_UserCode ");
                }
                if (!queryParam["S_UserName"].IsEmpty())
                {
                    dp.Add("S_UserName", "%" + queryParam["S_UserName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_UserName Like @S_UserName ");
                }
                return this.BaseRepository().FindList<Mes_SocialSetEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_SocialSet表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_SocialSetEntity GetMes_SocialSetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_SocialSetEntity>(keyValue);
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
                this.BaseRepository().Delete<Mes_SocialSetEntity>(t=>t.ID == keyValue);
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
        public void SaveEntity(string keyValue, Mes_SocialSetEntity entity)
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
