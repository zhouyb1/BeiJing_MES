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
    /// 日 期：2019-03-18 10:37
    /// 描 述：退供应商单查询
    /// </summary>
    public partial class Mes_BackSupplyQueryService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_BackSupplyHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.B_BackSupplyNo,
                t.B_StockCode,
                t.B_StockName,
                t.B_OrderDate,
                t.B_Status,
                dbo.GetUserNameById(t.B_CreateBy) B_CreateBy,
                t.B_CreateDate,
                dbo.GetUserNameById(t.B_UpdateBy) B_UpdateBy,
                t.B_UpdateDate,
                dbo.GetUserNameById(t.B_DeleteBy) B_DeleteBy,
                t.B_DeleteDate,
                dbo.GetUserNameById(t.B_UploadBy) B_UploadBy,
                t.B_UploadDate,
                t.B_Remark
                ");
                strSql.Append("  FROM Mes_BackSupplyHead t ");
                strSql.Append("  WHERE 1=1 and t.B_Status=3");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["B_OrderDate"].IsEmpty())
                {
                    dp.Add("B_OrderDate", "%" + queryParam["B_OrderDate"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.B_CreateDate Like @B_CreateDate ");
                }
                if (!queryParam["B_BackSupplyNo"].IsEmpty())
                {
                    dp.Add("B_BackSupplyNo", "%" + queryParam["B_BackSupplyNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.B_BackSupplyNo Like @B_BackSupplyNo ");
                }
                if (!queryParam["B_StockCode"].IsEmpty())
                {
                    dp.Add("B_StockCode", "%" + queryParam["B_StockCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.B_StockCode Like @B_StockCode ");
                }
                if (!queryParam["B_StockName"].IsEmpty())
                {
                    dp.Add("B_StockName", "%" + queryParam["B_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.B_StockName Like @B_StockName ");
                }
                return this.BaseRepository().FindList<Mes_BackSupplyHeadEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_BackSupplyDetail表数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_BackSupplyDetailEntity> GetMes_BackSupplyDetailList(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_BackSupplyDetailEntity>(t=>t.B_BackSupplyNo == keyValue );
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
        /// 获取Mes_BackSupplyHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_BackSupplyHeadEntity GetMes_BackSupplyHeadEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_BackSupplyHeadEntity>(keyValue);
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
        /// 获取Mes_BackSupplyDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_BackSupplyDetailEntity GetMes_BackSupplyDetailEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_BackSupplyDetailEntity>(t=>t.B_BackSupplyNo == keyValue);
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
            var db = this.BaseRepository().BeginTrans();
            try
            {
                var mes_BackSupplyHeadEntity = GetMes_BackSupplyHeadEntity(keyValue); 
                db.Delete<Mes_BackSupplyHeadEntity>(t=>t.ID == keyValue);
                db.Delete<Mes_BackSupplyDetailEntity>(t=>t.B_BackSupplyNo == mes_BackSupplyHeadEntity.B_BackSupplyNo);
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
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
        public void SaveEntity(string keyValue, Mes_BackSupplyHeadEntity entity,List<Mes_BackSupplyDetailEntity> mes_BackSupplyDetailList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var mes_BackSupplyHeadEntityTmp = GetMes_BackSupplyHeadEntity(keyValue); 
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<Mes_BackSupplyDetailEntity>(t=>t.B_BackSupplyNo == mes_BackSupplyHeadEntityTmp.B_BackSupplyNo);
                    foreach (Mes_BackSupplyDetailEntity item in mes_BackSupplyDetailList)
                    {
                        item.Create();
                        item.B_BackSupplyNo = mes_BackSupplyHeadEntityTmp.B_BackSupplyNo;
                        db.Insert(item);
                    }
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                    foreach (Mes_BackSupplyDetailEntity item in mes_BackSupplyDetailList)
                    {
                        item.Create();
                        item.B_BackSupplyNo = entity.B_BackSupplyNo;
                        db.Insert(item);
                    }
                }
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
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
