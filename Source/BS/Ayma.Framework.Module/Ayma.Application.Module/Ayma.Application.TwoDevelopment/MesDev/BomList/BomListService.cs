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
    /// 日 期：2019-03-02 14:08
    /// 描 述：BOM表
    /// </summary>
    public partial class BomListService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_BomEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.B_Date,
                t.B_OrderNo,
                t.B_GoodsCode,
                t.B_GoodsName,
                t.B_Unit,
                t.B_Grade,
                t.B_SecGoodsCode,
                t.B_SecGoodsName,
                t.B_SecUnit,
                t.B_SecQty,
                t.B_Conversion,
                t.B_CreateBy,
                t.B_CreateDate,
                t.B_UpdateBy,
                t.B_UpdateDate,
                t.B_Remark
                ");
                strSql.Append("  FROM Mes_Bom t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.B_CreateDate >= @startTime AND t.B_CreateDate <= @endTime ) ");
                }
                if (!queryParam["B_CreateDate"].IsEmpty())
                {
                    dp.Add("B_CreateDate", "%" + queryParam["B_CreateDate"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.B_CreateDate Like @B_CreateDate ");
                }
                if (!queryParam["B_GoodsCode"].IsEmpty())
                {
                    dp.Add("B_GoodsCode", "%" + queryParam["B_GoodsCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.B_GoodsCode Like @B_GoodsCode ");
                }
                if (!queryParam["B_GoodsName"].IsEmpty())
                {
                    dp.Add("B_GoodsName", "%" + queryParam["B_GoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.B_GoodsName Like @B_GoodsName ");
                }
                return this.BaseRepository().FindList<Mes_BomEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_Bom表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_BomEntity GetMes_BomEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_BomEntity>(keyValue);
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
                this.BaseRepository().Delete<Mes_BomEntity>(t=>t.ID == keyValue);
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
        public void SaveEntity(string keyValue, Mes_BomEntity entity)
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
