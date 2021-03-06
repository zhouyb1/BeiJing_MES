﻿using Dapper;
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
    /// 日 期：2019-01-07 09:31
    /// 描 述：供应商列表
    /// </summary>
    public partial class SupplyListService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_SupplyEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.S_Code,
                t.S_Name,
                t.S_EffectTime,
                dbo.GetUserNameById(t.S_CreateBy) S_CreateBy,
                t.S_CreateDate,
                dbo.GetUserNameById(t.S_UpdateBy) S_UpdateBy,
                t.S_UpdateDate,
                t.S_Remark,
                t.S_Person,
                t.S_Telephone,
                t.S_Corp,
                t.S_Address,
                t.S_TaxCode,
                t.S_Effect1,
                t.S_Effect2,
                t.S_Effect3,
                t.S_Effect4,
                t.S_Effect5
                ");
                strSql.Append("  FROM Mes_Supply t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["S_Name"].IsEmpty())
                {
                    dp.Add("S_Name", "%" + queryParam["S_Name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_Name Like @S_Name ");
                }
                if (!queryParam["S_Code"].IsEmpty())
                {
                    dp.Add("S_Code", "%" + queryParam["S_Code"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_Code Like @S_Code ");
                }
                if (!queryParam["S_Person"].IsEmpty())
                {
                    dp.Add("S_Person", "%" + queryParam["S_Person"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_Person Like @S_Person ");
                }
                if (!queryParam["S_Telephone"].IsEmpty())
                {
                    dp.Add("S_Telephone", "%" + queryParam["S_Telephone"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_Telephone Like @S_Telephone ");
                }
                if (!queryParam["S_Corp"].IsEmpty())
                {
                    dp.Add("S_Corp", "%" + queryParam["S_Corp"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_Corp Like @S_Corp ");
                }
                if (!queryParam["S_Address"].IsEmpty())
                {
                    dp.Add("S_Address", "%" + queryParam["S_Address"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_Address Like @S_Address ");
                }
                return this.BaseRepository().FindList<Mes_SupplyEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_Supply表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_SupplyEntity GetMes_SupplyEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_SupplyEntity>(c=>c.ID==keyValue|| c.S_Code==keyValue);
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
                this.BaseRepository().Delete<Mes_SupplyEntity>(t=>t.ID == keyValue);
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
        public void SaveEntity(string keyValue, Mes_SupplyEntity entity)
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
                    dp.Add("@codeType", "供应商编码");
                    dp.Add("@code", "", DbType.String, ParameterDirection.Output);
                    dp.Add("@goodsSecNo", "");
                    dp.Add("@stockType", "");
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
