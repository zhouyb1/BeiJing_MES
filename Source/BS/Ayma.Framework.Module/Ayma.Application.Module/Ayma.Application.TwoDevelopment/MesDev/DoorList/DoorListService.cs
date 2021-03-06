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
    /// 日 期：2019-01-07 11:04
    /// 描 述：门列表
    /// </summary>
    public partial class DoorListService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_DoorEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.D_Code,
                t.D_Name,
                t.D_WorkShopCode,
                t.D_Remark,
                (select W_Name from Mes_WorkShop w where W_Code=t.D_WorkShopCode) as D_WorkShopName
                ");
                strSql.Append("  FROM Mes_Door t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["D_Name"].IsEmpty())
                {
                    dp.Add("D_Name", "%" + queryParam["D_Name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.D_Name Like @D_Name ");
                }
                if (!queryParam["D_WorkShopCode"].IsEmpty())
                {
                    dp.Add("D_WorkShopCode", "%" + queryParam["D_WorkShopCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.D_WorkShopCode Like @D_WorkShopCode ");
                }
                return this.BaseRepository().FindList<Mes_DoorEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_Door表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_DoorEntity GetMes_DoorEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_DoorEntity>(keyValue);
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
                this.BaseRepository().Delete<Mes_DoorEntity>(t=>t.ID == keyValue);
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
        public void SaveEntity(string keyValue, Mes_DoorEntity entity)
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
                    dp.Add("@codeType", "门编码");
                    dp.Add("@code", "", DbType.String, ParameterDirection.Output);
                    dp.Add("@goodsSecNo", "");
                    dp.Add("@stockType", "");
                    db.ExecuteByProc("sp_GetCode", dp);
                    var D_Code = dp.Get<string>("@code"); //存储过程返回编号
                    entity.D_Code = D_Code;
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
