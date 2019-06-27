using Dapper;
using Ayma.DataBase.Repository;
using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-20 09:36
    /// 描 述：物料转换对应表
    /// </summary>
    public partial class ConvertService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_ConvertEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.C_Code,
                t.C_Name,
                t.C_SecCode,
                t.C_SecName, 
                t.C_Max,
                t.C_Min,
                t.C_CreateBy,
                t.C_CreateDate,
                t.C_UpdateBy,
                t.C_UpdateDate,
                t.C_Remark
                ");
                strSql.Append("  FROM Mes_Convert t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.C_CreateDate >= @startTime AND t.C_CreateDate <= @endTime ) ");
                }
                if (!queryParam["C_CreateDate"].IsEmpty())
                {
                    dp.Add("C_CreateDate", "%" + queryParam["C_CreateDate"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_CreateDate Like @C_CreateDate ");
                }
                if (!queryParam["C_Code"].IsEmpty())
                {
                    dp.Add("C_Code", "%" + queryParam["C_Code"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_Code Like @C_Code ");
                }
                if (!queryParam["C_Name"].IsEmpty())
                {
                    dp.Add("C_Name", "%" + queryParam["C_Name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_Name Like @C_Name ");
                }
                if (!queryParam["C_SecCode"].IsEmpty())
                {
                    dp.Add("C_SecCode", "%" + queryParam["C_SecCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_SecCode Like @C_SecCode ");
                }
                if (!queryParam["C_SecName"].IsEmpty())
                {
                    dp.Add("C_SecName", "%" + queryParam["C_SecName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_SecName Like @C_SecName ");
                }
                return this.BaseRepository().FindList<Mes_ConvertEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_Convert表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_ConvertEntity GetMes_ConvertEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_ConvertEntity>(keyValue);
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
                this.BaseRepository().Delete<Mes_ConvertEntity>(t=>t.ID == keyValue);
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
        public void SaveEntity(string keyValue, Mes_ConvertEntity entity)
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
        #region 验证重复

        /// <summary>
        /// 检查转换后的编码重复性
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="code">父Id</param>
        /// <returns></returns>
        public bool ExistCode(string keyValue, string code)
        {
            try
            {
                var expression = LinqExtensions.True<Mes_ConvertEntity>();
                expression = expression.And(t => t.C_SecCode.Trim().ToUpper() == code.Trim().ToUpper());

                if (!string.IsNullOrEmpty(keyValue))
                {
                    expression = expression.And(t => t.ID != keyValue);
                }
                return !this.BaseRepository().IQueryable(expression).Any();
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
