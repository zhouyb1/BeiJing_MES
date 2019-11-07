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
    /// 日 期：2019-03-06 12:03
    /// 描 述：车间管理
    /// </summary>
    public partial class WorkShopManagerService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_WorkShopEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.Id,
                t.W_Code,
                t.W_Name,
                t.W_Remark,
                t.CreateDate,
                t.CreateUserName,
                t.ModifyDate,
                t.ModifyUserName
                ");
                strSql.Append("  FROM Mes_WorkShop t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["W_Code"].IsEmpty())
                {
                    dp.Add("W_Code", "%" + queryParam["W_Code"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.W_Code Like @W_Code ");
                }
                if (!queryParam["W_Name"].IsEmpty())
                {
                    dp.Add("W_Name", "%" + queryParam["W_Name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.W_Name Like @W_Name ");
                }
                return this.BaseRepository().FindList<Mes_WorkShopEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_WorkShop表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_WorkShopEntity GetMes_WorkShopEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_WorkShopEntity>(keyValue);
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
                this.BaseRepository().Delete<Mes_WorkShopEntity>(t=>t.Id == keyValue);
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
        public void SaveEntity(string keyValue, Mes_WorkShopEntity entity)
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
                    dp.Add("@codeType", "车间编码");
                    dp.Add("@code", "", DbType.String, ParameterDirection.Output);
                    dp.Add("@goodsSecNo", "");
                    db.ExecuteByProc("sp_GetCode", dp);
                    var W_Code = dp.Get<string>("@code"); //存储过程返回编号
                    entity.W_Code = W_Code;
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
