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
    /// 日 期：2019-01-07 15:11
    /// 描 述：人员走动记录列表
    /// </summary>
    public partial class MoveRecordListService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_MoveRecordEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                 t.ID,
                t.M_UserCode,
                t.M_IP,
                t.M_RFIDCode,
                t.M_DoorCode,
                t.M_DoorName,
                t.M_Date,
                t.M_Remark,
				t.M_Status,
				(select F_RealName from AM_Base_User where F_EnCode= t.M_UserCode) as M_UserName
                ");
                strSql.Append("  FROM Mes_MoveRecord t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["M_UserCode"].IsEmpty())
                {
                    dp.Add("M_UserCode", "%" + queryParam["M_UserCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.M_UserCode Like @M_UserCode ");
                }
                if (!queryParam["M_UserName"].IsEmpty())
                {
                    dp.Add("M_UserName", "%" + queryParam["M_UserName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND (select F_RealName from AM_Base_User where F_EnCode= t.M_UserCode) Like @M_UserName ");
                }
                if (!queryParam["M_DoorName"].IsEmpty())
                {
                    dp.Add("M_DoorName", "%" + queryParam["M_DoorName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.M_DoorName Like @M_DoorName ");
                }
                if (!queryParam["M_RFIDCode"].IsEmpty())
                {
                    dp.Add("M_RFIDCode", "%" + queryParam["M_RFIDCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.M_RFIDCode Like @M_RFIDCode ");
                }
                return this.BaseRepository().FindList<Mes_MoveRecordEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_MoveRecord表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_MoveRecordEntity GetMes_MoveRecordEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_MoveRecordEntity>(keyValue);
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
                this.BaseRepository().Delete<Mes_MoveRecordEntity>(t=>t.ID == keyValue);
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
        public void SaveEntity(string keyValue, Mes_MoveRecordEntity entity)
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
