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
    /// 日 期：2019-03-13 19:30
    /// 描 述：不合格原因表
    /// </summary>
    public partial class Mes_ResonService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_ResonEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.R_Code,
                t.R_Name,
                t.R_Remark
                ");
                strSql.Append("  FROM Mes_Reson t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["R_Code"].IsEmpty())
                {
                    dp.Add("R_Code", "%" + queryParam["R_Code"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.R_Code Like @R_Code ");
                }
                if (!queryParam["R_Name"].IsEmpty())
                {
                    dp.Add("R_Name", "%" + queryParam["R_Name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.R_Name Like @R_Name ");
                }
                return this.BaseRepository().FindList<Mes_ResonEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_Reson表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_ResonEntity GetMes_ResonEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_ResonEntity>(keyValue);
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
                this.BaseRepository().Delete<Mes_ResonEntity>(t=>t.ID == keyValue);
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
        public void SaveEntity(string keyValue, Mes_ResonEntity entity)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
                }
                else
                {
                    var dp = new DynamicParameters(new { });
                    dp.Add("@codeType", "不合格原因编码");
                    dp.Add("@code", "", DbType.String, ParameterDirection.Output);
                    dp.Add("@goodsSecNo", "");
                    db.ExecuteByProc("sp_GetCode", dp);
                    var R_Code = dp.Get<string>("@code"); //存储过程返回编号
                    entity.R_Code = R_Code;
                    entity.Create();
                    db.Insert(entity);
                }
                db.Commit();
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
