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
    /// 日 期：2019-03-05 20:34
    /// 描 述：用户表
    /// </summary>
    public partial class SysUsersService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Sys_UsersEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.ID,
                t.U_Code,
                t.U_Name,
                t.U_Pass,
                t.U_Department,
                t.U_Post,
                t.U_Ralecode,
                t.U_Kind,
                t.U_Telephone,
                t.U_RFIDCode,
                t.U_Group,
                t.U_Indate,
                t.U_Outdate,
                t.U_Cert,
                t.U_Sex,
                t.U_Nation,
                t.U_Record,
                t.U_Origin,
                t.U_Address,
                t.U_Picture1
                ");
                strSql.Append("  FROM Sys_Users t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.U_Indate >= @startTime AND t.U_Indate <= @endTime ) ");
                }
                if (!queryParam["U_Indate"].IsEmpty())
                {
                    dp.Add("U_Indate",queryParam["U_Indate"].ToString(), DbType.String);
                    strSql.Append(" AND t.U_Indate = @U_Indate ");
                }
                if (!queryParam["U_Name"].IsEmpty())
                {
                    dp.Add("U_Name", "%" + queryParam["U_Name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.U_Name Like @U_Name ");
                }
                return this.BaseRepository().FindList<Sys_UsersEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Sys_Users表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Sys_UsersEntity GetSys_UsersEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Sys_UsersEntity>(keyValue);
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
                this.BaseRepository().Delete<Sys_UsersEntity>(t=>t.ID == keyValue);
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
        public void SaveEntity(string keyValue, Sys_UsersEntity entity)
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
