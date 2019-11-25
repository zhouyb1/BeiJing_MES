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
    /// 日 期：2019-01-07 12:47
    /// 描 述：仓库列表
    /// </summary>
    public partial class StockListService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_StockEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.S_Code,
                t.S_Name,
                t.S_Kind,
                t.S_Peson,
                t.S_CreateBy,
                t.S_CreateDate,
                t.S_UpdateBy,
                t.S_UpdateDate,
                t.S_Remark
                ");
                strSql.Append("  FROM Mes_Stock t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["S_Code"].IsEmpty())
                {
                    dp.Add("S_Code", "%" + queryParam["S_Code"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_Code Like @S_Code ");
                }
                if (!queryParam["S_Name"].IsEmpty())
                {
                    dp.Add("S_Name", "%" + queryParam["S_Name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_Name Like @S_Name ");
                }
                if (!queryParam["S_Kind"].IsEmpty())
                {
                    dp.Add("S_Kind",queryParam["S_Kind"].ToString(), DbType.String);
                    strSql.Append(" AND t.S_Kind = @S_Kind ");
                }
                if (!queryParam["S_Peson"].IsEmpty())
                {
                    dp.Add("S_Peson", "%" + queryParam["S_Peson"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_Peson Like @S_Peson ");
                }
                return this.BaseRepository().FindList<Mes_StockEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_Stock表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_StockEntity GetMes_StockEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_StockEntity>(keyValue);
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
                this.BaseRepository().Delete<Mes_StockEntity>(t=>t.ID == keyValue);
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
        public void SaveEntity(string keyValue, Mes_StockEntity entity)
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
                    dp.Add("@codeType", "仓库编码");
                    dp.Add("@code", "", DbType.String, ParameterDirection.Output);
                    dp.Add("@goodsSecNo", "");
                    dp.Add("@stockType",entity.S_Kind.ToString());                   
                    db.ExecuteByProc("sp_GetCode", dp);
                    var S_Code = dp.Get<string>("@code"); //存储过程返回编号
                    entity.S_Code = S_Code;
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
